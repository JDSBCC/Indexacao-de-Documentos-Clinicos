using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Exam")]
    public partial class Exam
    {
        private string examDescriptionField;
        private long examOrderField;
        private AlphanumResults examAlphanumResultsField;
        private MicroResults examMicroResultsField;
        private AttachResults examAttachResultsField;
        private Exams examChildExamsField;
        private string examProductField;
        private Notes examNotesField;
        private string examAcronymField;

        [WcfSerialization::DataMember(Name = "examDescription", IsRequired = false, Order = 0)]
        public string examDescription
        {
            get { return examDescriptionField; }
            set { examDescriptionField = value; }
        }

        [WcfSerialization::DataMember(Name = "examOrder", IsRequired = false, Order = 1)]
        public long examOrder
        {
            get { return examOrderField; }
            set { examOrderField = value; }
        }

        [WcfSerialization::DataMember(Name = "examAlphanumResults", IsRequired = false, Order = 2)]
        public AlphanumResults examAlphanumResults
        {
            get { return examAlphanumResultsField; }
            set { examAlphanumResultsField = value; }
        }

        [WcfSerialization::DataMember(Name = "examMicroResults", IsRequired = false, Order = 3)]
        public MicroResults examMicroResults
        {
            get { return examMicroResultsField; }
            set { examMicroResultsField = value; }
        }

        [WcfSerialization::DataMember(Name = "examAttachResults", IsRequired = false, Order = 4)]
        public AttachResults examAttachResults
        {
            get { return examAttachResultsField; }
            set { examAttachResultsField = value; }
        }

        [WcfSerialization::DataMember(Name = "examChildExams", IsRequired = false, Order = 5)]
        public Exams examChildExams
        {
            get { return examChildExamsField; }
            set { examChildExamsField = value; }
        }

        [WcfSerialization::DataMember(Name = "examProduct", IsRequired = false, Order = 6)]
        public string examProduct
        {
            get { return examProductField; }
            set { examProductField = value; }
        }

        [WcfSerialization::DataMember(Name = "examNotes", IsRequired = false, Order = 7)]
        public Notes examNotes
        {
            get { return examNotesField; }
            set { examNotesField = value; }
        }

        [WcfSerialization::DataMember(Name = "examAcronym", IsRequired = false, Order = 8)]
        public string examAcronym
        {
            get { return examAcronymField; }
            set { examAcronymField = value; }
        }
    }
}