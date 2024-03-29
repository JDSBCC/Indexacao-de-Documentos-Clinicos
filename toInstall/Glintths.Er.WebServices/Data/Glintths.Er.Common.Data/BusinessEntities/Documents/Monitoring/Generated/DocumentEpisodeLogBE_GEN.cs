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
    /// Date Created: ter?a-feira, 20 de Outubro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class DocumentEpisodeLog_GEN : AbstractEntity
    {	
		#region Variables
	
		
		private long docEpiLogId; ///
		private long docEpiLogDocLogId; ///
		private long docEpiLogEpiTypeId; ///
		private string docEpiLogEpiId; ///
		private long docEpiLogInstId; ///
		private long docEpiLogPlaceId; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty DocumentEpisodeLog object.
        /// </summary>
        public DocumentEpisodeLog_GEN() : base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty DocumentEpisodeLog object.
        /// </summary>
        public DocumentEpisodeLog_GEN(long docEpiLogId) : base(ObjectState.Added, null) 
        {
			this.docEpiLogId = docEpiLogId;
        }
		/// <summary>
        /// Initialize an new empty DocumentEpisodeLog object.
        /// </summary>
        public DocumentEpisodeLog_GEN(IDataReader reader, string companyDB) : base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  DocumentEpisodeLog object with the given parameters.
        /// </summary>
        public  DocumentEpisodeLog_GEN(long docEpiLogDocLogId, long docEpiLogEpiTypeId, string docEpiLogEpiId, long docEpiLogInstId, long docEpiLogPlaceId) : base(ObjectState.Added, null) 
        {	 
			this.docEpiLogDocLogId = docEpiLogDocLogId;
			this.docEpiLogEpiTypeId = docEpiLogEpiTypeId;
			this.docEpiLogEpiId = docEpiLogEpiId;
			this.docEpiLogInstId = docEpiLogInstId;
			this.docEpiLogPlaceId = docEpiLogPlaceId;
        }
		#endregion
		
		#region Properties
		

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long DocEpiLogId
        {
            get { return this.docEpiLogId; }
            set { 
				if(this.docEpiLogId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogId");
            		this.docEpiLogId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long DocEpiLogDocLogId
        {
            get { return this.docEpiLogDocLogId; }
            set { 
				if(this.docEpiLogDocLogId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogDocLogId");
            		this.docEpiLogDocLogId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long DocEpiLogEpiTypeId
        {
            get { return this.docEpiLogEpiTypeId; }
            set { 
				if(this.docEpiLogEpiTypeId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogEpiTypeId");
            		this.docEpiLogEpiTypeId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string DocEpiLogEpiId
        {
            get { return this.docEpiLogEpiId; }
            set { 
				if(this.docEpiLogEpiId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogEpiId");
            		this.docEpiLogEpiId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long DocEpiLogInstId
        {
            get { return this.docEpiLogInstId; }
            set { 
				if(this.docEpiLogInstId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogInstId");
            		this.docEpiLogInstId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long DocEpiLogPlaceId
        {
            get { return this.docEpiLogPlaceId; }
            set { 
				if(this.docEpiLogPlaceId != value) {
					DataStateChanged(ObjectState.Modified, "DocEpiLogPlaceId");
            		this.docEpiLogPlaceId = value;
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
						case "DOCEPILOGID":
							if (!reader.IsDBNull(i)) this.docEpiLogId = reader.GetInt64(i);
							break;
						case "DOCEPILOGDOCLOGID":
							if (!reader.IsDBNull(i)) this.docEpiLogDocLogId = reader.GetInt64(i);
							break;
						case "DOCEPILOGEPITYPEID":
							if (!reader.IsDBNull(i)) this.docEpiLogEpiTypeId = reader.GetInt64(i);
							break;
						case "DOCEPILOGEPIID":
							if (!reader.IsDBNull(i)) this.docEpiLogEpiId = Convert.ToString(reader.GetValue(i));
							break;
						case "DOCEPILOGINSTID":
							if (!reader.IsDBNull(i)) this.docEpiLogInstId = reader.GetInt64(i);
							break;
						case "DOCEPILOGPLACEID":
							if (!reader.IsDBNull(i)) this.docEpiLogPlaceId = reader.GetInt64(i);
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			DocumentEpisodeLog documentepisodelog = obj as DocumentEpisodeLog;
			if (documentepisodelog == null)
				return false;
			return documentepisodelog.DocEpiLogId == DocEpiLogId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


