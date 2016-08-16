using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Glintths.Er.Common.BusinessEntities;

namespace Glintths.Er.Interop.BusinessLogic.ClinicalProcess
{
    public class ClinicalProcessInteropLogic
    {
        public static bool PerformAction(string companyDb, string actionsXml, out string message, out string stackTrace)
        {
            bool success = true;
            message = string.Empty;
            stackTrace = string.Empty;
            XDocument doc = XDocument.Parse(actionsXml);
            if (doc.Root != null)
            {
                var nodes = doc.Root.Nodes();
                if (nodes != null)
                {
                    string value = ((XElement)nodes.First()).Element("ID").Value;
                    if (value == "SelectionForReport" || value == "DeletionInfo")
                    {
                        try
                        {
                            Database dal = CPCHS.Common.Database.Database.GetDatabase("InteropWCF", companyDb);
                            using (DbConnection conn = dal.CreateConnection())
                            {
                                conn.Open();
                                using (DbTransaction transaction1 = conn.BeginTransaction())
                                {
                                    foreach (XElement action in nodes)
                                    {
                                        if (action.Element("ID").Value == "SelectionForReport")
                                        {
                                            foreach (XElement elem in action.Element("Info").Nodes())
                                            {
                                                success &= PerformSelectionForReport(companyDb, elem.Element("ElementId").Value, elem.Element("ForReport").Value, elem.Element("Description").Value, elem.Element("PresentationOrder").Value, out message, out stackTrace, transaction1);
                                            }
                                        }

                                        if (action.Element("ID").Value == "DeletionInfo")
                                        {
                                            foreach (XElement elem in action.Element("Info").Nodes())
                                            {
                                                success &= PerformElementCancellation(companyDb, elem.Value, out message, out stackTrace, transaction1);
                                            }
                                        }
                                    }
                                    transaction1.Commit();
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            bool rethrow = ExceptionPolicy.HandleException(e, "Business Entities Exception Policy");
                            message += "\n" + e.Message;
                            stackTrace += "\n" + e.StackTrace; 
                            return false;
                        }
                    }
                }
            }
            return success;
        }

        private static bool PerformElementCancellation(string companyDb, string elemId, out string message, out string stackTrace, DbTransaction transaction)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.Element el = new Cpchs.Eresults.Common.WCF.BusinessEntities.Element()
                {
                    ElementId = long.Parse(elemId),
                };
                DocumentManagementBER.Instance.CancelElement(companyDb, el, transaction);
                message = string.Empty;
                stackTrace = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                message = "\n SelectionForReport[" + elemId + "]: " + e.Message;
                stackTrace = e.StackTrace;
                return false;
            }
        }

        private static bool PerformSelectionForReport(string companyDb, string elemId, string forReport, string description, string presOrder, out string message, out string stackTrace, DbTransaction transaction)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.Element el = new Cpchs.Eresults.Common.WCF.BusinessEntities.Element()
                {
                    ElementId = long.Parse(elemId),
                    ElementReport = forReport == "true" ? "S" : "N",
                    ElementDescription = description,
                    ElementReportPresOrder = string.IsNullOrEmpty(presOrder) ? 0 : long.Parse(presOrder)
                };
                DocumentManagementBER.Instance.UpdateElementReportInfo(companyDb, el, transaction);
                message = string.Empty;
                stackTrace = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                message = "\n SelectionForReport[" + elemId + "]: " + e.Message;
                stackTrace = e.StackTrace;
                return false;
            }
        }
    
        #region Serialization

        private static T Deserialize<T>(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(reader))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    T theObject = (T)serializer.ReadObject(xmlReader);
                    return theObject;
                }
            }
        }

        private static string Serialize<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memoryStream, obj);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var reader = new StreamReader(memoryStream);
                string content = reader.ReadToEnd();
                return content;
            }


            //System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //System.IO.StringWriter writer = new System.IO.StringWriter(sb);
            //ser.Serialize(writer, obj);
            //XmlDocument doc = new XmlDocument();
            //return sb.ToString();
        }

        #endregion Serialization
    }
}
