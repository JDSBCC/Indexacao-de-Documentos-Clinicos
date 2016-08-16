using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities", ItemName = "MicroResults")]
    public partial class MicroResults : System.Collections.Generic.List<Micro>
    {
    }
}