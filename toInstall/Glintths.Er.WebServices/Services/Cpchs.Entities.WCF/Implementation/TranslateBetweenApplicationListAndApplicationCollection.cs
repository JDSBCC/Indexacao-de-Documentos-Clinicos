using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenApplicationListAndApplicationCollection
    {
        public static ApplicationCollection TranslateApplicationsToApplications(ApplicationList from)
        {
            ApplicationCollection to = new ApplicationCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Application app in from.Items)
            {
                to.Add(TranslateBetweenApplicationBEAndApplicationDC.TranslateApplicationToApplication(app));
            }
            return to;
        }
    }
}