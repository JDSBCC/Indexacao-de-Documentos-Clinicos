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
using Cpchs.Eresults.Common.WCF;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: quarta-feira, 23 de Dezembro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class Alert_GEN : AbstractEntity
    {	
		#region Variables
		
		private long alertId; ///
		private long alertSubsId; ///
		private Nullable<DateTime> alertDate = new Nullable<DateTime>(); ///
		private long alertStatusId; ///
		
		private AlertSubscription alertSubscription;
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty Alert object.
        /// </summary>
        public Alert_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty Alert object.
        /// </summary>
        public Alert_GEN(long alertId) 
			: base(ObjectState.Added, null) 
        {
			this.alertId = alertId;
        }
		/// <summary>
        /// Initialize an new empty Alert object.
        /// </summary>
        public Alert_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  Alert object with the given parameters.
        /// </summary>
        public  Alert_GEN(long alertSubsId, Nullable<DateTime> alertDate, long alertStatusId) 
			: base(ObjectState.Added, null) 
        {	 
			this.alertSubsId = alertSubsId;
			this.alertDate = alertDate;
			this.alertStatusId = alertStatusId;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long AlertId
        {
            get { return this.alertId; }
            set { 
				if(this.alertId != value) {
					DataStateChanged(ObjectState.Modified, "AlertId");
            		this.alertId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long AlertSubsId
        {
            get { return this.alertSubsId; }
            set { 
				if(this.alertSubsId != value) {
					DataStateChanged(ObjectState.Modified, "AlertSubsId");
            		this.alertSubsId = value;
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public Nullable<DateTime> AlertDate
        {
            get { return this.alertDate; }
            set { 
				if(!this.alertDate.Equals(value)) {
					DataStateChanged(ObjectState.Modified, "AlertDate");
					this.alertDate = value;
				}
		}

		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long AlertStatusId
        {
            get { return this.alertStatusId; }
            set { 
				if(this.alertStatusId != value) {
					DataStateChanged(ObjectState.Modified, "AlertStatusId");
            		this.alertStatusId = value;
				}
			}
		}
		
		
		
		[DataMember]
		public AlertSubscription AlertSubscription
		{
			get { return this.alertSubscription; }
			set { 
				if(this.alertSubscription != value) {
					DataStateChanged(ObjectState.Modified, "AlertSubscription");
            				this.alertSubscription = value;
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
						case "ALERTID":
							if (!reader.IsDBNull(i)) this.alertId = reader.GetInt64(i);
							break;
						case "ALERTSUBSID":
							if (!reader.IsDBNull(i)) this.alertSubsId = reader.GetInt64(i);
							break;
						case "ALERTDATE":
							if (!reader.IsDBNull(i)) this.alertDate = reader.GetDateTime(i);
							break;
						case "ALERTSTATUSID":
							if (!reader.IsDBNull(i)) this.alertStatusId = reader.GetInt64(i);
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			Alert alert = obj as Alert;
			if (alert == null)
				return false;
			return alert.AlertId == AlertId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}

