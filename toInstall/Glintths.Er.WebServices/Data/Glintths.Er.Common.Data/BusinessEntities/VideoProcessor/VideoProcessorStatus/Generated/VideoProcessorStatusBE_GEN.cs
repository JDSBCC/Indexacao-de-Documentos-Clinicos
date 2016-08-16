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
    /// Date Created: quarta-feira, 27 de Outubro de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class VideoProcessorStatus_GEN : AbstractEntity
    {	
		#region Variables
		
		private long statusId = 0; ///
		private string statusName = string.Empty; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty VideoProcessorStatus object.
        /// </summary>
        public VideoProcessorStatus_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty VideoProcessorStatus object.
        /// </summary>
        public VideoProcessorStatus_GEN(long statusId) 
			: base(ObjectState.Added , null) 
        {
			this.statusId = statusId;
        }
		/// <summary>
        /// Initialize an new empty VideoProcessorStatus object.
        /// </summary>
        public VideoProcessorStatus_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  VideoProcessorStatus object with the given parameters.
        /// </summary>
        public  VideoProcessorStatus_GEN(long statusId, string statusName) 
			: base(ObjectState.Added, null) 
        {	 
			this.statusId = statusId;
			this.statusName = statusName;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long StatusId
        {
            get { return this.statusId; }
            set { 
				if(this.statusId != value) {
					this.statusId = value;
					DataStateChanged(ObjectState.Modified, "StatusId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string StatusName
        {
            get { return this.statusName; }
            set { 
				if(this.statusName != value) {
					this.statusName = value;
					DataStateChanged(ObjectState.Modified, "StatusName");
				}
			}
		}
		
		
		
		#endregion
	
		#region Methods
		
		protected void LoadVideoProcessorStatus_GEN(IDataReader reader, string companyDb)
        {
			base.CompanyDB = companyDb;
            base.ObjectState = CPCHS.Common.BusinessEntities.ObjectState.Unchanged;

			LoadFromReader(reader);
        }
	
		private void LoadFromReader(IDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
            {	
				for(int i=0; i<reader.FieldCount; i++) {
					switch(reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) {
						case "STATUSID":
							if (!reader.IsDBNull(i)) this.statusId = reader.GetInt64(i);
							break;
						case "STATUSNAME":
							if (!reader.IsDBNull(i)) this.statusName = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			VideoProcessorStatus videoprocessorstatus = obj as VideoProcessorStatus;
			if (videoprocessorstatus == null)
				return false;
			return videoprocessorstatus.StatusId == StatusId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


