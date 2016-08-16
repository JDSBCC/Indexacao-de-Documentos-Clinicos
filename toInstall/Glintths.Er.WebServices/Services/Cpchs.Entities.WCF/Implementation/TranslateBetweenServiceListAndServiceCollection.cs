using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenServiceListAndServiceCollection
    {
        public static ServiceCollection TranslateServicesToServices(ServiceList from)
        {
            ServiceCollection to = new ServiceCollection();
            Cpchs.Entities.WCF.DataContracts.Service tempService;
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Service service in from.Items)
            {
                tempService = new Cpchs.Entities.WCF.DataContracts.Service();
                tempService = TranslateBetweenServiceBEAndServiceDC.TranslateServiceToService(service);
                to.Add(tempService);
            }
            return to;
        }
    }
}