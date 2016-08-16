using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPlaceListAndMyNodeCollection
    {
        public static MyNodeCollection TranslatePlacesToMyNodes(Cpchs.Eresults.Common.WCF.BusinessEntities.PlaceList from)
        {
            MyNodeCollection to = new MyNodeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Place place in from.Items)
            {
                to.Add(TranslateBetweenPlaceBEAndMyNodeDC.TranslatePlaceToMyNode(place));
            }
            return to;
        }
    }
}
