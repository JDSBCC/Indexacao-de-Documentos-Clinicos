using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenInstitutionBEAndInstitutionDC
    {
        public static Cpchs.Entities.WCF.DataContracts.Institution TranslateInstitutionToInstitution(Cpchs.Eresults.Common.WCF.BusinessEntities.Institution from)
        {
            Cpchs.Entities.WCF.DataContracts.Institution to = new Cpchs.Entities.WCF.DataContracts.Institution();
            to.Id = from.InstitutionId;
            to.Code = from.InstitutionCode;
            to.Acronym = from.InstitutionAcronym;
            to.Description = from.InstitutionDesc;
            to.Places = TranslateBetweenPlaceListAndPlaceCollection.TranslatePlacesToPlaces(from.InstitutionPlaceList);
            return to;
        }
    }
}