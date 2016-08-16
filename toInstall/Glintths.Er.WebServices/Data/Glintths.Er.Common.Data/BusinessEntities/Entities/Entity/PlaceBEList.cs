using CPCHS.Common.BusinessEntities;
using System;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{	
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), Serializable]
    public class PlaceList: GenericList< Place >
    {
    }

    public class PlaceComparer : System.Collections.Generic.IComparer<Place>
    {
        public int Compare(Place x, Place y)
        {
            if (x.PlaceId < 1 && y.PlaceId > 0)
            { return -1; }
            else if (x.PlaceId > 0 && y.PlaceId < 1)
            { return 1; }
            return string.Compare(x.PlaceDescription, y.PlaceDescription);
        }
    }
}
