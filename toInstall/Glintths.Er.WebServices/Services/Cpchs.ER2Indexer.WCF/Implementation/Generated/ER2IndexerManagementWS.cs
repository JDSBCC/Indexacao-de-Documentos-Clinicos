//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using WCF = global::System.ServiceModel;

namespace Cpchs.ER2Indexer.WCF.ServiceImplementation
{
    /// <summary>
    /// Service Class - ER2IndexerManagementWS
    /// </summary>
    [WCF::ServiceBehavior(Name = "ER2IndexerManagementWS",
        Namespace = "urn:Cpchs.ER2Indexer",
        InstanceContextMode = WCF::InstanceContextMode.PerSession,
        ConcurrencyMode = WCF::ConcurrencyMode.Single)]
    public abstract class ER2IndexerManagementWSBase : Cpchs.ER2Indexer.WCF.ServiceContracts.IER2IndexerManagementSC
    {
        #region ER2IndexerManagementSC Members

        public virtual void IndexDocument(Cpchs.ER2Indexer.WCF.MessageContracts.IndexDocumentRequest request)
        {
        }

        public virtual void IndexDocumentV2(Cpchs.ER2Indexer.WCF.MessageContracts.IndexDocumentRequestV2 request)
        {
        }

        #endregion ER2IndexerManagementSC Members
    }

    public partial class ER2IndexerManagementWS : ER2IndexerManagementWSBase
    {
    }
}