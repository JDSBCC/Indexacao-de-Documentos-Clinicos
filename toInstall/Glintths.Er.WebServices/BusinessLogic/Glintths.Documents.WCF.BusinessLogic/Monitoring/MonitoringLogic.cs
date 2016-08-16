using System;
using System.Collections.Generic;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CPCHS.Common.BusinessEntities;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public class MonitoringLogic
    {
        public static DocumentLogList GetDocumentLogsByMultiCriteria(string companyDb, long? patTypeId, string patId,
            long? epiTypeId, string epiId, long? instId, long? placeId, long? appId, long? docTypeId, string doc,
            DateTime? docDateBegin, DateTime? docDateEnd, DateTime? arrivDateBegin,
            DateTime? arrivDateEnd, DateTime? procDateBegin, DateTime? procDateEnd,
            long? statusId, long? alertId, string showHistory, long? pageNumber, long? itemsPerPage, string orderField, string orderType)
        {
            string logIds = "";
            DocumentLogList docLogs = MonitoringManagementBER.Instance.GetDocLogsByMultiCriteria(companyDb, patTypeId, patId, epiTypeId,
                                                                                                 epiId, instId, placeId, appId, docTypeId, doc, docDateBegin, docDateEnd, arrivDateBegin, arrivDateEnd,
                                                                                                 procDateBegin, procDateEnd, statusId, alertId, showHistory, pageNumber, itemsPerPage, orderField, orderType);
            //obter os identificadores dos logs para pesquisas posteriores
            if (docLogs != null && docLogs.Items.Count > 0)
            {
                logIds = docLogs.Items.Aggregate(logIds, (current, docLog) => current + (docLog.DocLogId + ","));
                logIds.Remove(logIds.Length - 1);
                DocumentIndexLogList docIdxLogs = MonitoringManagementBER.Instance.GetDocIdxLogsByLogIdMulti(companyDb, logIds);
                InsertIndexLogs(docLogs, docIdxLogs);
            }
            return docLogs;
        }

        private static void InsertIndexLogs(DocumentLogList docLogs, DocumentIndexLogList docIdxLogs)
        {
            foreach (DocumentLog docLog in docLogs.Items)
            {
                docLog.DocLogIndexLog = new DocumentIndexLogList();
                foreach (DocumentIndexLog docIdxLog in docIdxLogs.Items)
                {
                    if (docLog.DocLogId == docIdxLog.DocIdxLogDocLogId)
                    {
                        docLog.DocLogIndexLog.Items.Add(docIdxLog);
                    }
                }
            }
        }

        public static AlertList GetAlertsByMultiCriteria(
            string companyDb, long? instId, long? placeId, long? appId, long? docTypeId, DateTime? dateBegin,
            DateTime? dateEnd, long? statusId, long? alertTypeId, string showHistory, long? pageNumber,
            long? itemsPerPage, string orderField, string orderType
            )
        {
            return MonitoringManagementBER.Instance.GetAlertsByMultiCriteria(companyDb, instId, placeId, appId, docTypeId,
                                                                                             dateBegin, dateEnd, statusId, alertTypeId, showHistory, pageNumber, itemsPerPage, orderField, orderType);
        }

        public static AlertList GetAlertsSubscriptions(string companyDb, string userId)
        {
            AlertSubscriptionList alertsList = MonitoringManagementBER.Instance.GetAlertsSubscription(companyDb, userId);
            AlertList alist = new AlertList();
            if (alertsList != null && alertsList.Items != null && alertsList.Items.Count != 0)
            {
                foreach(AlertSubscription aas in alertsList.Items)
                {
                    alist.Add(new Alert { AlertSubscription = aas });
                }
            }
            return alist;
        }

        public static void IgnoredAlert(string companyDb, long alertId)
        {
            MonitoringManagementBER.Instance.IgnoredAlert(companyDb, new Alert { AlertId = alertId }, new DbTransaction[]{});
        }

        public static void CorrectedAlert(string companyDb, long alertId)
        {
            MonitoringManagementBER.Instance.CorrectedAlert(companyDb, new Alert { AlertId = alertId }, new DbTransaction[] { });
        }

        public static void ProcessIndexDocument(string companyDb, long indexId)
        {
            MonitoringManagementBER.Instance.ProcessIndexDocument(companyDb, new DocumentLog { DocLogXmlId = indexId }, new DbTransaction[] { });
        }

        public static string UpdateAlertSubscriptions(string companyDb, List<AlertSubscription> alertsSubsList)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("DocumentsWCF", companyDb);
            using (DbConnection conn = dal.CreateConnection())
            {
                conn.Open();
                using (DbTransaction transaction1 = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (AlertSubscription alertSubscription in alertsSubsList)
                        {
                            switch (alertSubscription.CrudCode)
                            {
                                case "I":
                                    MonitoringManagementBER.Instance.InsertAlertSubscription(companyDb, alertSubscription, transaction1);
                                    break;
                                case "U":
                                    MonitoringManagementBER.Instance.UpdateAlertSubscription(companyDb, alertSubscription, transaction1);
                                    break;
                                case "D":
                                    MonitoringManagementBER.Instance.DeleteAlertSubscription(companyDb, alertSubscription, transaction1);
                                    break;
                            }
                        }
                        transaction1.Commit();
                        return "S";
                    }
                    catch (Exception e1)
                    {
                        transaction1.Rollback();
                        conn.Close();
                        throw new CpchsException(e1.Message);
                    }
                }
            }
        }
    }
}