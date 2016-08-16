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
    /// Date Created: ter?a-feira, 7 de Julho de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class PatientLocal : PatientLocal_GEN
	{
		
        /// <summary>
        /// Initialize an new empty PatientLocal object.
        /// </summary>
        public PatientLocal() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty PatientLocal object.
        /// </summary>
        public PatientLocal(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
        }
		
        /// <summary>
        /// Initialize a new  PatientLocal object with the given parameters.
        /// </summary>
        public  PatientLocal(long patientLocalId, long patientLocalParentId, long patientLocalEntId, long patientLocalInstId, long patientLocalLocalId, long patientLocalPatTypeId, string patientLocalPat, string patientLocalActive) : base(patientLocalId, patientLocalParentId, patientLocalEntId, patientLocalInstId, patientLocalLocalId, patientLocalPatTypeId, patientLocalPat, patientLocalActive)
        {
        }
		
        public PatientLocal(long patientLocalId) : base(patientLocalId)
        {
			
        }
	}
}

