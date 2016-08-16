using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using CPCHS.Common.BusinessEntities;
using Glintths.Er.Common.BusinessEntities.Generated;
using System.Runtime.Serialization;
	
namespace Glintths.Er.Common.BusinessEntities
{
    /// <summary>
    /// Date Created: quarta-feira, 25 de Agosto de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class Entity : Entity_GEN
	{
        /// <summary>
        /// Initialize an new empty Entity object.
        /// </summary>
        public Entity() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty Entity object.
        /// </summary>
        public Entity(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
        }
		
        /// <summary>
        /// Initialize a new  Entity object with the given parameters.
        /// </summary>
        public  Entity(string entName, string entTaxPayerNum, string entAddress, string entPlace, string entPostalCode, string entPhoneNum1, string entPhoneNum2, string entFaxNum) : base(entName, entTaxPayerNum, entAddress, entPlace, entPostalCode, entPhoneNum1, entPhoneNum2, entFaxNum)
        {
        }
		
	}
}

