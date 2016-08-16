using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using CPCHS.Common.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;
using System.Runtime.Serialization;
	
namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: quarta-feira, 27 de Outubro de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class VideoProcessorStatus : VideoProcessorStatus_GEN
	{
        /// <summary>
        /// Initialize an new empty VideoProcessorStatus object.
        /// </summary>
        public VideoProcessorStatus() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty VideoProcessorStatus object.
        /// </summary>
        public VideoProcessorStatus(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
        }
		
        /// <summary>
        /// Initialize a new  VideoProcessorStatus object with the given parameters.
        /// </summary>
        public  VideoProcessorStatus(long statusId, string statusName) : base(statusId, statusName)
        {
        }
		
        public VideoProcessorStatus(long statusId) : base(statusId)
        {
			
        }
		
		protected void LoadVideoProcessorStatus(IDataReader reader, string companyDb)
        {
        }
	}
}


