using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPlaceListAndPlaceCollection
    {
        public static PlaceCollection TranslatePlacesToPlaces(PlaceList from)
        {
            PlaceCollection to = new PlaceCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Place place in from.Items)
            {
                to.Add(TranslateBetweenPlaceBEAndPlaceDC.TranslatePlaceToPlace(place));
            }
            return to;
        }
    }
}