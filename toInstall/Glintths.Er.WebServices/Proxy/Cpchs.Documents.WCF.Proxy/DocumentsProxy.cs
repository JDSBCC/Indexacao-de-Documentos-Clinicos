using System;
using System.Configuration;
using System.Globalization;
using Cpchs.Documents.WCF.DataContracts;
using Cpchs.Documents.WCF.Proxy.DocumentsManagementSR;
using Cpchs.Security.Providers.Utilities;

namespace Cpchs.Documents.WCF.Proxy
{
    public sealed class DocumentsProxy
    {
        private readonly string _endpoint;
        private readonly DocumentsManagementSCClient _client;

        public string CompanyDbName
        {
            get { return SecurityContext.Instance.CurrentApplicationName; }
        }

        public DocumentsProxy()
        {
            _endpoint = WebServiceContext.Instance.GetWebServiceUrl(CompanyDbName,ToString());
            _client = new DocumentsManagementSCClient(_endpoint);
        }

        public DocumentsProxy(string companyName)
        {
            SecurityContext.Instance.CurrentApplication = new Common.Security.Application(companyName);
            _endpoint = WebServiceContext.Instance.GetWebServiceUrl(CompanyDbName,ToString());
            _client = new DocumentsManagementSCClient(_endpoint);
        }

        //public DataContracts.Documents GetDocumentsByMultiCriteria(
        //    string patEntIds,
        //    string epiTypeCode,
        //    string epiId,
        //    DateTime? epiDateBegin,
        //    DateTime? epiDateEnd,
        //    string docId,
        //    string docTypeCode,
        //    string appCode,
        //    string placeCode,
        //    string instCode,
        //    DateTime? execDateBegin,
        //    DateTime? execDateEnd,
        //    DateTime? valDateBegin,
        //    DateTime? valDateEnd,
        //    DateTime? emiDateBegin,
        //    DateTime? emiDateEnd,
        //    string extId,
        //    bool? myPublic,
        //    bool? report,
        //    string reqServCode,
        //    string execServCode,
        //    string globalFilters,
        //    string docsSessionFilters,
        //    string servsSessionFilters)
        //{
        //    string companyDb = SecurityContext.Instance.CurrentApplicationName;
        //    string userName = SecurityContext.Instance.CurrentMembershipUser.UserName;

        //    PaginationResponse pagResp;
        //    DocumentsList docList = _client.GetDocumentsByMultiCriteria(appCode, companyDb, docsSessionFilters, docId, docTypeCode,
        //                                                                emiDateEnd, emiDateBegin, patEntIds, epiDateEnd, epiId, epiDateBegin, epiTypeCode, execServCode,
        //                                                                execDateEnd, execDateBegin, extId, globalFilters, instCode, new PaginationRequest(), placeCode, myPublic,
        //                                                                report, reqServCode, servsSessionFilters, userName, valDateEnd, valDateBegin, out pagResp);
        //    _client.Close();
        //    return docList.Documents;
        //}

        //public DataContracts.Documents GetDocumentsByMultiCriteriaV2(
        //    string patEntIds,
        //    string epiTypeCode,
        //    string epiId,
        //    DateTime? epiDateBegin,
        //    DateTime? epiDateEnd,
        //    string docId,
        //    string docTypeCode,
        //    string appCode,
        //    string placeCode,
        //    string instCode,
        //    DateTime? execDateBegin,
        //    DateTime? execDateEnd,
        //    DateTime? valDateBegin,
        //    DateTime? valDateEnd,
        //    DateTime? emiDateBegin,
        //    DateTime? emiDateEnd,
        //    string extId,
        //    bool? myPublic,
        //    bool? report,
        //    string reqServCode,
        //    string execServCode,
        //    string globalFilters,
        //    string docsSessionFilters,
        //    string servsSessionFilters,
        //    long? workspaceId)
        //{
        //    string companyDb = SecurityContext.Instance.CurrentApplicationName;
        //    string userName = SecurityContext.Instance.CurrentMembershipUser.UserName;
        //    PaginationResponse pagResp;
        //    DocumentsList docList = _client.GetDocumentsByMultiCriteriaV2(appCode, companyDb, docsSessionFilters, docId, docTypeCode,
        //                                                                  emiDateEnd, emiDateBegin, patEntIds, epiDateEnd, epiId, epiDateBegin, epiTypeCode, execServCode,
        //                                                                  execDateEnd, execDateBegin, extId, globalFilters, instCode, new PaginationRequest(), placeCode, myPublic,
        //                                                                  report, reqServCode, servsSessionFilters, userName, valDateEnd, valDateBegin, workspaceId, out pagResp);
        //    _client.Close();
        //    return docList.Documents;
        //}

