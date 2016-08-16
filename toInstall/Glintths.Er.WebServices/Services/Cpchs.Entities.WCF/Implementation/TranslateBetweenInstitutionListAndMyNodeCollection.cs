using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenInstitutionListAndMyNodeCollection
    {
        public static MyNodeCollection TranslateInstitutionsToMyNodes(Cpchs.Eresults.Common.WCF.BusinessEntities.InstitutionList from)
        {
            MyNodeCollection to = new MyNodeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Institution inst in from.Items)
            {
                to.Add(TranslateBetweenInstitutionBEAndMyNodeDC.TranslateInstitutionToMyNode(inst));
            }
            return to;
        }
    }
}
