using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentTypeBEAndDocumentTypeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.DocumentType TranslateDocumentTypeToDocumentType(Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentType from)
        {
            Cpchs.Entities.WCF.DataContracts.DocumentType to = new Cpchs.Entities.WCF.DataContracts.DocumentType();
            to.Id = from.DocumentTypeId;
            to.Code = from.DocumentTypeCode;
            to.Acronym = from.DocumentTypeAcronym;
            to.Description = from.DocumentTypeDescription;
            to.InstitutionId = from.DocumentTypeInstitutionId;
            to.PlaceId = from.DocumentTypePlaceId;
            to.ApplicationId = from.DocumentTypeApplicationId;
            to.Archive = from.DocumentTypeArch;
            to.AnalyticalResult = from.DocumentTypeOrigin;
            to.Link = from.DocumentTypeLinks;
            to.HasForm = from.HasForm;
            to.FormDescription = from.FormDescription;
            to.MandatoryForm = from.MandatoryForm;
            to.Childs = TranslateBetweenDocumentListAndDocumentCollection.TranslateDocumentsToDocuments(from.DocumentTypeChilds);
            return to;
        }
    }
}