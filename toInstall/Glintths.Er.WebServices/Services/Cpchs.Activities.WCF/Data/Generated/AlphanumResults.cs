using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities", ItemName = "AlphanumResults")]
    public partial class AlphanumResults : System.Collections.Generic.List<Alphanum>
    {
    }
}