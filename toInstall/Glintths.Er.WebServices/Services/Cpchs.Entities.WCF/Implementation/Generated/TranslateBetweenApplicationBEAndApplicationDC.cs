using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenApplicationBEAndApplicationDC
    {
        public static Cpchs.Entities.WCF.DataContracts.Application TranslateApplicationToApplication(Cpchs.Eresults.Common.WCF.BusinessEntities.Application from)
        {
            Cpchs.Entities.WCF.DataContracts.Application to = new Cpchs.Entities.WCF.DataContracts.Application();
            to.Id = from.ApplicationId;
            to.Code = from.ApplicationCode;
            to.Acronym = from.ApplicationAcronym;
            to.Description = from.ApplicationDescription;
            to.DocumentTypes = TranslateBetweenDocumentListAndDocumentCollection.TranslateDocumentsToDocuments(from.ApplicationDocumentTypes);
            return to;
        }
    }
}