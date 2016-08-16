using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentListAndDocumentCollection
    {
        public static DocumentTypeCollection TranslateDocumentsToDocuments(Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentTypeList from)
        {
            DocumentTypeCollection to = new DocumentTypeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentType docType in from.Items)
            {
                to.Add(TranslateBetweenDocumentTypeBEAndDocumentTypeDC.TranslateDocumentTypeToDocumentType(docType));
            }
            return to;
        }
    }
}
