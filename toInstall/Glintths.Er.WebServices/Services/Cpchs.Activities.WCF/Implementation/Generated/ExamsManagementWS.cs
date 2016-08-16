using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    [WCF::ServiceBehavior(Name = "ExamsManagementWS",
        Namespace = "urn:Cpchs.Activities",
        InstanceContextMode = WCF::InstanceContextMode.PerSession,
        ConcurrencyMode = WCF::ConcurrencyMode.Single)]
    public abstract class ExamsManagementWSBase : Cpchs.Activities.WCF.ServiceContracts.IExamsManagementSC
    {
        #region ExamsManagementSC Members

        public virtual Cpchs.Activities.WCF.MessageContracts.GetExamsByDocumentIdResponse GetExamsByDocumentId(Cpchs.Activities.WCF.MessageContracts.GetExamsByDocumentIdRequest request)
        {
            return null;
        }

        public virtual Cpchs.Activities.WCF.MessageContracts.GetPatientExamsMultiResponse GetPatientExamsMulti(Cpchs.Activities.WCF.MessageContracts.GetPatientExamsMultiRequest request)
        {
            return null;
        }

        #endregion ExamsManagementSC Members
    }

    public partial class ExamsManagementWS : ExamsManagementWSBase
    {
    }
}