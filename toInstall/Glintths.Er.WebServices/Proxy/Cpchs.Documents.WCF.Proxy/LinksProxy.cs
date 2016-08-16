using System;
using Cpchs.Documents.WCF.DataContracts;
using Cpchs.Documents.WCF.Proxy.LinksManagementSR;
using Cpchs.Security.Providers.Utilities;

namespace Cpchs.Documents.WCF.Proxy
{
    public sealed class LinksProxy
    {
        private readonly string _endpoint;
        private readonly LinksManagementSCClient _client;

        public Uri EndpointUri
        {
            get { return _client.Endpoint.Address.Uri; }
        }

        public string CompanyDbName
        {
            get { return SecurityContext.Instance.CurrentApplicationName; }
        }

        public LinksProxy()
        {
            _endpoint = WebServiceContext.Instance.GetWebServiceUrl(CompanyDbName,ToString());
            _client = new LinksManagementSCClient(_endpoint);
        }

        public LinksProxy(string companyDb)
        {
            SecurityContext.Instance.CurrentApplication = new Common.Security.Application(companyDb);
            _endpoint = WebServiceContext.Instance.GetWebServiceUrl(CompanyDbName,ToString());
            _client = new LinksManagementSCClient(_endpoint);
        }

        //public DataContracts.Links GetDocumentLinks(long docId)
        //{
        //    string companyDb = SecurityContext.Instance.CurrentApplicationName;
        //    return GetDocumentLinks(companyDb, docId);
        //}

        /*public DataContracts.Links GetDocumentLinks(string companyDb, long docId)
        {
            GetDocumentLinksRequest request = new GetDocumentLinksRequest {CompanyDb = companyDb, DocId = docId};
            LinkList linkList = _client.GetDocumentLinks(request);
            _client.Close();
            return linkList.Links;
        }*/
    }
}