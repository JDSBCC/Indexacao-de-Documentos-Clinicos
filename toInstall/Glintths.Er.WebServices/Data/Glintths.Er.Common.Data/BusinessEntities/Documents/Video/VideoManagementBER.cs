
using System;
using System.Data;
using System.Collections;

using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;
	
using CPCHS.Common.BusinessEntities;

using Microsoft.Practices.EnterpriseLibrary.Caching;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: quinta-feira, 7 de Outubro de 2010
    /// Created By: Generated by CodeSmith
		/// Template Created By: CPCHS psilva, 2005
    /// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public sealed class VideoManagementBER : VideoManagementBER_GEN
    {
		#region Singleton
		private static VideoManagementBER instance = new VideoManagementBER();
		
		public static VideoManagementBER Instance
		{
			get { return instance; }
		}
		#endregion
		private VideoManagementBER()
		{
			
		}


        protected override string GetVideosByDocumentIdDBPackageName
        {
            get { return "PCK_DOCUMENTS_VIDEO"; }
        }

        protected override string GetVideoLinksByDocumentIdDBPackageName
        {
            get { return "PCK_DOCUMENTS_VIDEO"; }
        }
	}
}

