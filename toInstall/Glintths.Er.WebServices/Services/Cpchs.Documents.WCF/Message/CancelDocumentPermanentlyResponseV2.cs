using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
    /// <summary>
    /// Service Contract Class - CancelDocumentPermanentlyResponseV2
    /// </summary>
    [WCF::MessageContract(WrapperName = "CancelDocumentPermanentlyResponseV2", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class CancelDocumentPermanentlyResponseV2
    {
        private bool operationSuccess;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "OperationSuccess")]
        public bool OperationSuccess
        {
            get { return operationSuccess; }
            set { operationSuccess = value; }
        }
    }
}

