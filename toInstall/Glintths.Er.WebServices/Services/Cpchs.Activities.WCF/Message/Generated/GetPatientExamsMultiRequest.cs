using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetPatientExamsMultiRequest", WrapperNamespace = "urn:Cpchs.Activities", IsWrapped = true)]
    public partial class GetPatientExamsMultiRequest
    {
        private string companyDb;
        private string patEntIds;
        private System.Nullable<long> episodeTypeId;
        private string episodeId;
        private System.Nullable<System.DateTime> epiDateBegin;
        private System.Nullable<System.DateTime> epiDateEnd;
        private string doc;
        private string extId;
        private System.Nullable<long> docType;
        private System.Nullable<long> appId;
        private System.Nullable<long> localId;
        private System.Nullable<long> instId;
        private System.Nullable<System.DateTime> docDateBegin;
        private System.Nullable<System.DateTime> docDateEnd;
        private System.Nullable<System.DateTime> valDateBegin;
        private System.Nullable<System.DateTime> valDateEnd;
        private string userName;
        private string globalFilters;
        private string docsSessionFilters;
        private string servsSessionFilters;

        [WCF::MessageBodyMember(Name = "CompanyDb")]
        public string CompanyDb
        {
            get { return companyDb; }
            set { companyDb = value; }
        }

        [WCF::MessageBodyMember(Name = "PatEntIds")]
        public string PatEntIds
        {
            get { return patEntIds; }
            set { patEntIds = value; }
        }

        [WCF::MessageBodyMember(Name = "EpisodeTypeId")]
        public System.Nullable<long> EpisodeTypeId
        {
            get { return episodeTypeId; }
            set { episodeTypeId = value; }
        }

        [WCF::MessageBodyMember(Name = "EpisodeId")]
        public string EpisodeId
        {
            get { return episodeId; }
            set { episodeId = value; }
        }

        [WCF::MessageBodyMember(Name = "EpiDateBegin")]
        public System.Nullable<System.DateTime> EpiDateBegin
        {
            get { return epiDateBegin; }
            set { epiDateBegin = value; }
        }

        [WCF::MessageBodyMember(Name = "EpiDateEnd")]
        public System.Nullable<System.DateTime> EpiDateEnd
        {
            get { return epiDateEnd; }
            set { epiDateEnd = value; }
        }

        [WCF::MessageBodyMember(Name = "Doc")]
        public string Doc
        {
            get { return doc; }
            set { doc = value; }
        }

        [WCF::MessageBodyMember(Name = "ExtId")]
        public string ExtId
        {
            get { return extId; }
            set { extId = value; }
        }

        [WCF::MessageBodyMember(Name = "DocType")]
        public System.Nullable<long> DocType
        {
            get { return docType; }
            set { docType = value; }
        }

        [WCF::MessageBodyMember(Name = "AppId")]
        public System.Nullable<long> AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        [WCF::MessageBodyMember(Name = "LocalId")]
        public System.Nullable<long> LocalId
        {
            get { return localId; }
            set { localId = value; }
        }

        [WCF::MessageBodyMember(Name = "InstId")]
        public System.Nullable<long> InstId
        {
            get { return instId; }
            set { instId = value; }
        }

        [WCF::MessageBodyMember(Name = "DocDateBegin")]
        public System.Nullable<System.DateTime> DocDateBegin
        {
            get { return docDateBegin; }
            set { docDateBegin = value; }
        }

        [WCF::MessageBodyMember(Name = "DocDateEnd")]
        public System.Nullable<System.DateTime> DocDateEnd
        {
            get { return docDateEnd; }
            set { docDateEnd = value; }
        }

        [WCF::MessageBodyMember(Name = "ValDateBegin")]
        public System.Nullable<System.DateTime> ValDateBegin
        {
            get { return valDateBegin; }
            set { valDateBegin = value; }
        }

        [WCF::MessageBodyMember(Name = "ValDateEnd")]
        public System.Nullable<System.DateTime> ValDateEnd
        {
            get { return valDateEnd; }
            set { valDateEnd = value; }
        }

        [WCF::MessageBodyMember(Name = "UserName")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [WCF::MessageBodyMember(Name = "GlobalFilters")]
        public string GlobalFilters
        {
            get { return globalFilters; }
            set { globalFilters = value; }
        }

        [WCF::MessageBodyMember(Name = "DocsSessionFilters")]
        public string DocsSessionFilters
        {
            get { return docsSessionFilters; }
            set { docsSessionFilters = value; }
        }

        [WCF::MessageBodyMember(Name = "ServsSessionFilters")]
        public string ServsSessionFilters
        {
            get { return servsSessionFilters; }
            set { servsSessionFilters = value; }
        }
    }
}