using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using CPCHS.Common.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;
	
namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: segunda-feira, 6 de Outubro de 2008
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class Service : Service_GEN
	{
		
        /// <summary>
        /// Initialize an new empty Service object.
        /// </summary>
        public Service() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty Service object.
        /// </summary>
        public Service(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
        }
		
        /// <summary>
        /// Initialize a new  Service object with the given parameters.
        /// </summary>
        public Service(long serviceId, string serviceCode, string serviceAcronym, string serviceDescription)
            : base(serviceId, serviceCode, serviceAcronym, serviceDescription)
        {
        }
		
        public Service(long serviceId) : base(serviceId)
        {
			
        }
	}
}


