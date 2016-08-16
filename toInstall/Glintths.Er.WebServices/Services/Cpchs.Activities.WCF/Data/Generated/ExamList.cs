using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "ExamList")]
    public partial class ExamList
    {
        private Exams exams;

        [WcfSerialization::DataMember(Name = "Exams", IsRequired = false, Order = 0)]
        public Exams Exams
        {
            get { return exams; }
            set { exams = value; }
        }
    }
}