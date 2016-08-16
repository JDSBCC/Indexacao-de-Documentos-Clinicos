using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenGenderListAndGenderCollection
    {
        public static GenderCollection TranslateGenresToGenres(GenderList from)
        {
            GenderCollection to = new GenderCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Gender gender in from.Items)
            {
                to.Add(TranslateBetweenGenderBEAndGenderDC.TranslateGenderToGender(gender));
            }
            return to;
        }
    }
}