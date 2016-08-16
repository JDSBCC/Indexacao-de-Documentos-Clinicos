using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using Cpchs.Documents.WCF.BusinessLogic;
using Cpchs.Documents.WCF.DataContracts;
using Cpchs.Documents.WCF.FaultContracts;
using Cpchs.Documents.WCF.MessageContracts;
using Cpchs.Entities.WCF.DataContracts;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Document = Cpchs.Documents.WCF.DataContracts.Document;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public partial class DocumentsManagementWS
    {

        public override ConvertPdfToImagesResponse ConvertPdfToImages(ConvertPdfToImagesRequest request)
        {
            ConvertPdfToImagesResponse response = new ConvertPdfToImagesResponse();
            response.Images = new List<string>();

            try
            {
                string pdfUrl = request.RequestUrl;

                if (!string.IsNullOrEmpty(pdfUrl))
                {
                    

                    string query = pdfUrl.Substring(pdfUrl.LastIndexOf("query="));
                    query = query.Replace("query=", "");
                    query = Utils.EncryptionUtil.Decrypt(query);
                    string[] queryBroken = query.Split(new char[] { '&'});
                    string folderName = string.Empty;
                    foreach (string q in queryBroken)
                    {
                        if (q.StartsWith("docId"))
                            folderName = q + folderName;

                        else if (q.StartsWith("elemId"))
                            folderName = folderName + q;
                    }

                    string swfFullPath = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_TOOL_SWF"];
                    string pdfFullPath = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_TOOL_PDF"];
                    string tempPath = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_TEMP_PATH"];
                    string baseDefinitiveUrl = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_DEFINITIVE_BASEURL"];
                    string swf_command = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_CMD_SWF"];
                    string png_command = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_CMD_PNG"];
                    string from_extension = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_FROM_EXT"];
                    string to_rule_extension = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_TO_RULE_EXT"];
                    string to_extension = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_TO_EXT"];
                    string docache = System.Configuration.ConfigurationManager.AppSettings["CONVERSION_CACHE"];
                    long cacheMinutes = 10;

                    if (!string.IsNullOrEmpty(swfFullPath) &&
                       !string.IsNullOrEmpty(pdfFullPath) &&
                       !string.IsNullOrEmpty(tempPath) &&
                       !string.IsNullOrEmpty(baseDefinitiveUrl))
                    {
                        if (string.IsNullOrEmpty(docache))
                        {
                            cacheMinutes = 10;
                        }
                        else
                        {
                            try{
                                if(!long.TryParse(docache, out cacheMinutes))
                                    cacheMinutes = 10;
                            }
                            catch(Exception e)
                            {
                                cacheMinutes = 10;
                            }
                        }

                        if(string.IsNullOrEmpty(swf_command)){
                            swf_command = "{0} -o {1} -f -T 9 -t -s storeallcharacters";
                        }

                        if (string.IsNullOrEmpty(png_command))
                        {
                            png_command = "{0} -o {1} -s keepaspectratio";
                        }

                        if (string.IsNullOrEmpty(from_extension))
                        {
                            from_extension = ".pdf";
                        }
                        if (string.IsNullOrEmpty(to_rule_extension))
                        {
                            to_rule_extension = "file_%.swf";
                        }

                        if (string.IsNullOrEmpty(to_extension))
                        {
                            to_extension = ".png";
                        }

                        folderName = folderName.Replace('=', '-');
                        string folderNamePlusTimeStamp = folderName + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                        string tempFullFolder = System.IO.Path.Combine(tempPath, folderNamePlusTimeStamp);
                        string tempFullFilename = System.IO.Path.Combine(tempFullFolder, "file" + from_extension);

                        if(cacheMinutes >= 0){
                            string[] existentFolders = System.IO.Directory.GetDirectories(tempPath, folderName + "*");
                            if (existentFolders.Length != 0)
                            {
                                foreach (string ef in existentFolders)
                                {
                                    string dirname = System.IO.Path.GetFileName(ef);
                                    string efv = dirname.Substring(dirname.IndexOf('_')); 
                                    efv = efv.Replace("_", "");

                                    DateTime dateCreated = DateTime.ParseExact(efv, "ddMMyyyyHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                                    if (DateTime.Now.Subtract(dateCreated).TotalMinutes < cacheMinutes)
                                    {
                                        folderNamePlusTimeStamp = dirname;
                                        tempFullFolder = System.IO.Path.Combine(tempPath, dirname);
                                        tempFullFilename = System.IO.Path.Combine(tempFullFolder, "file" + from_extension);
                                    }
                                    else
                                    {
                                        System.IO.Directory.Delete(ef, true);
                                    }
                                }
                            }
                        }

                        if (!System.IO.Directory.Exists(tempFullFolder))
                        {
                            System.IO.Directory.CreateDirectory(tempFullFolder);
                        }

                        if (!System.IO.File.Exists(tempFullFilename))
                        {
                            WebClient wc = new WebClient();
                            wc.DownloadFile(pdfUrl, tempFullFilename);
                        }

                        string existentPage = System.IO.Path.Combine(tempFullFolder, to_rule_extension.Replace("%", request.Page.ToString()));
                        if (!System.IO.File.Exists(existentPage))
                        {
                            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                            startInfo.CreateNoWindow = false;
                            startInfo.UseShellExecute = false;
                            startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.FileName = pdfFullPath;
                            startInfo.Arguments = tempFullFilename + " "+ System.IO.Path.Combine(tempFullFolder, to_rule_extension);
                            try
                            {
                                using (System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start(startInfo))
                                {
                                    exeProcess.WaitForExit(600000);
                                }
                            }
                            catch
                            {
                            }
                        }

                        string existentPageImage = System.IO.Path.Combine(tempFullFolder, request.Page.ToString() + to_extension );

                        int count = System.IO.Directory.GetFiles(tempFullFolder, "*.swf").Length;
                        if (!System.IO.File.Exists(existentPageImage))
                        {
                            System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo();
                            si.CreateNoWindow = false;
                            si.UseShellExecute = false;
                            si.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            si.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            si.FileName = swfFullPath;
                            si.Arguments = existentPage + " " +existentPageImage;
                            try
                            {
                                using (System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start(si))
                                {
                                    exeProcess.WaitForExit(600000);
                                }

                                if(System.IO.File.Exists(existentPageImage))
                                    response.Images.Add(baseDefinitiveUrl + "/" + folderNamePlusTimeStamp + "/" + request.Page.ToString() + to_extension);
                            }
                            catch
                            {
                            }
                        }
                        else
                            response.Images.Add(baseDefinitiveUrl + "/" + folderNamePlusTimeStamp +"/"+ request.Page.ToString() + to_extension);

                        response.TotalPages = count;
                        response.CurrentPage = request.Page;
                    }
                }
            }
            catch(Exception e)
            {
            }

            return response;
        }


        public override UpdateElementReportInfoResponse UpdateElementReportInfo(UpdateElementReportInfoRequest request)
        {
            UpdateElementReportInfoResponse response = new UpdateElementReportInfoResponse { Success = true };
            try
            {
                ElementList list = new ElementList();
                if (request.ElementsList != null && request.ElementsList.Count > 0)
                {
                    foreach (ElementInfoForReport elem in request.ElementsList)
                    {
                        list.Add(TranslateBetweenElementBeAndElementDc.TranslateElementToElement(elem));
                    }
                }
                DocumentLogic.UpdateElementReportInfo(request.CompanyDb, list);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        public override CancelDocumentResponse CancelDocument(CancelDocumentRequest request)
        {
            CancelDocumentResponse response = new CancelDocumentResponse { Success = true };
            try
            {
                DocumentLogic.CancelDocumentByUniqueId(request.CompanyDb, request.DocumentUniqueId);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        public override CancelElementResponse CancelElement(CancelElementRequest request)
        {
            CancelElementResponse response = new CancelElementResponse { Success = true };
            try
            {
                DocumentLogic.CancelElement(request.CompanyDb, request.ElementId);
            }
            catch
            {
                response.Success = false;
            }
            return response;
        }

        public override CancelDocumentPermanentlyResponse CancelDocumentPermanently(CancelDocumentPermanentlyRequest request)
        {
            CancelDocumentPermanentlyResponse resp = new CancelDocumentPermanentlyResponse();
            try
            {
                DocumentManagementBER.Instance.CancelDocument(
                    request.CompanyDb,
                    request.Institution,
                    request.Place,
                    request.Application,
                    request.DocumentType,
                    request.DocumentExternalId
                );
                resp.OperationSuccess = true;
            }
            catch
            {
                resp.OperationSuccess = false;
            }
            return resp;
        }

        public override CancelDocumentPermanentlyResponseV2 CancelDocumentPermanentlyV2(CancelDocumentPermanentlyRequestV2 request)
        {
            CancelDocumentPermanentlyResponseV2 resp = new CancelDocumentPermanentlyResponseV2();
            try
            {
                DocumentManagementBER.Instance.CancelDocumentV2(
                    request.CompanyDb,
                    request.Institution,
                    request.Place,
                    request.Application,
                    request.DocumentType,
                    request.DocumentExternalId,
                    request.Observations,
                    request.JustificationId,
                    request.Username
                );
                resp.OperationSuccess = true;
            }
            catch
            {
                resp.OperationSuccess = false;
            }
            return resp;
        }

        public override CancelDocumentLastVersionResponse CancelDocumentLastVersion(CancelDocumentLastVersionRequest request)
        {
            CancelDocumentLastVersionResponse resp = new CancelDocumentLastVersionResponse();
            try
            {
                DocumentManagementBER.Instance.CancelDocumentVersion(
                    request.CompanyDb,
                    request.Institution,
                    request.Place,
                    request.Application,
                    request.DocumentType,
                    request.DocumentExternalId
                );
                resp.OperationSuccess = true;
            }
            catch
            {
                resp.OperationSuccess = false;
            }
            return resp;
        }

        public override CancelDocumentLastVersionResponseV2 CancelDocumentLastVersionV2(CancelDocumentLastVersionRequestV2 request)
        {
            CancelDocumentLastVersionResponseV2 resp = new CancelDocumentLastVersionResponseV2();
            try
            {
                DocumentManagementBER.Instance.CancelDocumentVersionV2(
                    request.CompanyDb,
                    request.Institution,
                    request.Place,
                    request.Application,
                    request.DocumentType,
                    request.DocumentExternalId,
                    request.Observations,
                    request.JustificationId,
                    request.Username
                );
                resp.OperationSuccess = true;
            }
            catch
            {
                resp.OperationSuccess = false;
            }
            return resp;
        }

        public override GetDocTypeDescResponse GetDocTypeDesc(GetDocTypeDescRequest request)
        {
            GetDocTypeDescResponse res = new GetDocTypeDescResponse
                                             {
                                                 DocTypeDesc = DocumentLogic.GetDocTypeDesc(
                                                     request.CompanyDb,
                                                     request.InstCode,
                                                     request.PlaceCode,
                                                     request.AppCode,
                                                     request.DocTypeCode)
                                             };
            return res;
        }

        private static string ConvertOrderField(string orderField)
        {
            switch (orderField)
            {
                default:
                    return null;
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

        //USADO PELO EPRMOBILE
        public override GetDocumentsForEPRResponse GetDocumentsForEPR(GetDocumentsForEPRRequest r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogicEPR.GetDocumentsForEPR(r.CompanyDb, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType, r.Period, r.EResultsVersion, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value : 10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue)
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                        documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocumentForEPR(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetDocumentsForEPRResponse response = new GetDocumentsForEPRResponse { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                {
                    Description =
                        "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }
        
        public override GetDocumentsByMultiCriteriaV2Response GetDocumentsByMultiCriteriaV2(GetDocumentsByMultiCriteriaV2Request r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogic.GetDocumentsByMultiCriteriaV2(r.CompanyDb, r.EntitiesIds, r.ReqService, r.ExecService, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType,
                    r.EpisodeStartDate, r.EpisodeEndDate, r.DocumentId, r.ExternalId, r.DocumentType, r.Application, r.Place, r.Institution, r.ExecutionStartDate,
                    r.ExecutionEndDate, r.ValidationStartDate, r.ValidationEndDate, r.EmissionStartDate, r.EmissionEndDate, r.GlobalFilters, r.DocsSessionFilters,
                    r.ServsSessionFilters, r.Username, r.Report, r.Public, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType,
                    r.WorkspaceId, r.FilterString, r.Period, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value:10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue )
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                            documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetDocumentsByMultiCriteriaV2Response response = new GetDocumentsByMultiCriteriaV2Response { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                                          {
                                              Description =
                                                  "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                                          };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetDocumentsByMultiCriteriaV2Response GetDocumentsByMultiCriteriaV3(GetDocumentsByMultiCriteriaV2Request r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogic.GetDocumentsByMultiCriteriaV3(r.CompanyDb, r.EntitiesIds, r.ReqService, r.ExecService, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType,
                    r.EpisodeStartDate, r.EpisodeEndDate, r.DocumentId, r.ExternalId, r.DocumentType, r.Application, r.Place, r.Institution, r.ExecutionStartDate,
                    r.ExecutionEndDate, r.ValidationStartDate, r.ValidationEndDate, r.EmissionStartDate, r.EmissionEndDate, r.GlobalFilters, r.DocsSessionFilters,
                    r.ServsSessionFilters, r.Username, r.Report, r.Public, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType,
                    r.WorkspaceId, r.FilterString, r.Period, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value : 10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue)
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                        documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetDocumentsByMultiCriteriaV2Response response = new GetDocumentsByMultiCriteriaV2Response { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                {
                    Description =
                        "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetDocumentsByMultiCriteriaV2Response GetDocumentsToCancel(GetDocumentsByMultiCriteriaV2Request r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogic.GetDocumentsToCancel(r.CompanyDb, r.EntitiesIds, r.ReqService, r.ExecService, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType,
                    r.EpisodeStartDate, r.EpisodeEndDate, r.DocumentId, r.ExternalId, r.DocumentType, r.Application, r.Place, r.Institution, r.ExecutionStartDate,
                    r.ExecutionEndDate, r.ValidationStartDate, r.ValidationEndDate, r.EmissionStartDate, r.EmissionEndDate, r.GlobalFilters, r.DocsSessionFilters,
                    r.ServsSessionFilters, r.Username, r.Report, r.Public, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType,
                    r.WorkspaceId, r.FilterString, r.Period, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value : 10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue)
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                        documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetDocumentsByMultiCriteriaV2Response response = new GetDocumentsByMultiCriteriaV2Response { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                {
                    Description =
                        "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetGeneralDocumentsResponse GetGeneralDocuments(GetGeneralDocumentsRequest r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogic.GetGeneralDocuments(r.CompanyDb, r.EntitiesIds, r.ReqService, r.ExecService, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType,
                    r.EpisodeStartDate, r.EpisodeEndDate, r.DocumentId, r.ExternalId, r.DocumentType, r.Application, r.Place, r.Institution, r.ExecutionStartDate,
                    r.ExecutionEndDate, r.ValidationStartDate, r.ValidationEndDate, r.EmissionStartDate, r.EmissionEndDate, r.GlobalFilters, r.DocsSessionFilters,
                    r.ServsSessionFilters, r.Username, r.Report, r.Public, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType,
                    r.WorkspaceId, r.FilterString, r.Period, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value : 10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue)
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                        documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetGeneralDocumentsResponse response = new GetGeneralDocumentsResponse { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                {
                    Description =
                        "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetGeneralDocumentsResponse GetCancelledDocuments(GetGeneralDocumentsRequest r)
        {
            try
            {
                string orderField = null;
                string orderType = null;
                long? resultsCount = null;
                if (r.PaginationInfo != null)
                {
                    orderField = ConvertOrderField(r.PaginationInfo.OrderField);
                    orderType = ConvertOrderType(r.PaginationInfo.OrderType);
                    resultsCount = r.PaginationInfo.ResultsCount;
                }
                else
                {
                    r.PaginationInfo = new PaginationRequest();
                }

                DocumentList docList = DocumentLogic.GetCancelledDocuments(r.CompanyDb, r.EntitiesIds, r.ReqService, r.ExecService, r.EpisodeType, r.EpisodeId, r.PatientId, r.PatientType,
                    r.EpisodeStartDate, r.EpisodeEndDate, r.DocumentId, r.ExternalId, r.DocumentType, r.Application, r.Place, r.Institution, r.ExecutionStartDate,
                    r.ExecutionEndDate, r.ValidationStartDate, r.ValidationEndDate, r.EmissionStartDate, r.EmissionEndDate, r.CancelDate, r.GlobalFilters, r.DocsSessionFilters,
                    r.ServsSessionFilters, r.Username, r.Report, r.Public, r.PaginationInfo.PageNumber, r.PaginationInfo.ItemsPerPage, orderField, orderType,
                    r.WorkspaceId, r.FilterString, r.Period, ref resultsCount);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                long iterator = 0;
                long minval = 0;
                long maxval = 10;
                long page = 0;
                long perpage = 10;
                if (r.PaginationInfo != null)
                {
                    perpage = r.PaginationInfo.ItemsPerPage.HasValue ? r.PaginationInfo.ItemsPerPage.Value : 10;
                    page = r.PaginationInfo.PageNumber.HasValue ? r.PaginationInfo.PageNumber.Value : 0;
                    if (r.PaginationInfo.PageNumber.HasValue)
                    {
                        minval = r.PaginationInfo.PageNumber.Value * perpage;
                        maxval = (r.PaginationInfo.PageNumber.Value + 1) * perpage;
                    }
                }

                if (docList != null)
                    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Document document in docList.Items)
                    {
                        /*if (iterator >= minval && iterator < maxval)
                        {
                        */
                        documents.Documents.Add(TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(r.CompanyDb, document));
                        /*}
                        iterator++;*/
                    }

                PaginationResponse pagResp = new PaginationResponse();
                pagResp.TotalNumber = resultsCount.Value;
                pagResp.PageNumber = r.PaginationInfo.PageNumber;

                GetGeneralDocumentsResponse response = new GetGeneralDocumentsResponse { MyDocumentsTree = documents, PaginationInfo = pagResp };
                return response;
            }
            catch (BusinessLogicException ble)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = ble.Message + "[" + ble.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (BusinessRulesException bre)
            {
                DocumentNotFound ex = new DocumentNotFound { Description = bre.Message + "[" + bre.InnerException.Message + "]" };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                {
                    Description =
                        "Ocorreu um erro na implementação do serviço [" + e.Message + "]"
                };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetPatientDocumentsResponse GetPatientDocuments(GetPatientDocumentsRequest request)
        {
            try
            {
                DocumentList docList = DocumentLogic.GetPatientDocuments(
                    request.CompanyDb,
                    request.PatientType,
                    request.PatientId,
                    request.EpisodeType,
                    request.EpisodeId,
                    request.Institution,
                    request.Place,
                    request.Application,
                    request.DocumentType,
                    request.Username);

                DocumentsList documents = new DocumentsList { Documents = new DataContracts.Documents() };

                if (docList != null)
                    foreach (Eresults.Common.WCF.BusinessEntities.Document obj in docList.Items)
                    {
                        Document document = TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentToDocument(request.CompanyDb, obj);
                        documents.Documents.Add(document);
                    }

                GetPatientDocumentsResponse response = new GetPatientDocumentsResponse { MyDocumentsTree = documents };
                return response;
            }
            catch (Exception e)
            {
                DocumentNotFound ex = new DocumentNotFound
                                          {
                                              Description =
                                                  "Ocoreu um erro na implementação do serviço [" + e.Message + "]"
                                          };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetAllPatientDocumentsResponse GetAllPatientDocuments(GetAllPatientDocumentsRequest request)
        {
            try
            {
                DocumentList docList =
                   DocumentLogic.GetPatientDocuments(
                        request.CompanyDb,
                        request.PatEntId,
                        request.ReqService,
                        request.EpisodeTypeId,
                        request.EpisodeId,
                        request.EpiDateBegin,
                        request.EpiDateEnd,
                        request.Doc,
                        request.ExtId,
                        request.DocType,
                        request.AppId,
                        request.LocalId,
                        request.InstId,
                        request.DocDateBegin,
                        request.DocDateEnd,
                        request.ValDateBegin,
                        request.ValDateEnd,
                        request.GlobalFilters,
                        request.DocsSessionFilters,
                        request.ServsSessionFilters,
                        request.UserName,
                        request.UserAnaRes);

                PatientDocumentList documents =
                    new PatientDocumentList { PatientDocuments = new PatientDocuments() };

                if (docList != null)
                    foreach (PatientDocument document in docList.Items.Select(TranslateBetweenDocumentBeAndPatientDocumentDc.TranslateDocumentToPatientDocument))
                    {
                        documents.PatientDocuments.Add(document);
                    }

                GetAllPatientDocumentsResponse response =
                    new GetAllPatientDocumentsResponse { PatientDocumentsTree = documents };

                return response;
            }
            catch
            {
                DocumentNotFound ex = new DocumentNotFound { Description = "Documentos não encontrados." };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetAllPatientDocumentsMultiResponse GetAllPatientDocumentsMulti(GetAllPatientDocumentsMultiRequest request)
        {
            try
            {
                DocumentList docList =
                   DocumentLogic.GetPatientDocumentsMulti(
                        request.CompanyDb,
                        request.PatEntIds,
                        request.ReqService,
                        request.EpisodeTypeId,
                        request.EpisodeId,
                        request.EpiDateBegin,
                        request.EpiDateEnd,
                        request.Doc,
                        request.ExtId,
                        request.DocType,
                        request.AppId,
                        request.LocalId,
                        request.InstId,
                        request.DocDateBegin,
                        request.DocDateEnd,
                        request.ValDateBegin,
                        request.ValDateEnd,
                        request.GlobalFilters,
                        request.DocsSessionFilters,
                        request.ServsSessionFilters,
                        request.UserName,
                        request.UserAnaRes,
                        request.ExecService);

                PatientDocumentList documents =
                    new PatientDocumentList { PatientDocuments = new PatientDocuments() };

                if (docList != null)
                    foreach (PatientDocument document in docList.Items.Select(TranslateBetweenDocumentBeAndPatientDocumentDc.TranslateDocumentToPatientDocument))
                    {
                        documents.PatientDocuments.Add(document);
                    }

                GetAllPatientDocumentsMultiResponse response =
                    new GetAllPatientDocumentsMultiResponse { PatientDocumentsTree = documents };

                return response;
            }
            catch
            {
                DocumentNotFound ex = new DocumentNotFound { Description = "Documentos não encontrados." };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetDocumentsByExternalIdResponse GetDocumentsByExternalId(GetDocumentsByExternalIdRequest request)
        {
            try
            {
                DocumentList docList =
                   DocumentLogic.GetDocumentsByExternalId(
                        request.CompanyDb,
                        request.Doc,
                        request.AppId,
                        request.LocalId,
                        request.InstId,
                        request.DocChildType,
                        request.DocFileType
                    );

                PatientDocumentList documents =
                    new PatientDocumentList { PatientDocuments = new PatientDocuments() };

                if (docList != null)
                    foreach (PatientDocument document in docList.Items.Select(TranslateBetweenDocumentBeAndPatientDocumentDc.TranslateDocumentToPatientDocument))
                    {
                        documents.PatientDocuments.Add(document);
                    }

                GetDocumentsByExternalIdResponse response =
                    new GetDocumentsByExternalIdResponse { DocumentsTree = documents };

                return response;
            }
            catch
            {
                DocumentNotFound ex = new DocumentNotFound { Description = "Documentos não encontrados." };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override GetDocumentsByExternalIdV2Response GetDocumentsByExternalIdV2(GetDocumentsByExternalIdV2Request request)
        {
            try
            {
                DocumentList docList =
                   DocumentLogic.GetDocumentsByExternalIdV2(
                        request.CompanyDb,
                        request.Document,
                        request.DocumentTypeId,
                        request.ApplicationId,
                        request.PlaceId,
                        request.InstitutionId,
                        request.DocChildType,
                        request.DocFileType
                    );

                PatientDocumentList documents =
                    new PatientDocumentList { PatientDocuments = new PatientDocuments() };

                if (docList != null)
                    foreach (PatientDocument document in docList.Items.Select(TranslateBetweenDocumentBeAndPatientDocumentDc.TranslateDocumentToPatientDocument))
                    {
                        documents.PatientDocuments.Add(document);
                    }

                GetDocumentsByExternalIdV2Response response =
                    new GetDocumentsByExternalIdV2Response { DocumentsTree = documents };

                return response;
            }
            catch
            {
                DocumentNotFound ex = new DocumentNotFound { Description = "Documentos não encontrados." };
                throw new FaultException<DocumentNotFound>(ex, ex.Description);
            }
        }

        public override void RegisterDocumentAccess(RegisterDocumentAccessRequest request)
        {
            DocumentLogic.RegisterDocumentAccess(
                request.CompanyDb,
                request.DocumentAccess.SessionId,
                request.DocumentAccess.UserId,
                request.DocumentAccess.UserName,
                request.DocumentAccess.DocId,
                request.DocumentAccess.DocRef,
                request.DocumentAccess.ArtifactId,
                request.DocumentAccess.VersionId,
                request.DocumentAccess.AppOrigin,
                request.DocumentAccess.DocType);
        }

        public override GetDocumentVideosResponse GetDocumentVideos(GetDocumentVideosRequest request)
        {
            Eresults.Common.WCF.BusinessEntities.VideoList videoList = DocumentLogic.GetDocumentVideos(request.CompanyDB, request.DocId);
            GetDocumentVideosResponse response = new GetDocumentVideosResponse();
            response.DocumentVideos = TranslateBetweenVideoBeAndVideoDc.TranslateVideoListBeToVideoListDc(videoList);
            return response;
        }

        public override PromoteDocumentToPublicResponse PromoteDocumentToPublic(PromoteDocumentToPublicRequest request)
        {
            PromoteDocumentToPublicResponse response = new PromoteDocumentToPublicResponse();
            response.Donne = DocumentLogic.PromoteDocumentToPublic(request.CompanyDB, TranslateBetweenDocumentBeAndDocumentDc.TranslateDocumentInfoForPublish(request.DocumentsInfo));
            return response;
        }


        public override OpenDynamicFormResponse OpenDynamicForm(OpenDynamicFormRequest request)
        {
            OpenDynamicFormResponse response = new OpenDynamicFormResponse();

            response.Url = DocumentLogic.OpenDynamicForm(request.CompanyDb, request.App, request.DocType, request.Filter, request.User);

            return response;
        }

        public override UpdateElementDescriptionResponse UpdateElementDescription(UpdateElementDescriptionRequest request)
        {
            UpdateElementDescriptionResponse response = new UpdateElementDescriptionResponse();

            response.Success = DocumentLogic.UpdateElementDescription(request.CompanyDb, request.ElementId, request.Title, request.Description);

            return response;
        }
    }
}