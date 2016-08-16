using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenApplicationListAndMyNodeCollection
    {
        public static MyNodeCollection TranslateApplicationsToMyNodes(Cpchs.Eresults.Common.WCF.BusinessEntities.ApplicationList from)
        {
            MyNodeCollection to = new MyNodeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Application app in from.Items)
            {
                to.Add(TranslateBetweenApplicationBEAndMyNodeDC.TranslateApplicationToMyNode(app));
            }
            return to;
        }
    }
}
