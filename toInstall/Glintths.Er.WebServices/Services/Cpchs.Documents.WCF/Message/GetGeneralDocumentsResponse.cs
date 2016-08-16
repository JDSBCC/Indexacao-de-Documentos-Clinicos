using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
    /// <summary>
    /// Service Contract Class - GetDocumentsByMultiCriteriaV2Response
    /// </summary>
    [WCF::MessageContract(WrapperName = "GetGeneralDocumentsResponse", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class GetGeneralDocumentsResponse
    {
        private Cpchs.Documents.WCF.DataContracts.DocumentsList myDocumentsTree;
        private Cpchs.Entities.WCF.DataContracts.PaginationResponse paginationInfo;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "MyDocumentsTree")]
        public Cpchs.Documents.WCF.DataContracts.DocumentsList MyDocumentsTree
        {
            get { return myDocumentsTree; }
            set { myDocumentsTree = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PaginationInfo")]
        public Cpchs.Entities.WCF.DataContracts.PaginationResponse PaginationInfo
        {
            get { return paginationInfo; }
            set { paginationInfo = value; }
        }
    }
}

