using System.Net.Security;
using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.ServiceContracts
{
    [WCF::ServiceContract(Namespace = "urn:Cpchs.Activities", Name = "ExamsManagementSC", SessionMode = WCF::SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None)]
    public partial interface IExamsManagementSC
    {
        [WCF::FaultContract(typeof(Cpchs.Activities.WCF.FaultContracts.NoExam))]
        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Activities.ExamsManagementSC.GetExamsByDocumentId", ReplyAction = "urn:Cpchs.Activities.ExamsManagementSC.GetExamsForArtifact", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Activities.WCF.MessageContracts.GetExamsByDocumentIdResponse GetExamsByDocumentId(Cpchs.Activities.WCF.MessageContracts.GetExamsByDocumentIdRequest request);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Activities.ExamsManagementSC.GetPatientExamsMulti", ReplyAction = "urn:Cpchs.Activities.ExamsManagementSC.GetPatientExamsMulti", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Activities.WCF.MessageContracts.GetPatientExamsMultiResponse GetPatientExamsMulti(Cpchs.Activities.WCF.MessageContracts.GetPatientExamsMultiRequest request);
    }
}