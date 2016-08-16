using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Antib")]
    public partial class Antib
    {
        private string antibNameField;
        private string antibSensitivityField;
        private string antibAcronymField;

        [WcfSerialization::DataMember(Name = "antibName", IsRequired = false, Order = 0)]
        public string antibName
        {
            get { return antibNameField; }
            set { antibNameField = value; }
        }

        [WcfSerialization::DataMember(Name = "antibSensitivity", IsRequired = false, Order = 1)]
        public string antibSensitivity
        {
            get { return antibSensitivityField; }
            set { antibSensitivityField = value; }
        }

        [WcfSerialization::DataMember(Name = "antibAcronym", IsRequired = false, Order = 2)]
        public string antibAcronym
        {
            get { return antibAcronymField; }
            set { antibAcronymField = value; }
        }
    }
}