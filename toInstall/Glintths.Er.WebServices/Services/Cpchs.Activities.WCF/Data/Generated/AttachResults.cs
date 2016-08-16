using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities", ItemName = "AttachResults")]
    public partial class AttachResults : System.Collections.Generic.List<Attach>
    {
    }
}