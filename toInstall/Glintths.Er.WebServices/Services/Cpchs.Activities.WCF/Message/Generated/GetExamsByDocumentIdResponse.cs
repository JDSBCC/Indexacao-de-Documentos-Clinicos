using WCF = global::System.ServiceModel;

namespace Cpchs.Activities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetExamsByDocumentIdResponse", WrapperNamespace = "urn:Cpchs.Activities", IsWrapped = true)]
    public partial class GetExamsByDocumentIdResponse
    {
        private Cpchs.Activities.WCF.DataContracts.ExamList exams;

        [WCF::MessageBodyMember(Name = "Exams")]
        public Cpchs.Activities.WCF.DataContracts.ExamList Exams
        {
            get { return exams; }
            set { exams = value; }
        }
    }
}