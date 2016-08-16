
using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetEpisodeTypeByCodeRequest", WrapperNamespace = "urn:Cpchs.Entities", IsWrapped = true)]
    public partial class GetEpisodeTypeByCodeRequest
    {
        private string companyDb;
        private string episodeCode;

        [WCF::MessageBodyMember(Name = "CompanyDb")]
        public string CompanyDb
        {
            get { return companyDb; }
            set { companyDb = value; }
        }

        [WCF::MessageBodyMember(Name = "EpisodeCode")]
        public string EpisodeCode
        {
            get { return episodeCode; }
            set { episodeCode = value; }
        }
    }
}
