using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Alphanum")]
    public partial class Alphanum
    {
        private string alphanumResultField;
        private string alphanumUnitField;
        private System.Nullable<double> alphanumInfRefValField;
        private System.Nullable<double> alphanumSupRefValField;
        private string alphanumTextRefValField;
        private long alphanumOrderField;
        private string alphanumMethodField;
        private string alphanumProductField;
        private string alphanumReqIdField;
        private System.Nullable<System.DateTime> alphanumReqDateField;
        private System.Collections.Generic.Dictionary<string, string> alphanumExamInfoField;
        private System.Nullable<System.DateTime> alphanumResDateField;
        private System.Nullable<System.DateTime> alphanumValDateField;
        private Notes alphanumReqNotesField;
        private Notes alphanumResNotesField;

        [WcfSerialization::DataMember(Name = "alphanumResult", IsRequired = false, Order = 0)]
        public string alphanumResult
        {
            get { return alphanumResultField; }
            set { alphanumResultField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumUnit", IsRequired = false, Order = 1)]
        public string alphanumUnit
        {
            get { return alphanumUnitField; }
            set { alphanumUnitField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumInfRefVal", IsRequired = false, Order = 2)]
        public System.Nullable<double> alphanumInfRefVal
        {
            get { return alphanumInfRefValField; }
            set { alphanumInfRefValField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumSupRefVal", IsRequired = false, Order = 3)]
        public System.Nullable<double> alphanumSupRefVal
        {
            get { return alphanumSupRefValField; }
            set { alphanumSupRefValField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumTextRefVal", IsRequired = false, Order = 4)]
        public string alphanumTextRefVal
        {
            get { return alphanumTextRefValField; }
            set { alphanumTextRefValField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumOrder", IsRequired = false, Order = 5)]
        public long alphanumOrder
        {
            get { return alphanumOrderField; }
            set { alphanumOrderField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumMethod", IsRequired = false, Order = 6)]
        public string alphanumMethod
        {
            get { return alphanumMethodField; }
            set { alphanumMethodField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumProduct", IsRequired = false, Order = 7)]
        public string alphanumProduct
        {
            get { return alphanumProductField; }
            set { alphanumProductField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumReqId", IsRequired = false, Order = 8)]
        public string alphanumReqId
        {
            get { return alphanumReqIdField; }
            set { alphanumReqIdField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumReqDate", IsRequired = false, Order = 9)]
        public System.Nullable<System.DateTime> alphanumReqDate
        {
            get { return alphanumReqDateField; }
            set { alphanumReqDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumExamInfo", IsRequired = false, Order = 10)]
        public System.Collections.Generic.Dictionary<string, string> alphanumExamInfo
        {
            get { return alphanumExamInfoField; }
            set { alphanumExamInfoField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumResDate", IsRequired = false, Order = 11)]
        public System.Nullable<System.DateTime> alphanumResDate
        {
            get { return alphanumResDateField; }
            set { alphanumResDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumValDate", IsRequired = false, Order = 12)]
        public System.Nullable<System.DateTime> alphanumValDate
        {
            get { return alphanumValDateField; }
            set { alphanumValDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumReqNotes", IsRequired = false, Order = 13)]
        public Notes alphanumReqNotes
        {
            get { return alphanumReqNotesField; }
            set { alphanumReqNotesField = value; }
        }

        [WcfSerialization::DataMember(Name = "alphanumResNotes", IsRequired = false, Order = 14)]
        public Notes alphanumResNotes
        {
            get { return alphanumResNotesField; }
            set { alphanumResNotesField = value; }
        }
    }
}