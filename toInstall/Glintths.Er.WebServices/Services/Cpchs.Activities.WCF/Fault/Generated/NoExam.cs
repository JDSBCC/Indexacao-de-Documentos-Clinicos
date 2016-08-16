using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.FaultContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "NoExam")]
    public partial class NoExam
    {
        private string description;

        [WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 0)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}