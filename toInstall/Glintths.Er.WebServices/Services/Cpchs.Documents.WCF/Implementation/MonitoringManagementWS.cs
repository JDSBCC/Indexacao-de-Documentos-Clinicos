using System.Collections.Generic;
using System.Linq;
using Cpchs.Documents.WCF.MessageContracts;
using Cpchs.Documents.WCF.BusinessLogic;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;
using Cpchs.Documents.WCF.DataContracts;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public partial class MonitoringManagementWS
    {
        public override GetDocumentLogsByMultiCriteriaResponse GetDocumentLogsByMultiCriteria(GetDocumentLogsByMultiCriteriaRequest request)
		{
            string orderType = null;
            string showHistory = null;
            if (request.PaginationInfo != null)
            {
                ConvertOrderField(request.PaginationInfo.OrderField);
                orderType = ConvertOrderType(request.PaginationInfo.OrderType);
            }
            else
            {
                request.PaginationInfo = new PaginationRequest();
            }

            if (request.SearchCriteria != null)
            {
                showHistory = ConvertShowHistory(request.SearchCriteria.ShowHistory);
            }
            else
            {
                request.SearchCriteria = new DocumentLogSearchCriteria();
            }

            // preparar o objecto de resposta
            GetDocumentLogsByMultiCriteriaResponse response = new GetDocumentLogsByMultiCriteriaResponse
                                                                  {
                                                                      AllDocumentLogs =
                                                                          new DataContracts.DocumentLogList(),
                                                                      PaginationInfo = new PaginationResponse()
                                                                  };

            // obter os registos de processamento
            Eresults.Common.WCF.BusinessEntities.DocumentLogList logList =
                MonitoringLogic.GetDocumentLogsByMultiCriteria(
                    request.CompanyDb,request.SearchCriteria.PatTypeId,request.SearchCriteria.PatId,
                    request.SearchCriteria.EpiTypeId,request.SearchCriteria.EpiId,request.SearchCriteria.InstId,
                    request.SearchCriteria.PlaceId,request.SearchCriteria.AppId,request.SearchCriteria.DocTypeId,
                    request.SearchCriteria.Doc,request.SearchCriteria.DocDateBegin,request.SearchCriteria.DocDateEnd,
                    request.SearchCriteria.ArrivDateBegin,request.SearchCriteria.ArrivDateEnd,
                    request.SearchCriteria.ProcDateBegin,request.SearchCriteria.ProcDateEnd,
                    request.SearchCriteria.StatusId, request.SearchCriteria.AlertId, showHistory, request.PaginationInfo.PageNumber,
                    request.PaginationInfo.ItemsPerPage, request.PaginationInfo.OrderField,orderType);

            DocumentLogs docLogs = new DocumentLogs();
            docLogs.AddRange(logList.Items.Select(TranslateBetweenDocumentLogBeAndDocumentLogDc.TranslateDocumentLogToDocumentLog));

            response.AllDocumentLogs.DocumentLogs = docLogs;

            // obter a informação de paginação (apenas se for enviada informação de paginação)
            if (request.PaginationInfo != null)
            {
                if (logList.Items != null && logList.Items.Count > 0)
                {
                    response.PaginationInfo.TotalNumber = logList.Items[0].totalNumber;
                }

            }

            return response;
		}
        
        public override GetAlertsResponse GetAlerts(GetAlertsRequest request)
        {
            string orderField = null;
            string orderType = null;
            string showHistory = null;
            if (request.PaginationInfo != null)
            {
                orderField = ConvertOrderField(request.PaginationInfo.OrderField);
                orderType = ConvertOrderType(request.PaginationInfo.OrderType);
            }
            else
            {
                request.PaginationInfo = new PaginationRequest();
            }

            if (request.SearchCriteria != null)
            {
                showHistory = ConvertShowHistory(request.SearchCriteria.ShowHistory);
            }
            else
            {
                request.SearchCriteria = new AlertListSearchCriteria();
            }

            // preparar o objecto de resposta
            GetAlertsResponse response = new GetAlertsResponse
                                             {
                                                 AllAlerts = new DataContracts.AlertList(),
                                                 PaginationInfo = new PaginationResponse()
                                             };

            // obter os registos de processamento
            Eresults.Common.WCF.BusinessEntities.AlertList alertList =
                MonitoringLogic.GetAlertsByMultiCriteria(
                    request.CompanyDb, request.SearchCriteria.InstId, request.SearchCriteria.PlaceId, request.SearchCriteria.AppId,
                    request.SearchCriteria.DocTypeId, request.SearchCriteria.DateBegin, request.SearchCriteria.DateEnd,
                    request.SearchCriteria.StatusId, request.SearchCriteria.AlertTypeId, showHistory, request.PaginationInfo.PageNumber,
                    request.PaginationInfo.ItemsPerPage, orderField, orderType
            );

            Alerts alertsList = new Alerts();
            alertsList.AddRange(alertList.Items.Select(TranslateBetweenAlertBeAndAlertDc.TranslateAlertToAlert));

            response.AllAlerts.Alerts = alertsList;

            // obter a informação de paginação (apenas se for enviada informação de paginação)
            if (request.PaginationInfo != null)
            {
                if (alertList.Items != null && alertList.Items.Count > 0)
                {
                    response.PaginationInfo.TotalNumber = alertList.Items[0].totalNumber;
                }

            }

            return response;
        }

        public override GetAlertsSubscriptionsResponse GetAlertsSubscriptions(GetAlertsSubscriptionsRequest request)
        {
            GetAlertsSubscriptionsResponse response = new GetAlertsSubscriptionsResponse();
            Eresults.Common.WCF.BusinessEntities.AlertList alerts = MonitoringLogic.GetAlertsSubscriptions(request.CompanyDb, request.UserId);
            Alerts alertsDClist = new Alerts();
            if (alerts != null && alerts.Count != 0)
            {
                alertsDClist.AddRange(alerts.Items.Select(TranslateBetweenAlertBeAndAlertDc.TranslateAlertToAlert));
            }
            response.AlertSubscriptions = new DataContracts.AlertList {Alerts = alertsDClist};
            return response;
        }

        public override IgnoredAlertResponse IgnoredAlert(IgnoredAlertRequest request)
        {
            try
            {
                MonitoringLogic.IgnoredAlert(request.CompanyDb, request.AlertId);
                return new IgnoredAlertResponse() { OperationSuccess = true };
            }
            catch
            {
                return new IgnoredAlertResponse() { OperationSuccess = false };
            }
        }

        public override CorrectedAlertResponse CorrectedAlert(CorrectedAlertRequest request)
        {
            try
            {
                MonitoringLogic.CorrectedAlert(request.CompanyDb, request.AlertId);
                return new CorrectedAlertResponse() { OperationSuccess = true };
            }
            catch
            {
                return new CorrectedAlertResponse() { OperationSuccess = false };
            }
        }

        public override ProcessIndexDocumentResponse ProcessIndexDocument(ProcessIndexDocumentRequest request)
        {
            try
            {
                MonitoringLogic.ProcessIndexDocument(request.CompanyDb, request.IndexId);
                return new ProcessIndexDocumentResponse() { OperationSuccess = true };
            }
            catch
            {
                return new ProcessIndexDocumentResponse() { OperationSuccess = false };
            }
        }

        private string ConvertOrderField(string orderField)
        {
            switch (orderField)
            {
                case "Instituição":
                    return "INSTS.DESCRICAO";
                case "Local":
                    return "PALCES.DESCRICAO";
                case "Aplicação":
                    return "APPS.DESCRICAO";
                case "T. Documento":
                    return "DOCTYPES.DESCRICAO";
                case "Id. Documento":
                    return "DOCLOGS.DOCUMENTO";
                case "Data Documento":
                    return "DOCLOGS.DATA_DOC";
                case "Data Chegada":
                    return "DOCLOGS.DATA_CHEG";
                case "Data Proc.":
                    return "DOCLOGS.DATA_PROC";
                case "Estado":
                    return "STATUS.DESCRICAO";
                default:
                    return orderField;
            }
        }

        private string ConvertOrderType(OrderTypeEnum orderType)
        {
            switch (orderType)
            {
                case OrderTypeEnum.ASC:
                    return "ASC";
                case OrderTypeEnum.DESC:
                    return "DESC";
                default:
                    return null;
            }
        }

        private string ConvertShowHistory(bool showHistory)
        {
            return showHistory ? "S" : "N";
        }

        public override UpdateAlertsSubscriptionsResponse UpdateAlertsSubscriptions(UpdateAlertsSubscriptionsRequest request)
        {
            try
            {
                List<AlertSubscription> alertsSubsList = (from a in request.AlertsList.Alerts select TranslateBetweenAlertBeAndAlertDc.TranslateAlertToAlert(a) into alert where alert != null && alert.AlertSubscription != null select alert.AlertSubscription).ToList();
                UpdateAlertsSubscriptionsResponse resp = new UpdateAlertsSubscriptionsResponse();
                string s = MonitoringLogic.UpdateAlertSubscriptions(request.CompanyDb, alertsSubsList);
                resp.OperationSuccess = s;
                return resp;
            }
            catch
            {
                return new UpdateAlertsSubscriptionsResponse { OperationSuccess = "N" };
            }
        }
    }
}