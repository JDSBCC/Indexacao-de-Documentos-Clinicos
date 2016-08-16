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
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: sexta-feira, 25 de Junho de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class Element_GEN : AbstractEntity
    {	
		#region Variables
		
		private long elementId = 0; ///
		private long elementVersion = 0; ///
		private string elementActiveVersion = string.Empty; ///
		private long elementDocUniqueId = 0; ///
		private long elementOrder = 0; ///
		private string elementTitle = string.Empty; ///
		private string elementDescription = string.Empty; ///
		private string elementStatus = string.Empty; ///
		private DateTime? elementDate = null; ///
		private string elementExternalId = string.Empty; ///
		private long elementTypeId = 0; ///
		private string elementPublic = string.Empty; ///
		private long elementStatusId = 0; ///
		private DateTime? elementExecutionDate = null; ///
		private DateTime? elementValidationDate = null; ///
		private DateTime? elementEmissionDate = null; ///
		private string elementReport = string.Empty; ///
		private long elementReportPresOrder = 0; ///
		
		private ElementType elementTypeBE;
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty Element object.
        /// </summary>
        public Element_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty Element object.
        /// </summary>
        public Element_GEN(long elementId, long elementVersion) 
			: base(ObjectState.Added , null) 
        {
			this.elementId = elementId;
			this.elementVersion = elementVersion;
        }
		/// <summary>
        /// Initialize an new empty Element object.
        /// </summary>
        public Element_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  Element object with the given parameters.
        /// </summary>
        public  Element_GEN(long elementId, long elementVersion, string elementActiveVersion, long elementDocUniqueId, long elementOrder, string elementTitle, string elementDescription, string elementStatus, DateTime? elementDate, string elementExternalId, long elementTypeId, string elementPublic, long elementStatusId, DateTime? elementExecutionDate, DateTime? elementValidationDate, DateTime? elementEmissionDate, string elementReport, long elementReportPresOrder) 
			: base(ObjectState.Added, null) 
        {	 
			this.elementId = elementId;
			this.elementVersion = elementVersion;
			this.elementActiveVersion = elementActiveVersion;
			this.elementDocUniqueId = elementDocUniqueId;
			this.elementOrder = elementOrder;
			this.elementTitle = elementTitle;
			this.elementDescription = elementDescription;
			this.elementStatus = elementStatus;
			this.elementDate = elementDate;
			this.elementExternalId = elementExternalId;
			this.elementTypeId = elementTypeId;
			this.elementPublic = elementPublic;
			this.elementStatusId = elementStatusId;
			this.elementExecutionDate = elementExecutionDate;
			this.elementValidationDate = elementValidationDate;
			this.elementEmissionDate = elementEmissionDate;
			this.elementReport = elementReport;
			this.elementReportPresOrder = elementReportPresOrder;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementId
        {
            get { return this.elementId; }
            set { 
				if(this.elementId != value) {
					DataStateChanged(ObjectState.Modified, "ElementId");
            		this.elementId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementVersion
        {
            get { return this.elementVersion; }
            set { 
				if(this.elementVersion != value) {
					DataStateChanged(ObjectState.Modified, "ElementVersion");
            		this.elementVersion = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementActiveVersion
        {
            get { return this.elementActiveVersion; }
            set { 
				if(this.elementActiveVersion != value) {
					DataStateChanged(ObjectState.Modified, "ElementActiveVersion");
            		this.elementActiveVersion = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementDocUniqueId
        {
            get { return this.elementDocUniqueId; }
            set { 
				if(this.elementDocUniqueId != value) {
					DataStateChanged(ObjectState.Modified, "ElementDocUniqueId");
            		this.elementDocUniqueId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementOrder
        {
            get { return this.elementOrder; }
            set { 
				if(this.elementOrder != value) {
					DataStateChanged(ObjectState.Modified, "ElementOrder");
            		this.elementOrder = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementTitle
        {
            get { return this.elementTitle; }
            set { 
				if(this.elementTitle != value) {
					DataStateChanged(ObjectState.Modified, "ElementTitle");
            		this.elementTitle = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementDescription
        {
            get { return this.elementDescription; }
            set { 
				if(this.elementDescription != value) {
					DataStateChanged(ObjectState.Modified, "ElementDescription");
            		this.elementDescription = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementStatus
        {
            get { return this.elementStatus; }
            set { 
				if(this.elementStatus != value) {
					DataStateChanged(ObjectState.Modified, "ElementStatus");
            		this.elementStatus = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public DateTime? ElementDate
        {
            get { return this.elementDate; }
            set { 
				if(!this.elementDate.Equals(value)) {
					DataStateChanged(ObjectState.Modified, "ElementDate");
					this.elementDate = value;
				}
		}

		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementExternalId
        {
            get { return this.elementExternalId; }
            set { 
				if(this.elementExternalId != value) {
					DataStateChanged(ObjectState.Modified, "ElementExternalId");
            		this.elementExternalId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementTypeId
        {
            get { return this.elementTypeId; }
            set { 
				if(this.elementTypeId != value) {
					DataStateChanged(ObjectState.Modified, "ElementTypeId");
            		this.elementTypeId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementPublic
        {
            get { return this.elementPublic; }
            set { 
				if(this.elementPublic != value) {
					DataStateChanged(ObjectState.Modified, "ElementPublic");
            		this.elementPublic = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementStatusId
        {
            get { return this.elementStatusId; }
            set { 
				if(this.elementStatusId != value) {
					DataStateChanged(ObjectState.Modified, "ElementStatusId");
            		this.elementStatusId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public DateTime? ElementExecutionDate
        {
            get { return this.elementExecutionDate; }
            set { 
				if(!this.elementExecutionDate.Equals(value)) {
					DataStateChanged(ObjectState.Modified, "ElementExecutionDate");
					this.elementExecutionDate = value;
				}
		}

		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public DateTime? ElementValidationDate
        {
            get { return this.elementValidationDate; }
            set { 
				if(!this.elementValidationDate.Equals(value)) {
					DataStateChanged(ObjectState.Modified, "ElementValidationDate");
					this.elementValidationDate = value;
				}
		}

		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public DateTime? ElementEmissionDate
        {
            get { return this.elementEmissionDate; }
            set { 
				if(!this.elementEmissionDate.Equals(value)) {
					DataStateChanged(ObjectState.Modified, "ElementEmissionDate");
					this.elementEmissionDate = value;
				}
		}

		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string ElementReport
        {
            get { return this.elementReport; }
            set { 
				if(this.elementReport != value) {
					DataStateChanged(ObjectState.Modified, "ElementReport");
            		this.elementReport = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElementReportPresOrder
        {
            get { return this.elementReportPresOrder; }
            set { 
				if(this.elementReportPresOrder != value) {
					DataStateChanged(ObjectState.Modified, "ElementReportPresOrder");
            		this.elementReportPresOrder = value;
				}
			}
		}
		
		
		
		[DataMember]
		public ElementType ElementTypeBE
		{
			get { return this.elementTypeBE; }
			set { 
				if(this.elementTypeBE != value) {
					DataStateChanged(ObjectState.Modified, "ElementTypeBE");
            				this.elementTypeBE = value;
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
						case "ELEMENTID":
							if (!reader.IsDBNull(i)) this.elementId = reader.GetInt64(i);
							break;
						case "ELEMENTVERSION":
							if (!reader.IsDBNull(i)) this.elementVersion = reader.GetInt64(i);
							break;
						case "ELEMENTACTIVEVERSION":
							if (!reader.IsDBNull(i)) this.elementActiveVersion = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTDOCUNIQUEID":
							if (!reader.IsDBNull(i)) this.elementDocUniqueId = reader.GetInt64(i);
							break;
						case "ELEMENTORDER":
							if (!reader.IsDBNull(i)) this.elementOrder = reader.GetInt64(i);
							break;
						case "ELEMENTTITLE":
							if (!reader.IsDBNull(i)) this.elementTitle = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTDESCRIPTION":
							if (!reader.IsDBNull(i)) this.elementDescription = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTSTATUS":
							if (!reader.IsDBNull(i)) this.elementStatus = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTDATE":
							if (!reader.IsDBNull(i)) this.elementDate = reader.GetDateTime(i);
							break;
						case "ELEMENTEXTERNALID":
							if (!reader.IsDBNull(i)) this.elementExternalId = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTTYPEID":
							if (!reader.IsDBNull(i)) this.elementTypeId = reader.GetInt64(i);
							break;
						case "ELEMENTPUBLIC":
							if (!reader.IsDBNull(i)) this.elementPublic = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTSTATUSID":
							if (!reader.IsDBNull(i)) this.elementStatusId = reader.GetInt64(i);
							break;
						case "ELEMENTEXECUTIONDATE":
							if (!reader.IsDBNull(i)) this.elementExecutionDate = reader.GetDateTime(i);
							break;
						case "ELEMENTVALIDATIONDATE":
							if (!reader.IsDBNull(i)) this.elementValidationDate = reader.GetDateTime(i);
							break;
						case "ELEMENTEMISSIONDATE":
							if (!reader.IsDBNull(i)) this.elementEmissionDate = reader.GetDateTime(i);
							break;
						case "ELEMENTREPORT":
							if (!reader.IsDBNull(i)) this.elementReport = Convert.ToString(reader.GetValue(i));
							break;
						case "ELEMENTREPORTPRESORDER":
							if (!reader.IsDBNull(i)) this.elementReportPresOrder = reader.GetInt64(i);
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			Element element = obj as Element;
			if (element == null)
				return false;
			return element.ElementVersion == ElementVersion;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}

