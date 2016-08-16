using CPCHS.Common.BusinessEntities;
using System;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{	
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), Serializable]
    public class ApplicationList: GenericList< Application >
    {
    }

    public class ApplicationComparer : System.Collections.Generic.IComparer<Application>
    {
        public int Compare(Application x, Application y)
        {
            if (x.ApplicationId < 1 && y.ApplicationId > 0)
            { return -1; }
            else if (x.ApplicationId > 0 && y.ApplicationId < 1)
            { return 1; }
            return string.Compare(x.ApplicationDescription, y.ApplicationDescription);
        }
    }
}
