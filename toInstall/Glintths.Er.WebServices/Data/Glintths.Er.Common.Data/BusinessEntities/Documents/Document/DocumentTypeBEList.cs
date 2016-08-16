using CPCHS.Common.BusinessEntities;
using System;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{	
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), Serializable]
    public class DocumentTypeList: GenericList< DocumentType >
    {
    }

    public class DocumentTypeComparer : System.Collections.Generic.IComparer<DocumentType>
    {
        public int Compare(DocumentType x, DocumentType y)
        {
            if (x.DocumentTypeId < 1 && y.DocumentTypeId > 0)
            { return -1; }
            else if (x.DocumentTypeId > 0 && y.DocumentTypeId < 1)
            { return 1; }
            return string.Compare(x.DocumentTypeDescription, y.DocumentTypeDescription);
        }
    }
}
