using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Glintths.Base.WCF.BusinessLogic
{
    public class CodificationsLogic
    {
        public static ERConfigurationList GetConfigurationByAreaKey(string companyDb, long? instId, long? placeId, long? appId, long? doctypeId, string key)
        {
            ERConfigurationList list = new ERConfigurationList();

            if (!instId.HasValue || !placeId.HasValue || !appId.HasValue || !doctypeId.HasValue)
            {
                list = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetConfigurationsByKey(companyDb, key);
            }
            else
            {
                list = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetConfigurationByAreaAndKey(companyDb, instId.Value, placeId.Value, appId.Value, doctypeId.Value, key);
            }

            return list;
        }

        public static ERConfigurationList GetConfigurationByScope(string companyDb, long? instId, long? placeId, long? appId, long? doctypeId, string scope)
        {
            ERConfigurationList list = new ERConfigurationList();

            list = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetConfigurationByScope(companyDb, instId, placeId, appId, doctypeId, scope);

            return list;
        }
    }
}
