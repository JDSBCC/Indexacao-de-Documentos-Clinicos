using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "CheckUserSettingsRequest", WrapperNamespace = "urn:Cpchs.Entities", IsWrapped = true)] 
    public class CheckUserSettingsRequest
    {
        [WCF::MessageBodyMember(Name = "CompanyDb")] 
        public string CompanyDb
        {
            get;
            set;
        }

        [WCF::MessageBodyMember(Name = "Username")] 
        public string Username
        {
            get;
            set;
        }

        [WCF::MessageBodyMember(Name = "Application")] 
        public string Application
        {
            get;
            set;
        }

        [WCF::MessageBodyMember(Name = "Module")] 
        public string Module
        {
            get;
            set;
        }
    }
}
