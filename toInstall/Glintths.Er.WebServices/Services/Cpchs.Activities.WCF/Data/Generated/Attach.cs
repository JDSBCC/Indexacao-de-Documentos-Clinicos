using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Attach")]
    public partial class Attach
    {
        private string attachExtensionField;
        private string attachBaseUrlField;
        private bool attachEncryptionField;
        private string attachQueryUrlField;
        private string attachDescrField;
        private string attachProductField;
        private string attachReqIdField;
        private System.Nullable<System.DateTime> attachReqDateField;
        private System.Collections.Generic.Dictionary<string, string> attachExamInfoField;
        private long attachOrderField;
        private System.Nullable<System.DateTime> attachResDateField;
        private System.Nullable<System.DateTime> attachValDateField;
        private Notes attachReqNotesField;
        private Notes attachResNotesField;

        [WcfSerialization::DataMember(Name = "attachExtension", IsRequired = false, Order = 0)]
        public string attachExtension
        {
            get { return attachExtensionField; }
            set { attachExtensionField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachBaseUrl", IsRequired = false, Order = 1)]
        public string attachBaseUrl
        {
            get { return attachBaseUrlField; }
            set { attachBaseUrlField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachEncryption", IsRequired = false, Order = 2)]
        public bool attachEncryption
        {
            get { return attachEncryptionField; }
            set { attachEncryptionField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachQueryUrl", IsRequired = false, Order = 3)]
        public string attachQueryUrl
        {
            get { return attachQueryUrlField; }
            set { attachQueryUrlField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachDescr", IsRequired = false, Order = 4)]
        public string attachDescr
        {
            get { return attachDescrField; }
            set { attachDescrField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachProduct", IsRequired = false, Order = 5)]
        public string attachProduct
        {
            get { return attachProductField; }
            set { attachProductField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachReqId", IsRequired = false, Order = 6)]
        public string attachReqId
        {
            get { return attachReqIdField; }
            set { attachReqIdField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachReqDate", IsRequired = false, Order = 7)]
        public System.Nullable<System.DateTime> attachReqDate
        {
            get { return attachReqDateField; }
            set { attachReqDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachExamInfo", IsRequired = false, Order = 8)]
        public System.Collections.Generic.Dictionary<string, string> attachExamInfo
        {
            get { return attachExamInfoField; }
            set { attachExamInfoField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachOrder", IsRequired = false, Order = 9)]
        public long attachOrder
        {
            get { return attachOrderField; }
            set { attachOrderField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachResDate", IsRequired = false, Order = 10)]
        public System.Nullable<System.DateTime> attachResDate
        {
            get { return attachResDateField; }
            set { attachResDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachValDate", IsRequired = false, Order = 11)]
        public System.Nullable<System.DateTime> attachValDate
        {
            get { return attachValDateField; }
            set { attachValDateField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachReqNotes", IsRequired = false, Order = 12)]
        public Notes attachReqNotes
        {
            get { return attachReqNotesField; }
            set { attachReqNotesField = value; }
        }

        [WcfSerialization::DataMember(Name = "attachResNotes", IsRequired = false, Order = 13)]
        public Notes attachResNotes
        {
            get { return attachResNotesField; }
            set { attachResNotesField = value; }
        }

        
    }
}