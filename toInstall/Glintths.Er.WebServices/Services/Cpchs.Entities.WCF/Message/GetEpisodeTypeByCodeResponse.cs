using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(IsWrapped = true)] 
    public partial class GetEpisodeTypeByCodeResponse
    {
        [WCF::MessageBodyMember(Name = "EpisodeType")]
        public Cpchs.Entities.WCF.DataContracts.EpisodeType EpisodeType
        {
            get;
            set;
        }
    }
}
