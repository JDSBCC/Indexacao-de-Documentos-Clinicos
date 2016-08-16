using System;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Globalization;

using Microsoft.Practices.EnterpriseLibrary.Logging;
	
using CPCHS.Common.BusinessEntities;
	

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: ter?a-feira, 7 de Julho de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class Service_GEN : AbstractEntity
    {	
		#region Variables
	
		
		private long serviceId; ///
		private string serviceCode; ///
		private string serviceAcronym; ///
		private string serviceDescription; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty Service object.
        /// </summary>
        public Service_GEN() : base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty Service object.
        /// </summary>
        public Service_GEN(long serviceId) : base(ObjectState.Added, null) 
        {
			this.serviceId = serviceId;
        }
		/// <summary>
        /// Initialize an new empty Service object.
        /// </summary>
        public Service_GEN(IDataReader reader, string companyDB) : base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  Service object with the given parameters.
        /// </summary>
        public  Service_GEN(long serviceId, string serviceCode, string serviceAcronym, string serviceDescription) : base(ObjectState.Added, null) 
        {	 
			this.serviceId = serviceId;
			this.serviceCode = serviceCode;
			this.serviceAcronym = serviceAcronym;
			this.serviceDescription = serviceDescription;
        }
		#endregion
		
		#region Properties
		

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ServiceId
        {
            get { return this.serviceId; }
            set { 
				if(this.serviceId != value) {
					DataStateChanged(ObjectState.Modified, "ServiceId");
            		this.serviceId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ServiceCode
        {
            get { return this.serviceCode; }
            set { 
				if(this.serviceCode != value) {
					DataStateChanged(ObjectState.Modified, "ServiceCode");
            		this.serviceCode = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ServiceAcronym
        {
            get { return this.serviceAcronym; }
            set { 
				if(this.serviceAcronym != value) {
					DataStateChanged(ObjectState.Modified, "ServiceAcronym");
            		this.serviceAcronym = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ServiceDescription
        {
            get { return this.serviceDescription; }
            set { 
				if(this.serviceDescription != value) {
					DataStateChanged(ObjectState.Modified, "ServiceDescription");
            		this.serviceDescription = value;
				}
			}
		}
		
		
		
		#endregion
	
		#region Methods
	
		private void LoadFromReader(IDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
            {	
				for(int i=0; i<reader.FieldCount; i++) {
					switch(reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) {
						case "SERVICEID":
							if (!reader.IsDBNull(i)) this.serviceId = reader.GetInt64(i);
							break;
						case "SERVICECODE":
							if (!reader.IsDBNull(i)) this.serviceCode = Convert.ToString(reader.GetValue(i));
							break;
						case "SERVICEACRONYM":
							if (!reader.IsDBNull(i)) this.serviceAcronym = Convert.ToString(reader.GetValue(i));
							break;
						case "SERVICEDESCRIPTION":
							if (!reader.IsDBNull(i)) this.serviceDescription = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			Service service = obj as Service;
			if (service == null)
				return false;
			return service.ServiceId == ServiceId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


