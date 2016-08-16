using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetPatienstBySpecsRequest", WrapperNamespace = "urn:Cpchs.Entities", IsWrapped = true)]
    public partial class GetPatientsBySpecsRequest
    {
        private string companyDb;
        private string search;
        private string globalFilters;
      //  private string docsSessionFilters;
        private string servsSessionFilters;
        private string userName;

        [WCF::MessageBodyMember(Name = "CompanyDb")]
        public string CompanyDb
        {
            get { return companyDb; }
            set { companyDb = value; }
        }

        [WCF::MessageBodyMember(Name = "Search")]
        public string Search
        {
            get { return search; }
            set { search = value; }
        }

        [WCF::MessageBodyMember(Name = "GlobalFilters")]
        public string GlobalFilters
        {
            get { return globalFilters; }
            set { globalFilters = value; }
        }

 /*       [WCF::MessageBodyMember(Name = "DocsSessionFilters")]
        public string DocsSessionFilters
        {
            get { return docsSessionFilters; }
            set { docsSessionFilters = value; }
        }
        */
        [WCF::MessageBodyMember(Name = "ServsSessionFilters")]
        public string ServsSessionFilters
        {
            get { return servsSessionFilters; }
            set { servsSessionFilters = value; }
        }

        [WCF::MessageBodyMember(Name = "UserName")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
