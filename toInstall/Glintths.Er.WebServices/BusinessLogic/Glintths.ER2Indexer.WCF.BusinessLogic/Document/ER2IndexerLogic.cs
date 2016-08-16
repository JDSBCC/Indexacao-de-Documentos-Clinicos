using Cpchs.ER2Indexer.WCF.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using CPCHS.Common.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Cpchs.ER2Indexer.WCF.BusinessLogic
{
    public class ER2IndexerLogic
    {
        public static string CancelDocument(string companyDb, string instCode, string placeCode, string appCode, string docTypeCode, string docId)
        {
            try
            {
                return ER2IndexerDocumentBER.Instance.CancelDocument(companyDb, instCode, placeCode, appCode, docTypeCode, docId);
            }
            catch
            {
                return "-1";
            }
        }

        //public static void IndexDocument(string companyDb, DocumentInfo documentInfo, string username)
        //{

        //    CallContext.SetData("USERNAME",username );

        //    IndexDocument(companyDb, documentInfo);
        //}

        public static void IndexDocument(string companyDb, DocumentInfo documentInfo)
        {
            ERConfiguration config = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSFILESTRATEGY", "FILE_UPLOAD");
            string className = "Cpchs.ER2Indexer.WCF.BusinessLogic.EresultsStrategyDatabase, Glintths.ER2Indexer.WCF.BusinessLogic";
            if (config != null && !string.IsNullOrEmpty(config.ErConfigValue))
            {
                className = config.ErConfigValue;
            }

            var obj = SaveEresultsFileStrategyFactory.Create(Type.GetType(className));

            if (null == obj)
            {
                throw new CpchsException("Erro ao inicializar o AbstractEresultsFileStrategy");
            }
            obj.IndexDocument(companyDb, documentInfo);
        }

        public static void ReprocessDocument(string companyDb, decimal xmlIndexId)
        {
            ERConfiguration config = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSFILESTRATEGY", "FILE_UPLOAD");
            string className = "Cpchs.ER2Indexer.WCF.BusinessLogic.EresultsStrategyDatabase, Glintths.ER2Indexer.WCF.BusinessLogic";
            if (config != null && !string.IsNullOrEmpty(config.ErConfigValue))
            {
                className = config.ErConfigValue;
            }

            var obj = SaveEresultsFileStrategyFactory.Create(Type.GetType(className));

            if (null == obj)
            {
                throw new CpchsException("Erro ao inicializar o AbstractEresultsFileStrategy");
            }
            obj.ReprocessDocument(companyDb, xmlIndexId);
        }

        public static void InsertDocInfo(string companyDb, DocumentInfo documentInfo, DbTransaction transaction)
        {
            string data = documentInfo.DocumentInfoXml.ToString();
            int i = 0;

            int window = 30 * 1024;

            while (i < data.Length)
            {
                if ((i + window) > data.Length)
                {
                    window = data.Length - i;
                }

                documentInfo.DocumentInfoPartialStream = data.Substring(i, window);
                ER2IndexerDocumentBER.Instance.UpdateDocInfo(companyDb, documentInfo, transaction);

                i = i + window;
            }
        }

        public static bool HasToGenerateThumb(DocumentInfo documentInfo, FileStreamInfo fsi)
        {
            if ((fsi.FileStreamInfoThumbFile as byte[]) == null)
            {
                XmlDocument info = new XmlDocument();
                info.LoadXml(documentInfo.DocumentInfoXml.ToString());
                string fileExt = string.Empty;
                foreach (XmlNode fileNode in info.GetElementsByTagName("Ficheiro"))
                {
                    if (fileNode.ChildNodes[0].InnerText == fsi.FileStreamInfoFileName)
                    {
                        if (fileNode.ChildNodes.Count == 1 || String.IsNullOrEmpty(fileNode.ChildNodes[1].InnerText))
                        {
                            int it = fsi.FileStreamInfoFileName.LastIndexOf('.');
                            if (it != -1)
                            {
                                fileExt = fsi.FileStreamInfoFileName.Substring(it + 1);
                            }
                        }
                        else
                        {
                            fileExt = fileNode.ChildNodes[1].InnerText;
                        }
                    }
                }
                if (fileExt.Equals("png", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("jpg", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("tiff", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("tif", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("bmp", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("gif", StringComparison.CurrentCultureIgnoreCase) ||
                      fileExt.Equals("wmf", StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public abstract class AbstractEresultsFileStrategy
    {
        public abstract void Initialize();

        public abstract void IndexDocument(string companyDb, DocumentInfo documentInfo);
        public abstract void ReprocessDocument(string companyDb, decimal xmlIndexId);
    
    }

    public class EresultsStrategyDatabase : AbstractEresultsFileStrategy
    {
        public override void Initialize()
        {
        }

        public override void ReprocessDocument(string companyDb, decimal xmlIndexId)
        {

        }

        public override void IndexDocument(string companyDb, DocumentInfo documentInfo)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("ER2Indexer", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {
                    try
                    {
                        documentInfo = ER2IndexerDocumentBER.Instance.InsertEmptyDocInfo(companyDb, documentInfo, transaction1);
                        ER2IndexerLogic.InsertDocInfo(companyDb, documentInfo, transaction1);
                        foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
                        {
                            fsi.FileStreamInfoXmlId = documentInfo.DocumentInfoXmlId;
                            fsi.FileStreamInfoFileId = Guid.NewGuid().ToString();

                            InsertFileStream(companyDb, fsi, transaction1);
                            InsertThumbnailStream(companyDb, documentInfo, fsi, transaction1);
                        }
                        transaction1.Commit();
                    }
                    catch (Exception e1)
                    {
                        transaction1.Rollback();
                        throw new CpchsException(e1.Message);
                    }
                    finally { conn.Close(); }
                }
            }

            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction2 = conn.BeginTransaction())
                {
                    try
                    {
                        AddFormResponse(companyDb, documentInfo, dal, transaction2);

                        ER2IndexerDocumentBER.Instance.FinalizeDocInfo(companyDb, documentInfo, transaction2);
                        transaction2.Commit();
                    }
                    catch (Exception e1)
                    {
                        transaction2.Rollback();
                        throw new CpchsException(e1.Message);
                    }
                    finally { conn.Close(); }
                }
            }
        }

        private void AddFormResponse(string companyDb, DocumentInfo documentInfo, Database dal, DbTransaction transaction)
        {
            if (documentInfo != null && !string.IsNullOrEmpty(documentInfo.FormResponse))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string[] keys = documentInfo.FormFilter.Split(new char[] { ',' });
                foreach (var k in keys)
                {
                    string[] keyvalue = k.Split(new char[] { '=' });
                    dic.Add(keyvalue[0], keyvalue[1]);
                }

                Glintths.DF.BusinessRules.DForms.BusinessRules.DFormsManagementBr.Instance.DFormExternalSave(companyDb, documentInfo.FormResponse, "ERESULTS", dic, dal, transaction);
            }
        }

        private static void InsertThumbnailStream(string companyDb, DocumentInfo documentInfo, FileStreamInfo fsi, DbTransaction transaction)
        {
            if (ER2IndexerLogic.HasToGenerateThumb(documentInfo, fsi))
            {
                fsi.FileStreamInfoThumbFile = ThumbGenerator.GetThumb(fsi.FileStreamInfoBinaryData as byte[]);
                byte[] data = fsi.FileStreamInfoThumbFile as byte[];
                int i = 0;
                int window = 30 * 1024;
                if (data != null)
                {
                    while (i < data.Length)
                    {
                        if ((i + window) > data.Length)
                        {
                            window = data.Length - i;
                        }
                        byte[] part = new byte[window];
                        for (int j = 0; j < window; j++)
                        {
                            part[j] = data[i + j];
                        }
                        fsi.FileInfoPartialStream = part;
                        ER2IndexerDocumentBER.Instance.UpdateFileStreamInfoThumb(companyDb, fsi, transaction);
                        i += window;
                    }
                }
            }
        }

        private static void InsertFileStream(string companyDb, FileStreamInfo fsi, DbTransaction transaction)
        {
            fsi = ER2IndexerDocumentBER.Instance.InsertEmptyFileStreamInfo(companyDb, fsi, transaction);
            byte[] data = fsi.FileStreamInfoBinaryData as byte[];
            int i = 0;
            int window = 30 * 1024;
            if (data != null)
            {
                while (i < data.Length)
                {
                    if ((i + window) > data.Length)
                    {
                        window = data.Length - i;
                    }
                    byte[] part = new byte[window];
                    for (int j = 0; j < window; j++)
                    {
                        part[j] = data[i + j];
                    }
                    fsi.FileInfoPartialStream = part;
                    ER2IndexerDocumentBER.Instance.UpdateFileStreamInfo(companyDb, fsi, transaction);
                    i += window;
                }
            }
        }
    }

    public class EresultsStrategyFileSystem : AbstractEresultsFileStrategy
    {
        public override void Initialize()
        {
        }

        //public override void IndexDocument(string companyDb, DocumentInfo documentInfo)
        //{
        //    Database dal = CPCHS.Common.Database.Database.GetDatabase("ER2Indexer", companyDb);
        //    using (DbConnection conn = dal.CreateConnection())
        //    {
        //        conn.Open();
        //        using (DbTransaction transaction1 = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                documentInfo = ER2IndexerDocumentBER.Instance.InsertEmptyDocInfo(companyDb, documentInfo, transaction1);
        //                ER2IndexerLogic.InsertDocInfo(companyDb, documentInfo, transaction1);
        //                foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
        //                {
        //                    fsi.FileStreamInfoXmlId = documentInfo.DocumentInfoXmlId;
        //                    fsi.FileStreamInfoFileId = Guid.NewGuid().ToString();
        //                    ER2IndexerDocumentBER.Instance.InsertEmptyFileStreamInfo(companyDb, fsi, transaction1);
        //                }
        //                transaction1.Commit();
        //            }
        //            catch (Exception e1)
        //            {
        //                transaction1.Rollback();
        //                throw new CpchsException(e1.Message);
        //            }
        //            finally { conn.Close(); }
        //        }
        //    }

        //    using (DbConnection conn = dal.CreateConnection())
        //    {
        //        conn.Open();
        //        using (DbTransaction transaction2 = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                ER2IndexerDocumentBER.Instance.FinalizeDocInfo(companyDb, documentInfo, transaction2);
        //                transaction2.Commit();
        //            }
        //            catch (Exception e1)
        //            {
        //                transaction2.Rollback();
        //                throw new CpchsException(e1.Message);
        //            }
        //            finally { conn.Close(); }
        //        }
        //    }

        //    string eresultsBasePath = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSFILEPATH", "FILE_UPLOAD").ErConfigValue;
        //    foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
        //    {
        //        SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoBinaryData as byte[], false);
        //        if (ER2IndexerLogic.HasToGenerateThumb(documentInfo, fsi))
        //        {
        //            fsi.FileStreamInfoThumbFile = ThumbGenerator.GetThumb(fsi.FileStreamInfoBinaryData as byte[]);
        //            SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoThumbFile as byte[], true);
        //        }
        //    }
        //}



        //private static void SaveFile(string companyDb, string eresultsBasePath, decimal indexerId, string fileName, byte[] fileData, bool isThumb)
        //{
        //    string fullFileName = GetFullPath(companyDb, eresultsBasePath, indexerId, fileName, isThumb);
        //    string fileDirName = Path.GetDirectoryName(fullFileName);
        //    if (fileDirName != null)
        //    {
        //        Directory.CreateDirectory(fileDirName);
        //    }
        //    MemoryStream memoryStream = new MemoryStream(fileData);
        //    BinaryWriter binaryWriter = new BinaryWriter(new FileStream(fullFileName, FileMode.Create));
        //    byte[] binaryData = new byte[memoryStream.Length];
        //    memoryStream.Read(binaryData, 0, binaryData.Length);
        //    binaryWriter.Write(binaryData);
        //    binaryWriter.Close();
        //    memoryStream.Close();
        //}

        //private static string GetFullPath(string companyDb, string basePath, decimal indexerId, string fileName, bool isThumb)
        //{
        //    DocumentInfo docInfo = ER2IndexerDocumentBER.Instance.GetDocumentInfo(companyDb, indexerId);
        //    string fullFilePath = Path.Combine(basePath, docInfo.DocumentInfoInstCod);
        //    fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoPlaceCod);
        //    fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoAppCod);
        //    fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoDocTypeCod);
        //    if (docInfo.DtCri != null)
        //    {
        //        string[] date = docInfo.DtCri.Value.ToString("yyyy-MM-dd").Split('-');
        //        fullFilePath = Path.Combine(fullFilePath, date[0]);
        //        fullFilePath = Path.Combine(fullFilePath, date[1]);
        //    }
        //    fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoDocId.ToString(CultureInfo.InvariantCulture));
        //    if (isThumb)
        //    {
        //        fullFilePath = Path.Combine(fullFilePath, "thumbs");
        //    }
        //    StringBuilder fullFileName = new StringBuilder();
        //    fullFileName.Append(docInfo.ElementoId).Append("_");
        //    fullFileName.Append(docInfo.CodVersao);
        //    fullFileName.Append(Path.GetExtension(fileName));
        //    return Path.Combine(fullFilePath, fullFileName.ToString());
        //}

        public override void IndexDocument(string companyDb, DocumentInfo documentInfo)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("ER2Indexer", companyDb);
            string eresultsBinPath = string.Empty;
            string message = string.Empty;
            string institution, place, app, doctype;
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {
                    try
                    {
                        documentInfo = ER2IndexerDocumentBER.Instance.InsertEmptyDocInfo(companyDb, documentInfo, transaction1);
                        ER2IndexerLogic.InsertDocInfo(companyDb, documentInfo, transaction1);
                        foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
                        {
                            fsi.FileStreamInfoXmlId = documentInfo.DocumentInfoXmlId;
                            fsi.FileStreamInfoFileId = Guid.NewGuid().ToString();
                            ER2IndexerDocumentBER.Instance.InsertEmptyFileStreamInfo(companyDb, fsi, transaction1);
                        }

                        eresultsBinPath = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSBINPATH", "FILE_UPLOAD").ErConfigValue;

                        string xml = string.Empty;
                        System.Xml.Linq.XDocument doc = null;
                        try
                        {
                            xml = documentInfo.DocumentInfoXml.ToString();

                            doc = System.Xml.Linq.XDocument.Parse(RemoveAllNamespaces(xml));

                            institution = doc.Root.Element("Documento").Element("Instituicao").Value;
                            place = doc.Root.Element("Documento").Element("Local").Value;
                            app = doc.Root.Element("Documento").Element("Origem").Value;
                            doctype = doc.Root.Element("Documento").Element("Tipo").Value;


                            eresultsBinPath = GetOverridedBinPath(companyDb, eresultsBinPath, institution, place, app, doctype);


                        }
                        catch (Exception e)
                        {
                        }

                        

                        foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
                        {
                            SaveBinFile(companyDb, eresultsBinPath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoBinaryData as byte[]);
                        }

                        transaction1.Commit();
                    }
                    catch (Exception e1)
                    {
                        transaction1.Rollback();
                        throw new CpchsException(e1.Message);
                    }
                    finally { conn.Close(); }
                }
            }

            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction2 = conn.BeginTransaction())
                {
                    try
                    {
                        AddFormResponse(companyDb, documentInfo, dal, transaction2);

                        message = "FinalizeDocInfo";
                        ER2IndexerDocumentBER.Instance.FinalizeDocInfo(companyDb, documentInfo, transaction2);
                        message = "eresultsBasePath";
                        string eresultsBasePath = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSFILEPATH", "FILE_UPLOAD").ErConfigValue;
                        message = "XDocument";

                        string xml = string.Empty;
                        System.Xml.Linq.XDocument doc = null;
                        
                        try
                        {
                            xml = documentInfo.DocumentInfoXml.ToString();

                            doc = System.Xml.Linq.XDocument.Parse(RemoveAllNamespaces(xml));

                            institution = doc.Root.Element("Documento").Element("Instituicao").Value;
                            place = doc.Root.Element("Documento").Element("Local").Value;
                            app = doc.Root.Element("Documento").Element("Origem").Value;
                            doctype = doc.Root.Element("Documento").Element("Tipo").Value;

                            eresultsBasePath = GetOverridedFilePath(companyDb, eresultsBasePath, institution, place, app, doctype);
                        }
                        catch (Exception e)
                        {
                        }

                        string lastFileName = string.Empty;
                        foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
                        {
                            message = "EACH FILE";
                            lastFileName = fsi.FileStreamInfoFileName;
                            string elemExternalId = null;
                            message = "OBTER EXTERNO";
                            try
                            {
                                if (doc != null)
                                {
                                    elemExternalId = doc.Descendants("Elemento").Where(f => f.Descendants("Ficheiro").First().Descendants("NomeOriginal").FirstOrDefault().Value == fsi.FileStreamInfoFileName).First().Descendants("IdExternoElem").First().Value;
                                }
                            }
                            catch
                            {
                                elemExternalId = null;
                            }

                            message = "SAVE FILE" + documentInfo.DocumentInfoXmlId + " " + fsi.FileStreamInfoFileName;
                            SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoBinaryData as byte[], false, elemExternalId, transaction2);
                            if (ER2IndexerLogic.HasToGenerateThumb(documentInfo, fsi))
                            {
                                message = "SAVE THUMB FILE";
                                fsi.FileStreamInfoThumbFile = ThumbGenerator.GetThumb(fsi.FileStreamInfoBinaryData as byte[]);
                                SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoThumbFile as byte[], true, elemExternalId, transaction2);
                            }
                        }

                        try
                        {
                            message = "REMOVE BIN";
                            RemoveBinFile(companyDb, eresultsBinPath, documentInfo.DocumentInfoXmlId, lastFileName);
                        }
                        catch
                        {

                        }
                        transaction2.Commit();
                    }
                    catch (Exception e1)
                    {
                        transaction2.Rollback();
                        throw new CpchsException(e1.Message + Environment.NewLine + e1.StackTrace);
                    }
                    finally { conn.Close(); }
                }
            }
        }

        private string GetOverridedBinPath(string companyDb, string eresultsBinPath, string institution, string place, string app, string doctype)
        {
            try
            {
                var er = EntityManagementBER.Instance.GetIndexFilesConfiguration(companyDb, institution, place, app, doctype, "ER_INDEX_BIN_PATH");

                if (er != null && er.ErConfigValue != null)
                    return er.ErConfigValue;
                return eresultsBinPath;
            }
            catch (Exception e)
            {
                return eresultsBinPath;
            }
        }

        private string GetOverridedFilePath(string companyDb, string eresultsFilPath,string institution, string place, string app, string doctype)
        {
            try
            {
                var er = EntityManagementBER.Instance.GetIndexFilesConfiguration(companyDb, institution, place, app, doctype, "ER_INDEX_FILE_PATH");

                if (er != null && er.ErConfigValue != null)
                    return er.ErConfigValue;
                return eresultsFilPath;
            }
            catch (Exception e)
            {
                return eresultsFilPath;
            }
        }

        private void AddFormResponse(string companyDb, DocumentInfo documentInfo, Database dal, DbTransaction transaction)
        {
            if (documentInfo != null && !string.IsNullOrEmpty(documentInfo.FormResponse))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string[] keys = documentInfo.FormFilter.Split(new char[] { ',' });
                foreach (var k in keys)
                {
                    string[] keyvalue = k.Split(new char[] { '=' });
                    dic.Add(keyvalue[0], keyvalue[1]);
                }

                Glintths.DF.BusinessRules.DForms.BusinessRules.DFormsManagementBr.Instance.DFormExternalSave(companyDb, documentInfo.FormResponse, "ERESULTS", dic, dal, transaction);
            }
        }

        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        public override void ReprocessDocument(string companyDb, decimal xmlIndexId)
        {
            /*Database dal = CPCHS.Common.Database.Database.GetDatabase("ER2Indexer", companyDb);
            string eresultsBinPath = string.Empty;

            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction2 = conn.BeginTransaction())
                {
                    try
                    {
                        ER2IndexerDocumentBER.Instance.FinalizeDocInfo(companyDb, documentInfo, transaction2);

                        eresultsBinPath = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSBINPATH", "FILE_UPLOAD").ErConfigValue;
                        string eresultsBasePath = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "ERESULTSFILEPATH", "FILE_UPLOAD").ErConfigValue;

                        string xml = string.Empty;
                        System.Xml.Linq.XDocument doc = null;
                        try
                        {
                            xml = documentInfo.DocumentInfoXml.ToString();

                            doc = System.Xml.Linq.XDocument.Parse(xml);
                        }
                        catch (Exception e)
                        {
                        }

                        string lastFileName = string.Empty;
                        foreach (FileStreamInfo fsi in documentInfo.FilesStreams.Items)
                        {
                            lastFileName = fsi.FileStreamInfoFileName;
                            string elemExternalId = null;
                            try
                            {
                                if (doc != null)
                                {
                                    elemExternalId = doc.Descendants("Elemento").Where(f => f.Descendants("Ficheiro").First().Descendants("NomeOriginal").FirstOrDefault().Value == fsi.FileStreamInfoFileName).First().Descendants("IdExternoElem").First().Value;
                                }
                            }
                            catch
                            {
                                elemExternalId = null;
                            }

                            SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoBinaryData as byte[], false, elemExternalId, transaction2);
                            if (ER2IndexerLogic.HasToGenerateThumb(documentInfo, fsi))
                            {
                                fsi.FileStreamInfoThumbFile = ThumbGenerator.GetThumb(fsi.FileStreamInfoBinaryData as byte[]);
                                SaveFile(companyDb, eresultsBasePath, documentInfo.DocumentInfoXmlId, fsi.FileStreamInfoFileName, fsi.FileStreamInfoThumbFile as byte[], true, elemExternalId, transaction2);
                            }
                        }

                        RemoveBinFile(companyDb, eresultsBinPath, documentInfo.DocumentInfoXmlId, lastFileName);

                        transaction2.Commit();
                    }
                    catch (Exception e1)
                    {
                        transaction2.Rollback();
                        throw new CpchsException(e1.Message);
                    }
                    finally { conn.Close(); }
                }
            }*/
        }

        private static void RemoveBinFile(string companyDb, string eresultsBasePath, decimal indexerId, string fileName)
        {
            string fullFileName = GetBinPath(companyDb, eresultsBasePath, indexerId, fileName);
            string fileDirName = Path.GetDirectoryName(fullFileName);
            if (fileDirName != null)
            {
                if (Directory.Exists(fileDirName))
                {
                    ForceDeleteDirectory(fileDirName);
                }
            }

        }

        public static void ForceDeleteDirectory(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };

            foreach (var info in directory.GetFileSystemInfos("*"))
            {
                info.Attributes = FileAttributes.Normal;
            }

            directory.Delete(true);
        }

        private static void SaveBinFile(string companyDb, string eresultsBasePath, decimal indexerId, string fileName, byte[] fileData)
        {
            string fullFileName = GetBinPath(companyDb, eresultsBasePath, indexerId, fileName);
            string fileDirName = Path.GetDirectoryName(fullFileName);
            if (fileDirName != null)
            {
                Directory.CreateDirectory(fileDirName);
            }
            MemoryStream memoryStream = new MemoryStream(fileData);
            BinaryWriter binaryWriter = new BinaryWriter(new FileStream(fullFileName, FileMode.Create));
            byte[] binaryData = new byte[memoryStream.Length];
            memoryStream.Read(binaryData, 0, binaryData.Length);
            binaryWriter.Write(binaryData);
            binaryWriter.Close();
            memoryStream.Close();
        }

        private static void SaveFile(string companyDb, string eresultsBasePath, decimal indexerId, string fileName, byte[] fileData, bool isThumb, string external_id, DbTransaction transaction2)
        {
            string fullFileName = GetFullPath(companyDb, eresultsBasePath, indexerId, fileName, isThumb, external_id, transaction2);
            string fileDirName = Path.GetDirectoryName(fullFileName);
            if (fileDirName != null)
            {
                Directory.CreateDirectory(fileDirName);
            }
            MemoryStream memoryStream = new MemoryStream(fileData);
            BinaryWriter binaryWriter = new BinaryWriter(new FileStream(fullFileName, FileMode.Create));
            byte[] binaryData = new byte[memoryStream.Length];
            memoryStream.Read(binaryData, 0, binaryData.Length);
            binaryWriter.Write(binaryData);
            binaryWriter.Close();
            memoryStream.Close();
        }

        private static string GetFullPath(string companyDb, string basePath, decimal indexerId, string fileName, bool isThumb, string external_id, DbTransaction transaction2)
        {
            DocumentInfo docInfo = ER2IndexerDocumentBER.Instance.GetDocumentInfo(companyDb, indexerId, external_id, transaction2);
            //string fullFilePath = Path.Combine(basePath, docInfo.DocumentInfoInstCod);
            //fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoPlaceCod);
            //fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoAppCod);
            //fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoDocTypeCod);
            string fullFilePath = Path.Combine(basePath, docInfo.DocumentInfoInstCodConv);
            fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoPlaceCodConv);
            fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoAppCodConv);
            fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoDocTypeCodConv);

            if (docInfo.DtCri != null)
            {
                string[] date = docInfo.DtCri.Value.ToString("yyyy-MM-dd").Split('-');
                fullFilePath = Path.Combine(fullFilePath, date[0]);
                fullFilePath = Path.Combine(fullFilePath, date[1]);
            }
            fullFilePath = Path.Combine(fullFilePath, docInfo.DocumentInfoDocId.ToString(CultureInfo.InvariantCulture));
            if (isThumb)
            {
                fullFilePath = Path.Combine(fullFilePath, "thumbs");
            }
            StringBuilder fullFileName = new StringBuilder();
            fullFileName.Append(docInfo.ElementoId).Append("_");
            fullFileName.Append(docInfo.CodVersao);
            fullFileName.Append(Path.GetExtension(fileName));
            return Path.Combine(fullFilePath, fullFileName.ToString());
        }

        private static string GetBinPath(string companyDb, string basePath, decimal indexerId, string fileName)
        {
            string fullFilePath = Path.Combine(basePath, indexerId.ToString());

            //string[] date = DateTime.Now.ToString("yyyy-MM-dd").Split('-');
            //fullFilePath = Path.Combine(fullFilePath, date[0]);
            //fullFilePath = Path.Combine(fullFilePath, date[1]);
            fullFilePath = Path.Combine(fullFilePath, fileName);

            return fullFilePath;
        }
    }

    public class SaveEresultsFileStrategyFactory
    {
        public static AbstractEresultsFileStrategy Create(Type providerType)
        {
            AbstractEresultsFileStrategy provider = Activator.CreateInstance(providerType) as AbstractEresultsFileStrategy;
            if (provider != null)
            {
                provider.Initialize();
            }
            return provider;
        }
    }
}