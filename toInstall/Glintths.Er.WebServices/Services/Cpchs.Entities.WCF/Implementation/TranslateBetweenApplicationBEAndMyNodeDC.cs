using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenApplicationBEAndMyNodeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.MyNode TranslateApplicationToMyNode(Cpchs.Eresults.Common.WCF.BusinessEntities.Application from)
        {
            Cpchs.Entities.WCF.DataContracts.MyNode to = new Cpchs.Entities.WCF.DataContracts.MyNode();
            to.MyNodeDescription = from.ApplicationDescription;
            to.MyNodeOriginalId = from.ApplicationId;
            to.MyNodeIds = null;
            to.MyNodeChilds = TranslateBetweenDocumentTypeListAndMyNodeCollection.TranslateDocumentTypesToMyNodes(from.ApplicationDocumentTypes);
            return to;
        }
    }
}

