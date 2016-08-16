using System;
using System.Configuration;
using System.Globalization;
using Cpchs.Documents.WCF.BusinessLogic;
using Cpchs.Documents.WCF.DataContracts;
using Cpchs.Documents.WCF.MessageContracts;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public partial class FilesManagementWS
    {
        public override GetDocumentFilesResponse GetDocumentFiles(GetDocumentFilesRequest request)
        {
            FileList fileList = FileLogic.GetDocumentFiles(request.CompanyDb,request.DocId);
            string fileBaseUrl = GetFileBaseUrl(request.CompanyDb);
            string thumbBaseUrl = GetThumbBaseUrl(request.CompanyDb);
            FileInfoList files = new FileInfoList {FilesInfo = new FilesInfo()};
            foreach(File obj in fileList.Items)
            {
                FileInfo file = TranslateBetweenFileBeAndFileInfoDc.TranslateFileToFileInfo(obj);                
                file.FileEncryption = true;
                file.FileBaseUrl = fileBaseUrl;
                file.FileQueryUrl = "docId=" + request.DocId+ "&elemId=" + obj.FileElemId;
                if (obj.Thumb != null && obj.Thumb.Length != 0)
                    file.FileThumb = "data:image/png;base64," + Convert.ToBase64String(obj.Thumb, Base64FormattingOptions.None);
                else
                {
                    if (!string.IsNullOrEmpty(obj.FileThumbPath))
                    {
                        file.FileThumb = thumbBaseUrl + "?" + file.FileQueryUrl;
                    }
                }

                files.FilesInfo.Add(file);
            }
            GetDocumentFilesResponse response = new GetDocumentFilesResponse {DocumentFiles = files};
            return response;
        }

        public override GetFileByElementIdResponse GetFileByElementId(GetFileByElementIdRequest request)
        {
            GetFileByElementIdResponse response = new GetFileByElementIdResponse {File = new FileInfo()};
            File file = FileLogic.GetFileByElementid(request.CompanyDb, request.ElementId);
            if (file != null)
            {
                string fileBaseUrl = GetFileBaseUrl(request.CompanyDb);
                FileInfo fileInfo = TranslateBetweenFileBeAndFileInfoDc.TranslateFileToFileInfo(file);
                fileInfo.FileEncryption = true;
                fileInfo.FileBaseUrl = fileBaseUrl;
                fileInfo.FileQueryUrl = "docId=" + file.DocumentUniqueId.ToString(CultureInfo.InvariantCulture) + "&elemId=" + file.FileElemId;                
                if (file.Thumb != null && file.Thumb.Length != 0)
                    fileInfo.FileThumb = "data:image/png;base64," + Convert.ToBase64String(file.Thumb, Base64FormattingOptions.None);
                response.File = fileInfo;
            }
            return response;
        }
        
        private static string GetFileBaseUrl(string companyDb)
        {
            try
            {
                ERConfiguration config = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "FILEVIEWERURI", "INITCONFIG");
                return config.ErConfigValue;
            }
            catch
            {
                return ConfigurationSettings.AppSettings["FileBaseUrl"];
            }
        }

        private static string GetThumbBaseUrl(string companyDb)
        {
            try
            {
                ERConfiguration config = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "THUMBVIEWERURI", "INITCONFIG");
                return config.ErConfigValue;
            }
            catch
            {
                return ConfigurationSettings.AppSettings["FileBaseUrl"];
            }
        }
    }
}