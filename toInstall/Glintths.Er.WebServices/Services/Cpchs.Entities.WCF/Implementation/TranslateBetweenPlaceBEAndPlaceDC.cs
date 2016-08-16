using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPlaceBEAndPlaceDC
    {
        public static Cpchs.Entities.WCF.DataContracts.Place TranslatePlaceToPlace(Cpchs.Eresults.Common.WCF.BusinessEntities.Place from)
        {
            Cpchs.Entities.WCF.DataContracts.Place to = new Cpchs.Entities.WCF.DataContracts.Place();
            to.Id = from.PlaceId;
            to.Code = from.PlaceCode;
            to.Acronym = from.PlaceAcronym;
            to.Description = from.PlaceDescription;
            to.Applications = TranslateBetweenApplicationListAndApplicationCollection.TranslateApplicationsToApplications(from.PlaceApplicationList);
            return to;
        }
    }
}