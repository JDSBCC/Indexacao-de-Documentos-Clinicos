using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Activities.WCF.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "urn:Cpchs.Activities", Name = "Note")]
    public partial class Note
    {
        private string noteBody;
        private string noteReqRef;

        [WcfSerialization::DataMember(Name = "NoteBody", IsRequired = false, Order = 0)]
        public string NoteBody
        {
            get { return noteBody; }
            set { noteBody = value; }
        }

        [WcfSerialization::DataMember(Name = "NoteReqRef", IsRequired = false, Order = 1)]
        public string NoteReqRef
        {
            get { return noteReqRef; }
            set { noteReqRef = value; }
        }
    }
}