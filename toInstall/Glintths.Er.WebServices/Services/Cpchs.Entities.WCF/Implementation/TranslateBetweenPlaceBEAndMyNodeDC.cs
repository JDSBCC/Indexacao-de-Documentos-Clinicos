using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPlaceBEAndMyNodeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.MyNode TranslatePlaceToMyNode(Cpchs.Eresults.Common.WCF.BusinessEntities.Place from)
        {
            Cpchs.Entities.WCF.DataContracts.MyNode to = new Cpchs.Entities.WCF.DataContracts.MyNode();
            to.MyNodeDescription = from.PlaceDescription;
            to.MyNodeOriginalId = from.PlaceId;
            to.MyNodeIds = null;
            to.MyNodeChilds = TranslateBetweenApplicationListAndMyNodeCollection.TranslateApplicationsToMyNodes(from.PlaceApplicationList);
            return to;
        }
    }
}

