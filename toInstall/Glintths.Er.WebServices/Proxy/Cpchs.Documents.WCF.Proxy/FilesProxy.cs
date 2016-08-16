using System;
using Cpchs.Documents.WCF.DataContracts;
using Cpchs.Documents.WCF.Proxy.FilesManagementSR;
using Cpchs.Security.Providers.Utilities;

namespace Cpchs.Documents.WCF.Proxy
{
    public sealed class FilesProxy
    {
        private readonly string _endpoint;
        private readonly FilesManagementSCClient _client;

        public Uri EndpointUri
        {
            get { return _client.Endpoint.Address.Uri; }
        }

        public string CompanyDbName
        {
            get { return SecurityContext.Instance.CurrentApplicationName; }
        }

        public FilesProxy()
        {
            _endpoint = WebServiceContext.Instance.GetWebServiceUrl(CompanyDbName,ToString());
            _client = new FilesManagementSCClient(_endpoint);
        }

        /*public DataContracts.FilesInfo GetDocumentFiles(long docId)
        {
            string companyDb = SecurityContext.Instance.CurrentApplicationName;
            GetDocumentFilesRequest request = new GetDocumentFilesRequest {CompanyDb = companyDb, DocId = docId};
            FileInfoList fileList = _client.GetDocumentFiles(request);
            _client.Close();
            return fileList.FilesInfo;
        }*/
    }
}