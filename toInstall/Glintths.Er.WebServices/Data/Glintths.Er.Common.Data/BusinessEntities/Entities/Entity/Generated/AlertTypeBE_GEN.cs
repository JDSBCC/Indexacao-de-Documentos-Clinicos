using System;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using CPCHS.Common.BusinessEntities;
using System.Runtime.Serialization;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: quarta-feira, 23 de Dezembro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class AlertType_GEN : AbstractEntity
    {	
		#region Variables
		
		private long alertTypeId; ///
		private string alertTypeCode = string.Empty; ///
		private string alertTypeAcronym = string.Empty; ///
		private string alertTypeDesc = string.Empty; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty AlertType object.
        /// </summary>
        public AlertType_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty AlertType object.
        /// </summary>
        public AlertType_GEN(long alertTypeId) 
			: base(ObjectState.Added, null) 
        {
			this.alertTypeId = alertTypeId;
        }
		/// <summary>
        /// Initialize an new empty AlertType object.
        /// </summary>
        public AlertType_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  AlertType object with the given parameters.
        /// </summary>
        public  AlertType_GEN(string alertTypeCode, string alertTypeAcronym, string alertTypeDesc) 
			: base(ObjectState.Added, null) 
        {	 
			this.alertTypeCode = alertTypeCode;
			this.alertTypeAcronym = alertTypeAcronym;
			this.alertTypeDesc = alertTypeDesc;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long AlertTypeId
        {
            get { return this.alertTypeId; }
            set { 
				if(this.alertTypeId != value) {
					DataStateChanged(ObjectState.Modified, "AlertTypeId");
            		this.alertTypeId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string AlertTypeCode
        {
            get { return this.alertTypeCode; }
            set { 
				if(this.alertTypeCode != value) {
					DataStateChanged(ObjectState.Modified, "AlertTypeCode");
            		this.alertTypeCode = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string AlertTypeAcronym
        {
            get { return this.alertTypeAcronym; }
            set { 
				if(this.alertTypeAcronym != value) {
					DataStateChanged(ObjectState.Modified, "AlertTypeAcronym");
            		this.alertTypeAcronym = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string AlertTypeDesc
        {
            get { return this.alertTypeDesc; }
            set { 
				if(this.alertTypeDesc != value) {
					DataStateChanged(ObjectState.Modified, "AlertTypeDesc");
            		this.alertTypeDesc = value;
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
						case "ALERTTYPEID":
							if (!reader.IsDBNull(i)) this.alertTypeId = reader.GetInt64(i);
							break;
						case "ALERTTYPECODE":
							if (!reader.IsDBNull(i)) this.alertTypeCode = Convert.ToString(reader.GetValue(i));
							break;
						case "ALERTTYPEACRONYM":
							if (!reader.IsDBNull(i)) this.alertTypeAcronym = Convert.ToString(reader.GetValue(i));
							break;
						case "ALERTTYPEDESC":
							if (!reader.IsDBNull(i)) this.alertTypeDesc = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			AlertType alerttype = obj as AlertType;
			if (alerttype == null)
				return false;
			return alerttype.AlertTypeId == AlertTypeId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}

