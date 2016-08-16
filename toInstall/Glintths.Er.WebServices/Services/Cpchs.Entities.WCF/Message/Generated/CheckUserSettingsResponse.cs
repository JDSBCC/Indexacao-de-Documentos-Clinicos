using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(IsWrapped = true)] 
    public class CheckUserSettingsResponse
    {
        [WCF::MessageBodyMember(Name = "HasSettings")] 
        public bool? HasSettings
        {
            get;
            set;
        }
    }
}