        /*public DataContracts.Documents GetPatientDocuments(
            string patientType,
            string patientId,
            string episodeType,
            string episodeId,
            string institution,
            string place,
            string application,
            string documentType)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            string userName = SecurityContext.Instance.CurrentMembershipUser.UserName;
            PaginationResponse pagResp;
            DocumentsList docList = _client.GetPatientDocuments(
                application,
                companyDb,
                documentType,
                episodeId,
                episodeType,
                institution,
                patientId,
                patientType,
                place,
                userName,
                out pagResp);
            _client.Close();
            return docList.Documents;
        }*/

        /*public DataContracts.PatientDocuments GetPatientDocuments(
                    long? patEntId,
                    long? tEpisodio,
                    string episodeId,
                    DateTime? epiDateBegin,
                    DateTime? epiDateEnd,
                    string doc,
                    DateTime? docDateBegin,
                    DateTime? docDateEnd,
                    string extId,
                    long? docType,
                    long? appId,
                    long? localId,
                    long? instId,
                    long? reqServId,
                    DateTime? valDateBegin,
                    DateTime? valDateEnd,
                    string globalFilters,
                    string docsSessionFilters,
                    string servsSessionFilters)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            string userName = SecurityContext.Instance.CurrentMembershipUser.UserName;
            bool userAnaRes;
            try
            {
                string anaResRoleKey = ConfigurationManager.AppSettings["AnaResRoleKey"].ToString(CultureInfo.InvariantCulture);
                userAnaRes = SecurityContext.Instance.IsUserInRole(anaResRoleKey);
            }
            catch
            {
                userAnaRes = true;
            }
            GetAllPatientDocumentsRequest request = new GetAllPatientDocumentsRequest
                                                        {
                                                            CompanyDb = companyDb,
                                                            PatEntId = patEntId,
                                                            EpisodeTypeId = tEpisodio,
                                                            EpisodeId = episodeId,
                                                            EpiDateBegin = epiDateBegin,
                                                            EpiDateEnd = epiDateEnd,
                                                            Doc = doc,
                                                            DocDateBegin = docDateBegin,
                                                            DocDateEnd = docDateEnd,
                                                            ExtId = extId,
                                                            DocType = docType,
                                                            AppId = appId,
                                                            LocalId = localId,
                                                            InstId = instId,
                                                            ReqService = reqServId,
                                                            ValDateBegin = valDateBegin,
                                                            ValDateEnd = valDateEnd,
                                                            GlobalFilters = globalFilters,
                                                            DocsSessionFilters = docsSessionFilters,
                                                            ServsSessionFilters = servsSessionFilters,
                                                            UserName = userName,
                                                            UserAnaRes = userAnaRes
                                                        };
            PatientDocumentList docList = _client.GetAllPatientDocuments(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        /*public DataContracts.PatientDocuments GetPatientDocuments(
                    long? patEntId,
                    long? tEpisodio,
                    string episodeId,
                    DateTime? epiDateBegin,
                    DateTime? epiDateEnd,
                    string doc,
                    DateTime? docDateBegin,
                    DateTime? docDateEnd,
                    string extId,
                    long? docType,
                    long? appId,
                    long? localId,
                    long? instId,
                    long? reqServId,
                    DateTime? valDateBegin,
                    DateTime? valDateEnd,
                    string globalFilters,
                    string docsSessionFilters,
                    string servsSessionFilters,
                    string userName,
                    string companyDb)
        {
            bool userAnaRes;
            try
            {
                string anaResRoleKey = ConfigurationManager.AppSettings["AnaResRoleKey"].ToString(CultureInfo.InvariantCulture);
                userAnaRes = SecurityContext.Instance.IsUserInRole(anaResRoleKey);
            }
            catch
            {
                userAnaRes = true;
            }
            GetAllPatientDocumentsRequest request = new GetAllPatientDocumentsRequest
                                                        {
                                                            CompanyDb = companyDb,
                                                            PatEntId = patEntId,
                                                            EpisodeTypeId = tEpisodio,
                                                            EpisodeId = episodeId,
                                                            EpiDateBegin = epiDateBegin,
                                                            EpiDateEnd = epiDateEnd,
                                                            Doc = doc,
                                                            DocDateBegin = docDateBegin,
                                                            DocDateEnd = docDateEnd,
                                                            ExtId = extId,
                                                            DocType = docType,
                                                            AppId = appId,
                                                            LocalId = localId,
                                                            InstId = instId,
                                                            ReqService = reqServId,
                                                            ValDateBegin = valDateBegin,
                                                            ValDateEnd = valDateEnd,
                                                            GlobalFilters = globalFilters,
                                                            DocsSessionFilters = docsSessionFilters,
                                                            ServsSessionFilters = servsSessionFilters,
                                                            UserName = userName,
                                                            UserAnaRes = userAnaRes
                                                        };
            PatientDocumentList docList = _client.GetAllPatientDocuments(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        /*public DataContracts.PatientDocuments GetPatientDocumentsMulti(
                    string patEntIds,
                    long? tEpisodio,
                    string episodeId,
                    DateTime? epiDateBegin,
                    DateTime? epiDateEnd,
                    string doc,
                    DateTime? docDateBegin,
                    DateTime? docDateEnd,
                    string extId,
                    long? docType,
                    long? appId,
                    long? localId,
                    long? instId,
                    long? reqServId,
                    DateTime? valDateBegin,
                    DateTime? valDateEnd,
                    string globalFilters,
                    string docsSessionFilters,
                    string servsSessionFilters)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            string userName = SecurityContext.Instance.CurrentMembershipUser.UserName;
            bool userAnaRes;
            try
            {
                string anaResRoleKey = ConfigurationManager.AppSettings["AnaResRoleKey"].ToString(CultureInfo.InvariantCulture);
                userAnaRes = SecurityContext.Instance.IsUserInRole(anaResRoleKey);
            }
            catch
            {
                userAnaRes = true;
            }
            GetAllPatientDocumentsMultiRequest request = new GetAllPatientDocumentsMultiRequest
                                                             {
                                                                 CompanyDb = companyDb,
                                                                 PatEntIds = patEntIds,
                                                                 EpisodeTypeId = tEpisodio,
                                                                 EpisodeId = episodeId,
                                                                 EpiDateBegin = epiDateBegin,
                                                                 EpiDateEnd = epiDateEnd,
                                                                 Doc = doc,
                                                                 DocDateBegin = docDateBegin,
                                                                 DocDateEnd = docDateEnd,
                                                                 ExtId = extId,
                                                                 DocType = docType,
                                                                 AppId = appId,
                                                                 LocalId = localId,
                                                                 InstId = instId,
                                                                 ReqService = reqServId,
                                                                 ValDateBegin = valDateBegin,
                                                                 ValDateEnd = valDateEnd,
                                                                 GlobalFilters = globalFilters,
                                                                 DocsSessionFilters = docsSessionFilters,
                                                                 ServsSessionFilters = servsSessionFilters,
                                                                 UserName = userName,
                                                                 UserAnaRes = userAnaRes
                                                             };
            PatientDocumentList docList = _client.GetAllPatientDocumentsMulti(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        /*public DataContracts.PatientDocuments GetPatientDocumentsMulti(
                    string patEntIds,
                    long? tEpisodio,
                    string episodeId,
                    DateTime? epiDateBegin,
                    DateTime? epiDateEnd,
                    string doc,
                    DateTime? docDateBegin,
                    DateTime? docDateEnd,
                    string extId,
                    long? docType,
                    long? appId,
                    long? localId,
                    long? instId,
                    long? reqServId,
                    DateTime? valDateBegin,
                    DateTime? valDateEnd,
                    string globalFilters,
                    string docsSessionFilters,
                    string servsSessionFilters,
                    string userName,
                    string companyDb)
        {
            bool userAnaRes;
            try
            {
                string anaResRoleKey = ConfigurationManager.AppSettings["AnaResRoleKey"].ToString(CultureInfo.InvariantCulture);
                userAnaRes = SecurityContext.Instance.IsUserInRole(anaResRoleKey);
            }
            catch
            {
                userAnaRes = true; 
            }
            GetAllPatientDocumentsMultiRequest request = new GetAllPatientDocumentsMultiRequest
                                                             {
                                                                 CompanyDb = companyDb,
                                                                 PatEntIds = patEntIds,
                                                                 EpisodeTypeId = tEpisodio,
                                                                 EpisodeId = episodeId,
                                                                 EpiDateBegin = epiDateBegin,
                                                                 EpiDateEnd = epiDateEnd,
                                                                 Doc = doc,
                                                                 DocDateBegin = docDateBegin,
                                                                 DocDateEnd = docDateEnd,
                                                                 ExtId = extId,
                                                                 DocType = docType,
                                                                 AppId = appId,
                                                                 LocalId = localId,
                                                                 InstId = instId,
                                                                 ReqService = reqServId,
                                                                 ValDateBegin = valDateBegin,
                                                                 ValDateEnd = valDateEnd,
                                                                 GlobalFilters = globalFilters,
                                                                 DocsSessionFilters = docsSessionFilters,
                                                                 ServsSessionFilters = servsSessionFilters,
                                                                 UserName = userName,
                                                                 UserAnaRes = userAnaRes
                                                             };
            PatientDocumentList docList = _client.GetAllPatientDocumentsMulti(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        /*public DataContracts.PatientDocuments GetDocumentsByExternalId(
                    string doc,
                    long appId,
                    long localId,
                    long instId)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            GetDocumentsByExternalIdRequest request = new GetDocumentsByExternalIdRequest
                                                          {
                                                              CompanyDb = companyDb,
                                                              Doc = doc,
                                                              AppId = appId,
                                                              LocalId = localId,
                                                              InstId = instId
                                                          };
            PatientDocumentList docList = _client.GetDocumentsByExternalId(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        /*public DataContracts.PatientDocuments GetDocumentsByExternalIdV2(string doc, long docTypeId, long appId, long placeId, long instId)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            GetDocumentsByExternalIdV2Request request = new GetDocumentsByExternalIdV2Request
                                                            {
                                                                CompanyDb = companyDb,
                                                                Document = doc,
                                                                DocumentTypeId = docTypeId,
                                                                ApplicationId = appId,
                                                                PlaceId = placeId,
                                                                InstitutionId = instId
                                                            };
            PatientDocumentList docList = _client.GetDocumentsByExternalIdV2(request);
            _client.Close();
            return docList.PatientDocuments;
        }*/

        public void RegisterDocumentAccess(
            string sessionId, 
            string userName, 
            long docId, 
            string docRef, 
            long artifactId, 
            long versionId, 
            long appOrigin,
            long docType
            )
        {
            DocumentAccess da = new DocumentAccess
            {
                SessionId = sessionId,
                UserName = userName,
                DocId = docId,
                DocRef = docRef,
                ArtifactId = artifactId,
                VersionId = versionId,
                AppOrigin = appOrigin,
                DocType = docType
            };
            _client.RegisterDocumentAccess(
                new RegisterDocumentAccessRequest()
                {
                    CompanyDb = CompanyDbName, 
                    DocumentAccess = da 
                });
        }

        /*public DataContracts.Videos GetDocumentVideos(long docId)
        {
            GetDocumentVideosRequest req = new GetDocumentVideosRequest
                                               {
                CompanyDB = CompanyDbName,
                DocId = docId
            };
            _client.Close();
            return _client.GetDocumentVideos(req).Videos;
        }*/
    }
}