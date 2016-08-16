using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenInstitutionListAndInstitutionCollection
    {
        public static InstitutionCollection TranslateInstitutionsToInstitutions(Cpchs.Eresults.Common.WCF.BusinessEntities.InstitutionList from)
        {
            InstitutionCollection to = new InstitutionCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Institution inst in from.Items)
            {
                to.Add(TranslateBetweenInstitutionBEAndInstitutionDC.TranslateInstitutionToInstitution(inst));
            }
            return to;
        }
    }
}