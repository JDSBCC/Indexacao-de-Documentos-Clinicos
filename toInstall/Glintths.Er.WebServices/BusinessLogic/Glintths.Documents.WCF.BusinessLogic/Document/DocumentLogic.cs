using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Xml.Linq;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public class DocumentLogic
    {
        public static void UpdateElementReportInfo(string companyDb, ElementList elems)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {
                    foreach (Element elem in elems.Items)
                    {
                        DocumentManagementBER.Instance.UpdateElementReportInfo(companyDb, elem, transaction1);
                    }
                    transaction1.Commit();
                }
            }
        }

        public static void CancelDocumentByUniqueId(string companyDb, long documentUniqueId)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {
                    Document doc = new Document { DocumentId = documentUniqueId };
                    DocumentManagementBER.Instance.CancelDocumentByUniqueId(companyDb, doc, transaction1);
                    transaction1.Commit();
                }
            }
        }

        public static void CancelElement(string companyDb, long elementId)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {

                    Element elem = new Element { ElementId = elementId };
                    DocumentManagementBER.Instance.CancelElement(companyDb, elem, transaction1);
                    transaction1.Commit();
                }
            }
        }

        public static DocumentList GetDocumentsByExternalId(string companyDb, string docRef, long appId, long localId, long instId, string docChildType, string docFileType)
        {
            DocumentList docList = DocumentManagementBER.Instance.GetDocumentsByDocRef(companyDb, docRef, appId, localId, instId, docChildType, docFileType);
            // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
            DocumentList newDocList = new DocumentList();
            List<string> ids = new List<string>();
            foreach (Document doc in docList.Items.Where(doc => !ids.Contains(doc.DocumentId.ToString(CultureInfo.InvariantCulture))))
            {
                ids.Add(doc.DocumentId.ToString(CultureInfo.InvariantCulture));
                newDocList.Items.Add(doc);
            }
            // para separar pais e filhos
            DocumentList parentDocList = new DocumentList();
            DocumentList childDocList = new DocumentList();
            foreach (Document doc in newDocList.Items)
            {
                if (doc.DocumentParentId == 0)
                {
                    parentDocList.Items.Add(doc);
                }
                else
                {
                    childDocList.Items.Add(doc);
                }
            }
            // tratamento da hierarquia
            InsertChilds(parentDocList, childDocList);
            return FinalizeChilds(parentDocList);
        }

        public static DocumentList GetDocumentsByExternalIdV2(string companyDb, string docRef, long docTypeId, long appId, long localId, long instId, string docChildType, string docFileType)
        {
            DocumentList docList = DocumentManagementBER.Instance.GetDocumentsByDocRefV2(companyDb, docRef, instId, localId, appId, docTypeId, docChildType, docFileType);
            // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
            DocumentList newDocList = new DocumentList();
            List<string> ids = new List<string>();
            foreach (Document doc in docList.Items.Where(doc => !ids.Contains(doc.DocumentId.ToString(CultureInfo.InvariantCulture))))
            {
                ids.Add(doc.DocumentId.ToString(CultureInfo.InvariantCulture));
                newDocList.Items.Add(doc);
            }
            // para separar pais e filhos
            DocumentList parentDocList = new DocumentList();
            DocumentList childDocList = new DocumentList();
            foreach (Document doc in newDocList.Items)
            {
                if (doc.DocumentParentId == 0)
                {
                    parentDocList.Items.Add(doc);
                }
                else
                {
                    childDocList.Items.Add(doc);
                }
            }
            // tratamento da hierarquia
            InsertChilds(parentDocList, childDocList);
            return FinalizeChilds(parentDocList);
        }

        public static DocumentList GetDocumentsByMultiCriteriaV2(string companyDb, string entitiesIds, string reqService, string execService, string episodeType,
            string episodeId, string patientId, string patientType, DateTime? epiStartDate, DateTime? epiEndDate, string docRef, string extId, string docType, string app, string place,
            string inst, DateTime? execStartDate, DateTime? execEndDate, DateTime? valStartDate, DateTime? valEndDate,
            DateTime? emiStartDate, DateTime? emiEndDate, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters,
            string userName, bool? myReport, bool? myPublic, long? pageNumber, long? itemsPerPage, string orderField, string orderType, long? workspaceId, string filterString, string period, ref long? resultsCount)
        {
            try
            {
                string globalFilters;
                string docsSessionFilters;
                string servsSessionFilters;
                if (myGlobalFilters == null || myGlobalFilters == "N")
                {
                    globalFilters = "N";
                }
                else
                {
                    globalFilters = "S";
                }
                if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
                {
                    docsSessionFilters = "N";
                }
                else
                {
                    docsSessionFilters = "S";
                }
                if (myServsSessionFilters == null || myServsSessionFilters == "N")
                {
                    servsSessionFilters = "N";
                }
                else
                {
                    servsSessionFilters = "S";
                }
                string report = "I";
                if (myReport.HasValue && myReport.Value)
                {
                    report = "S";
                }
                string _public = "S";
                if (myPublic.HasValue && !myPublic.Value)
                {
                    _public = "N";
                }
                DocumentList docList;
                try
                {
                    if (workspaceId.HasValue)
                    {
                        docList = DocumentManagementBER.Instance.GetDocumentsByMultiCriteriaV2(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, epiStartDate,
                            epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                            globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, ref resultsCount);
                    }
                    else
                    {
                        docList = DocumentManagementBER.Instance.GetDocs(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, patientId, patientType, epiStartDate,
                            epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                            globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, period, ref resultsCount);
                    }
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio."+ e.Message + Environment.NewLine+e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                //var list = DocumentManagementBER.Instance.GetDocumentsThumbs(companyDb, docList.Items.Select(x => x.DocumentId).ToList());
                //foreach (var item in list)
                //{
                //    if (item.Thumb == null || item.Thumb.Length == 0)
                //        continue;
                //    Document doc = docList.Items.Where(x => x.DocumentId == item.DocumentId).FirstOrDefault();
                //    if (doc != null)
                //        doc.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                //}
                
                // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
                DocumentList newDocList = new DocumentList();
                /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
                {
                    newDocList.Items.Add(doc);
                }*/

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f=>f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }
                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);
                //todos os filhos que não foram inseridos em pais são tratados como pais
                var list = childDocList.Items.Where(x => !x.Processed);
                foreach (var item in list)
                {
                    parentDocList.Add(item);
                }

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }


        public static DocumentList GetDocumentsByMultiCriteriaV3(string companyDb, string entitiesIds, string reqService, string execService, string episodeType,
    string episodeId, string patientId, string patientType, DateTime? epiStartDate, DateTime? epiEndDate, string docRef, string extId, string docType, string app, string place,
    string inst, DateTime? execStartDate, DateTime? execEndDate, DateTime? valStartDate, DateTime? valEndDate,
    DateTime? emiStartDate, DateTime? emiEndDate, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters,
    string userName, bool? myReport, bool? myPublic, long? pageNumber, long? itemsPerPage, string orderField, string orderType, long? workspaceId, string filterString, string period, ref long? resultsCount)
        {
            try
            {
                string globalFilters;
                string docsSessionFilters;
                string servsSessionFilters;
                if (myGlobalFilters == null || myGlobalFilters == "N")
                {
                    globalFilters = "N";
                }
                else
                {
                    globalFilters = "S";
                }
                if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
                {
                    docsSessionFilters = "N";
                }
                else
                {
                    docsSessionFilters = "S";
                }
                if (myServsSessionFilters == null || myServsSessionFilters == "N")
                {
                    servsSessionFilters = "N";
                }
                else
                {
                    servsSessionFilters = "S";
                }
                string report = "I";
                if (myReport.HasValue && myReport.Value)
                {
                    report = "S";
                }
                string _public = "S";
                if (myPublic.HasValue && !myPublic.Value)
                {
                    _public = "N";
                }

                DocumentList docList;

                try
                {
                    if (workspaceId.HasValue)
                    {
                        docList = DocumentManagementBER.Instance.GetDocumentsByMultiCriteriaV3(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, epiStartDate,
                            epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                            globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, ref resultsCount);
                    }
                    else
                    {
                        docList = DocumentManagementBER.Instance.GetDocs(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, patientId, patientType, epiStartDate,
                            epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                            globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, period, ref resultsCount);
                    }
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio." + e.Message + Environment.NewLine + e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                //var list = DocumentManagementBER.Instance.GetDocumentsThumbs(companyDb, docList.Items.Select(x => x.DocumentId).ToList());
                //foreach (var item in list)
                //{
                //    if (item.Thumb == null || item.Thumb.Length == 0)
                //        continue;
                //    Document doc = docList.Items.Where(x => x.DocumentId == item.DocumentId).FirstOrDefault();
                //    if (doc != null)
                //        doc.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                //}

                // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
                DocumentList newDocList = new DocumentList();
                /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
                {
                    newDocList.Items.Add(doc);
                }*/

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f => f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }
                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);
                //todos os filhos que não foram inseridos em pais são tratados como pais
                var list = childDocList.Items.Where(x => !x.Processed);
                foreach (var item in list)
                {
                    parentDocList.Add(item);
                }

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }

        public static DocumentList GetDocumentsToCancel(string companyDb, string entitiesIds, string reqService, string execService, string episodeType,
            string episodeId, string patientId, string patientType, DateTime? epiStartDate, DateTime? epiEndDate, string docRef, string extId, string docType, string app, string place,
            string inst, DateTime? execStartDate, DateTime? execEndDate, DateTime? valStartDate, DateTime? valEndDate,
            DateTime? emiStartDate, DateTime? emiEndDate, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters,
            string userName, bool? myReport, bool? myPublic, long? pageNumber, long? itemsPerPage, string orderField, string orderType, long? workspaceId, string filterString, string period, ref long? resultsCount)
        {
            try
            {
                string globalFilters;
                string docsSessionFilters;
                string servsSessionFilters;
                if (myGlobalFilters == null || myGlobalFilters == "N")
                {
                    globalFilters = "N";
                }
                else
                {
                    globalFilters = "S";
                }
                if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
                {
                    docsSessionFilters = "N";
                }
                else
                {
                    docsSessionFilters = "S";
                }
                if (myServsSessionFilters == null || myServsSessionFilters == "N")
                {
                    servsSessionFilters = "N";
                }
                else
                {
                    servsSessionFilters = "S";
                }
                string report = "I";
                if (myReport.HasValue && myReport.Value)
                {
                    report = "S";
                }
                string _public = "S";
                if (myPublic.HasValue && !myPublic.Value)
                {
                    _public = "N";
                }
                DocumentList docList;
                try
                {

                    docList = DocumentManagementBER.Instance.GetDocumentsToCancel(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, patientId, patientType, epiStartDate,
                            epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                            globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, period, ref resultsCount);
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio." + e.Message + Environment.NewLine + e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                //var list = DocumentManagementBER.Instance.GetDocumentsThumbs(companyDb, docList.Items.Select(x => x.DocumentId).ToList());
                //foreach (var item in list)
                //{
                //    if (item.Thumb == null || item.Thumb.Length == 0)
                //        continue;
                //    Document doc = docList.Items.Where(x => x.DocumentId == item.DocumentId).FirstOrDefault();
                //    if (doc != null)
                //        doc.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                //}

                // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
                DocumentList newDocList = new DocumentList();
                /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
                {
                    newDocList.Items.Add(doc);
                }*/

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f => f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }
                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);
                //todos os filhos que não foram inseridos em pais são tratados como pais
                var list = childDocList.Items.Where(x => !x.Processed);
                foreach (var item in list)
                {
                    parentDocList.Add(item);
                }

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }

        public static DocumentList GetGeneralDocuments(string companyDb, string entitiesIds, string reqService, string execService, string episodeType,
            string episodeId, string patientId, string patientType, DateTime? epiStartDate, DateTime? epiEndDate, string docRef, string extId, string docType, string app, string place,
            string inst, DateTime? execStartDate, DateTime? execEndDate, DateTime? valStartDate, DateTime? valEndDate,
            DateTime? emiStartDate, DateTime? emiEndDate, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters,
            string userName, bool? myReport, bool? myPublic, long? pageNumber, long? itemsPerPage, string orderField, string orderType, long? workspaceId, string filterString, string period, ref long? resultsCount)
        {
            try
            {
                string globalFilters;
                string docsSessionFilters;
                string servsSessionFilters;
                if (myGlobalFilters == null || myGlobalFilters == "N")
                {
                    globalFilters = "N";
                }
                else
                {
                    globalFilters = "S";
                }
                if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
                {
                    docsSessionFilters = "N";
                }
                else
                {
                    docsSessionFilters = "S";
                }
                if (myServsSessionFilters == null || myServsSessionFilters == "N")
                {
                    servsSessionFilters = "N";
                }
                else
                {
                    servsSessionFilters = "S";
                }
                string report = "I";
                if (myReport.HasValue && myReport.Value)
                {
                    report = "S";
                }
                string _public = "S";
                if (myPublic.HasValue && !myPublic.Value)
                {
                    _public = "N";
                }
                DocumentList docList;
                try
                {
                    docList = DocumentManagementBER.Instance.GetGeneralDocuments(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, patientId, patientType, epiStartDate,
                        epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, reqService, execService,
                        globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, period, ref resultsCount);
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio." + e.Message + Environment.NewLine + e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                //var list = DocumentManagementBER.Instance.GetDocumentsThumbs(companyDb, docList.Items.Select(x => x.DocumentId).ToList());
                //foreach (var item in list)
                //{
                //    if (item.Thumb == null || item.Thumb.Length == 0)
                //        continue;
                //    Document doc = docList.Items.Where(x => x.DocumentId == item.DocumentId).FirstOrDefault();
                //    if (doc != null)
                //        doc.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                //}

                // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
                DocumentList newDocList = new DocumentList();
                /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
                {
                    newDocList.Items.Add(doc);
                }*/

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f => f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }
                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);
                //todos os filhos que não foram inseridos em pais são tratados como pais
                var list = childDocList.Items.Where(x => !x.Processed);
                foreach (var item in list)
                {
                    parentDocList.Add(item);
                }

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }

        public static DocumentList GetCancelledDocuments(string companyDb, string entitiesIds, string reqService, string execService, string episodeType,
            string episodeId, string patientId, string patientType, DateTime? epiStartDate, DateTime? epiEndDate, string docRef, string extId, string docType, string app, string place,
            string inst, DateTime? execStartDate, DateTime? execEndDate, DateTime? valStartDate, DateTime? valEndDate,
            DateTime? emiStartDate, DateTime? emiEndDate, DateTime? cancelDate, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters,
            string userName, bool? myReport, bool? myPublic, long? pageNumber, long? itemsPerPage, string orderField, string orderType, long? workspaceId, string filterString, string period, ref long? resultsCount)
        {
            try
            {
                string globalFilters;
                string docsSessionFilters;
                string servsSessionFilters;
                if (myGlobalFilters == null || myGlobalFilters == "N")
                {
                    globalFilters = "N";
                }
                else
                {
                    globalFilters = "S";
                }
                if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
                {
                    docsSessionFilters = "N";
                }
                else
                {
                    docsSessionFilters = "S";
                }
                if (myServsSessionFilters == null || myServsSessionFilters == "N")
                {
                    servsSessionFilters = "N";
                }
                else
                {
                    servsSessionFilters = "S";
                }
                string report = "I";
                if (myReport.HasValue && myReport.Value)
                {
                    report = "S";
                }
                string _public = "S";
                if (myPublic.HasValue && !myPublic.Value)
                {
                    _public = "N";
                }
                DocumentList docList;
                try
                {
                    docList = DocumentManagementBER.Instance.GetCancelledDocuments(companyDb, docRef, extId, entitiesIds, episodeType, episodeId, patientId, patientType, epiStartDate,
                        epiEndDate, inst, place, app, docType, execStartDate, execEndDate, valStartDate, valEndDate, emiStartDate, emiEndDate, cancelDate, reqService, execService,
                        globalFilters, docsSessionFilters, servsSessionFilters, userName, pageNumber, itemsPerPage, orderField, orderType, report, _public, workspaceId, filterString, period, ref resultsCount);
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio." + e.Message + Environment.NewLine + e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                //var list = DocumentManagementBER.Instance.GetDocumentsThumbs(companyDb, docList.Items.Select(x => x.DocumentId).ToList());
                //foreach (var item in list)
                //{
                //    if (item.Thumb == null || item.Thumb.Length == 0)
                //        continue;
                //    Document doc = docList.Items.Where(x => x.DocumentId == item.DocumentId).FirstOrDefault();
                //    if (doc != null)
                //        doc.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                //}

                // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
                DocumentList newDocList = new DocumentList();
                /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
                {
                    newDocList.Items.Add(doc);
                }*/

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f => f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }
                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);
                //todos os filhos que não foram inseridos em pais são tratados como pais
                var list = childDocList.Items.Where(x => !x.Processed);
                foreach (var item in list)
                {
                    parentDocList.Add(item);
                }

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }

        private static string MakeImageSrcData(byte[] imgBytes)
        {
            return "data:image/png;base64," + Convert.ToBase64String(imgBytes, Base64FormattingOptions.None);
        }

        public static DocumentList GetPatientDocuments(string companyDb, string patientType, string patientId, string episodeType, string episodeId, string institution, string place, string application, string documentType, string userName)
        {
            DocumentList docList = DocumentManagementBER.Instance.GetPatientDocumentsV2(companyDb, patientType, patientId, episodeType, episodeId, institution, place, application, documentType, userName);
            DocumentList newDocList = new DocumentList();
            /*foreach (Document doc in docList.Items.Where(doc => !IdentifyRepeatedDocument(newDocList, doc)))
            {
                newDocList.Items.Add(doc);
            }*/

            foreach (Document doc in docList.Items)
            {
                var ndoc = newDocList.Items.Where(f => f.DocumentId == doc.DocumentId).FirstOrDefault();
                if (ndoc != null)
                {
                    if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                        ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                }
                else
                    newDocList.Add(doc);
            }


            DocumentList parentDocList = new DocumentList();
            DocumentList childDocList = new DocumentList();
            foreach (Document doc in newDocList.Items)
            {
                if (doc.DocumentParentId == 0)
                {
                    parentDocList.Items.Add(doc);
                }
                else
                {
                    childDocList.Items.Add(doc);
                }
            }
            // tratamento da hierarquia
            InsertChilds(parentDocList, childDocList);
            return FinalizeChilds(parentDocList);
        }

        private static bool IdentifyRepeatedDocument(DocumentList docList, Document doc)
        {
            foreach (Document oldDoc in docList.Items.Where(oldDoc => oldDoc.DocumentId == doc.DocumentId).Where(oldDoc => doc.DocumentElements != null && doc.DocumentElements.Items.Count > 0))
            {
                oldDoc.DocumentElements.Items.Add(doc.DocumentElements.Items[0]);
                return true;
            }
            return false;
        }

        public static DocumentList GetPatientDocuments(string companyDb, long? patEntId, long? reqService, long? episodeTypeId, string episodeId, DateTime? epiDateBegin, DateTime? epiDateEnd, string docRef, string extId, long? docTypeId, long? appId, long? localId, long? instId, DateTime? docDateBegin, DateTime? docDateEnd, DateTime? valDateBegin, DateTime? valDateEnd, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters, string userName, bool myUserAnaRes)
        {
            string globalFilters;
            string docsSessionFilters;
            string servsSessionFilters;
            if (myGlobalFilters == null || myGlobalFilters == "N")
            {
                globalFilters = "N";
            }
            else
            {
                globalFilters = "S";
            }
            if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            {
                docsSessionFilters = "N";
            }
            else
            {
                docsSessionFilters = "S";
            }
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            {
                servsSessionFilters = "N";
            }
            else
            {
                servsSessionFilters = "S";
            }
            string userAnaRes = "S";
            if (!myUserAnaRes)
            {
                userAnaRes = "N";
            }
            DocumentList docList = DocumentManagementBER.Instance.GetPatientDocuments(companyDb, docRef, extId, patEntId, episodeTypeId, episodeId, epiDateBegin, epiDateEnd, docTypeId, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, reqService, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);
            // para retirar registos duplicados de documentos (por exemplo, os resultado analíticos ou documentos digitalizados)
            DocumentList newDocList = new DocumentList();
            List<string> ids = new List<string>();
            foreach (Document doc in docList.Items.Where(doc => !ids.Contains(doc.DocumentId.ToString(CultureInfo.InvariantCulture))))
            {
                ids.Add(doc.DocumentId.ToString(CultureInfo.InvariantCulture));
                newDocList.Items.Add(doc);
            }
            // para separar pais e filhos
            DocumentList parentDocList = new DocumentList();
            DocumentList childDocList = new DocumentList();
            foreach (Document doc in newDocList.Items)
            {
                if (doc.DocumentParentId == 0)
                {
                    parentDocList.Items.Add(doc);
                }
                else
                {
                    childDocList.Items.Add(doc);
                }
            }
            // tratamento da hierarquia
            InsertChilds(parentDocList, childDocList);
            return FinalizeChilds(parentDocList);
        }

        public static DocumentList GetPatientDocumentsMulti(string companyDb, string patEntIds, long? reqService, long? episodeTypeId, string episodeId, DateTime? epiDateBegin, DateTime? epiDateEnd, string docRef, string extId, long? docTypeId, long? appId, long? localId, long? instId, DateTime? docDateBegin, DateTime? docDateEnd, DateTime? valDateBegin, DateTime? valDateEnd, string myGlobalFilters, string myDocsSessionFilters, string myServsSessionFilters, string userName, bool myUserAnaRes, long? execService)
        {
            string globalFilters;
            string docsSessionFilters;
            string servsSessionFilters;
            if (myGlobalFilters == null || myGlobalFilters == "N")
            {
                globalFilters = "N";
            }
            else
            {
                globalFilters = "S";
            }
            if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            {
                docsSessionFilters = "N";
            }
            else
            {
                docsSessionFilters = "S";
            }
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            {
                servsSessionFilters = "N";
            }
            else
            {
                servsSessionFilters = "S";
            }
            string userAnaRes = "S";
            if (!myUserAnaRes)
            {
                userAnaRes = "N";
            }
            DocumentList docList = DocumentManagementBER.Instance.GetPatientDocumentsMulti(companyDb, docRef, extId, patEntIds, episodeTypeId, episodeId, epiDateBegin, epiDateEnd, docTypeId, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, reqService, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes,execService);
            DocumentList newDocList = docList;
            // para separar pais e filhos
            DocumentList parentDocList = new DocumentList();
            DocumentList childDocList = new DocumentList();
            foreach (Document doc in newDocList.Items)
            {
                if (doc.DocumentParentId == 0)
                {
                    parentDocList.Items.Add(doc);
                }
                else
                {
                    childDocList.Items.Add(doc);
                }
            }
            // tratamento da hierarquia
            InsertChilds(parentDocList, childDocList);
            return FinalizeChilds(parentDocList);
        }
        
        private static DocumentList FinalizeChilds(DocumentList parentDocList)
        {
            DocumentList finalList = new DocumentList();
            foreach (Document doc in parentDocList.Items)
            {
                if (doc.DocumentChilds != null && doc.DocumentChilds.Count == 1)
                {
                    Document thisDoc = doc.DocumentChilds[0];
                    thisDoc.DocumentParentId = 0;
                    finalList.Add(thisDoc);
                }
                else
                {
                    finalList.Add(doc);
                }
            }
            return finalList;
        }

        public static void InsertChilds(DocumentList parents, DocumentList allChilds)
        {
            foreach (Document paren in parents.Items)
            {
                foreach (Document child in allChilds.Items)
                {
                    if (paren.DocumentId != child.DocumentParentId)
                        continue;
                    if(paren.DocumentElements != null)
                        paren.DocumentElements = new ElementList();
                    if (paren.DocumentChilds.Items != null)
                        paren.DocumentChilds.Items.Add(child);
                    child.Processed = true;
                }
                if (paren.DocumentChilds.Items != null && paren.DocumentChilds.Items.Count > 0)
                {
                    InsertChilds(paren.DocumentChilds, allChilds);
                }
            }
        }

        public static void RegisterDocumentAccess(string companyDb, string sessionId, long? userId, string userName, long docId, string docRef, long artifactId, long versionId, long appOrigin, long docType)
        {
            DocumentManagementBER.Instance.RegisterDocumentAccess(companyDb, sessionId, userId, userName, docId, docRef, artifactId, versionId, appOrigin, docType);
        }

        public static string GetDocTypeDesc(string companyDb, string inst, string place, string app, string tdoc)
        {
            DocumentTypeList docTypes = DocumentManagementBER.Instance.GetDocTypeDesc(companyDb, inst, place, app, tdoc);
            return docTypes.Items.Count > 0 ? docTypes[0].DocumentTypeDescription : "";
        }

        public static VideoList GetDocumentVideos(string companyDb, long docId)
        {
            VideoList videoList = VideoManagementBER.Instance.GetVideosByDocumentId(companyDb, docId);
            VideoLinkList videoLinkList = VideoManagementBER.Instance.GetVideoLinksByDocumentId(companyDb, docId);
            foreach (var video in videoList.Items)
            {
                IEnumerable<VideoLink> thisVideoLinks = videoLinkList.Items.Where(x => x.VideoLinkElemId == video.VideoElemId && x.VideoLinkVersionCode == video.VideoVersionCode);
                video.VideoLinks = new VideoLinkList();
                foreach (var videoLink in thisVideoLinks)
                {
                    video.VideoLinks.Add(videoLink);
                }
            }
            return videoList;
        }
                
        public static bool PromoteDocumentToPublic(string companyDb, List<Document> documentsList)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (Document doc in documentsList)
                        {
                            DocumentManagementBER.Instance.PromoteDocumentToPublic(companyDb, doc, transaction);
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public static string OpenDynamicForm(string companyDb, string app, string doctype, string filter, string user)
        {
            return DocumentManagementBER.Instance.GetDynamicForm(companyDb, app, doctype, filter, user);
        }


        public static bool UpdateElementDescription(string companyDb, long elementId, string title, string description)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {

                        DocumentManagementBER.Instance.UpdateElementDescription(companyDb, transaction, elementId, title, description);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

    }
}