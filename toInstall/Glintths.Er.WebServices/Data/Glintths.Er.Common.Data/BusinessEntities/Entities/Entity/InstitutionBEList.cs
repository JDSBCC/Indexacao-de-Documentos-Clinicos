using CPCHS.Common.BusinessEntities;
using System;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{	
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), Serializable]
    public class InstitutionList: GenericList< Institution >
    {        
    }

    public class InstitutionComparer : System.Collections.Generic.IComparer<Institution>
    {
        public int Compare(Institution x, Institution y)
        {
            if (x.InstitutionId < 1 && y.InstitutionId > 0)
            { return -1; }
            else if (x.InstitutionId > 0 && y.InstitutionId < 1)
            { return 1; }
            return string.Compare(x.InstitutionDesc, y.InstitutionDesc);
        }
    }
}
