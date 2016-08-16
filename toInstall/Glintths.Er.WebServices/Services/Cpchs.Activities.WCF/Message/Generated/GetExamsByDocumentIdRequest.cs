using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetExamsByDocumentIdRequest", WrapperNamespace = "urn:Cpchs.Activities", IsWrapped = true)]
    public partial class GetExamsByDocumentIdRequest
    {
        private long docIdField;
        private string companyDbField;
        private string usernameField;
        private string sessionField;

        [WCF::MessageBodyMember(Name = "docId")]
        public long docId
        {
            get { return docIdField; }
            set { docIdField = value; }
        }

        [WCF::MessageBodyMember(Name = "companyDb")]
        public string companyDb
        {
            get { return companyDbField; }
            set { companyDbField = value; }
        }

        [WCF::MessageBodyMember(Name = "username")]
        public string username
        {
            get { return usernameField; }
            set { usernameField = value; }
        }

        [WCF::MessageBodyMember(Name = "session")]
        public string session
        {
            get { return sessionField; }
            set { sessionField = value; }
        }
    }
}