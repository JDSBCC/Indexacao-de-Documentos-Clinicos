using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities", ItemName = "AntibResults")]
    public partial class AntibResults : System.Collections.Generic.List<Antib>
    {
    }
}