using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "urn:Cpchs.Activities")]
    public partial class Notes : System.Collections.Generic.List<Note>
    {
    }
}