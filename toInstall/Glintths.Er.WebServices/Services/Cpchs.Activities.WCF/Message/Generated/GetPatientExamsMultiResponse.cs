using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetPatientExamsMultiResponse", WrapperNamespace = "urn:Cpchs.Activities", IsWrapped = true)]
    public partial class GetPatientExamsMultiResponse
    {
        private Cpchs.Activities.WCF.DataContracts.ExamList patientExams;

        [WCF::MessageBodyMember(Name = "PatientExams")]
        public Cpchs.Activities.WCF.DataContracts.ExamList PatientExams
        {
            get { return patientExams; }
            set { patientExams = value; }
        }
    }
}