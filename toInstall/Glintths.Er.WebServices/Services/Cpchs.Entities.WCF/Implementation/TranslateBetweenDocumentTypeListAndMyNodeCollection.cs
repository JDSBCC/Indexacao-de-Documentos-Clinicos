using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentTypeListAndMyNodeCollection
    {
        public static MyNodeCollection TranslateDocumentTypesToMyNodes(Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentTypeList from)
        {
            MyNodeCollection to = new MyNodeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentType docType in from.Items)
            {
                to.Add(TranslateBetweenDocumentTypeBEAndMyNodeDC.TranslateDocumentTypeToMyNode(docType));
            }
            return to;
        }
    }
}
