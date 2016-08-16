using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenInstitutionBEAndMyNodeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.MyNode TranslateInstitutionToMyNode(Cpchs.Eresults.Common.WCF.BusinessEntities.Institution from)
        {
            Cpchs.Entities.WCF.DataContracts.MyNode to = new Cpchs.Entities.WCF.DataContracts.MyNode();
            to.MyNodeDescription = from.InstitutionDesc;
            to.MyNodeOriginalId = from.InstitutionId;
            to.MyNodeIds = null;
            to.MyNodeChilds = TranslateBetweenPlaceListAndMyNodeCollection.TranslatePlacesToMyNodes(from.InstitutionPlaceList);
            return to;
        }
    }
}

