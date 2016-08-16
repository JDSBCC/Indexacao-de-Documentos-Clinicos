using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
    /// <summary>
    /// Service Contract Class - CancelDocumentPermanentlyRequestV2
    /// </summary>
    [WCF::MessageContract(WrapperName = "CancelDocumentPermanentlyRequestV2", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class CancelDocumentPermanentlyRequestV2
    {
        private string companyDb;
        private string institution;
        private string place;
        private string application;
        private string documentType;
        private string documentExternalId;
        private string username;
        private string observations;
        private string justificationId;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
        public string CompanyDb
        {
            get { return companyDb; }
            set { companyDb = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Institution")]
        public string Institution
        {
            get { return institution; }
            set { institution = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Place")]
        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Application")]
        public string Application
        {
            get { return application; }
            set { application = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocumentType")]
        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocumentExternalId")]
        public string DocumentExternalId
        {
            get { return documentExternalId; }
            set { documentExternalId = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Username")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Observations")]
        public string Observations
        {
            get { return observations; }
            set { observations = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "JustificationId")]
        public string JustificationId
        {
            get { return justificationId; }
            set { justificationId = value; }
        }
    }
}

