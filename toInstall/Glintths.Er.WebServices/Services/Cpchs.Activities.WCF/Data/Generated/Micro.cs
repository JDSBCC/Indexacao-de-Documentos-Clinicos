using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Micro")]
    public partial class Micro
    {
        private string microGroupField;
        private string microNameField;
        private string microQuantifField;
        private long microOrderField;
        private AntibResults microAntibsField;
        private string microProductField;
        private string microReqIdField;
        private System.Nullable<System.DateTime> microReqDateField;
        private System.Collections.Generic.Dictionary<string, string> microExamInfoField;
        private System.Nullable<System.DateTime> microResDateField;
        private System.Nullable<System.DateTime> microValDateField;
        private Notes microReqNotesField;
        private Notes microResNotesField;
        private string microIdField;

        [WcfSerialization::DataMember(Name = "microGroup", IsRequired = false, Order = 0)]
        public string microGroup
        {
            get { return microGroupField; }
            set { microGroupField = value; }
        }

        [WcfSerialization::DataMember(Name = "microName", IsRequired = false, Order = 1)]
        public string microName
        {
            get { return microNameField; }
            set { microNameField = value; }
        }

        [WcfSerialization::DataMember(Name = "microQuantif", IsRequired = false, Order = 2)]
        public string microQuantif
        {
            get { return microQuantifField; }
            set { microQuantifField = value; }
        }

        [WcfSerialization::DataMember(Name = "microOrder", IsRequired = false, Order = 3)]
        public long microOrder
        {
            get { return microOrderField; }
            set { microOrderField = value; }
        }

        [WcfSerialization::DataMember(Name = "microAntibs", IsRequired = false, Order = 4)]
        public AntibResults microAntibs
        {
            get { return microAntibsField; }
            set { microAntibsField = value; }
        }

        [WcfSerialization::DataMember(Name = "microProduct", IsRequired = false, Order = 5)]
        public string microProduct
        {
            get { return microProductField; }
            set { microProductField = value; }
        }

        [WcfSerialization::DataMember(Name = "microReqId", IsRequired = false, Order = 6)]
        public string microReqId
        {
            get { return microReqIdField; }
            set { microReqIdField = value; }
        }

        [WcfSerialization::DataMember(Name = "microReqDate", IsRequired = false, Order = 7)]
        public System.Nullable<System.DateTime> microReqDate
        {
            get { return microReqDateField; }
            set { microReqDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "microExamInfo", IsRequired = false, Order = 8)]
        public System.Collections.Generic.Dictionary<string, string> microExamInfo
        {
            get { return microExamInfoField; }
            set { microExamInfoField = value; }
        }

        [WcfSerialization::DataMember(Name = "microResDate", IsRequired = false, Order = 9)]
        public System.Nullable<System.DateTime> microResDate
        {
            get { return microResDateField; }
            set { microResDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "microValDate", IsRequired = false, Order = 10)]
        public System.Nullable<System.DateTime> microValDate
        {
            get { return microValDateField; }
            set { microValDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "microReqNotes", IsRequired = false, Order = 11)]
        public Notes microReqNotes
        {
            get { return microReqNotesField; }
            set { microReqNotesField = value; }
        }

        [WcfSerialization::DataMember(Name = "microResNotes", IsRequired = false, Order = 12)]
        public Notes microResNotes
        {
            get { return microResNotesField; }
            set { microResNotesField = value; }
        }

        [WcfSerialization::DataMember(Name = "microId", IsRequired = false, Order = 13)]
        public string microId
        {
            get { return microIdField; }
            set { microIdField = value; }
        }
    }
}