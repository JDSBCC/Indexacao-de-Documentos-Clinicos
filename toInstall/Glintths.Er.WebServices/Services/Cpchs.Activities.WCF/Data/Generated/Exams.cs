using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities", ItemName = "Exams")]
    public partial class Exams : System.Collections.Generic.List<Exam>
    {
    }
}