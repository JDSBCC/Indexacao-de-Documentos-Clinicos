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
    /// Date Created: ter?a-feira, 10 de Julho de 2012
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract(Name = "AntibRes_GEN", Namespace = "http://glintt.com/types")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class AntibRes_GEN : AbstractEntity
    {	
		#region Variables
		
		private long id = 0; ///
		private long microResId = 0; ///
		private string sens = string.Empty; ///
		
		private Codif antib = new Codif();
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty AntibRes object.
        /// </summary>
        public AntibRes_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty AntibRes object.
        /// </summary>
        public AntibRes_GEN(long id) 
			: base(ObjectState.Added , null) 
        {
			this.id = id;
        }
		/// <summary>
        /// Initialize an new empty AntibRes object.
        /// </summary>
        public AntibRes_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, reader, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  AntibRes object with the given parameters.
        /// </summary>
        public  AntibRes_GEN(long microResId, string sens) 
			: base(ObjectState.Added, null) 
        {	 
			this.microResId = microResId;
			this.sens = sens;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long Id
        {
            get { return this.id; }
            set { 
				if(this.id != value) {
					this.id = value;
					DataStateChanged(ObjectState.Modified, "Id");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long MicroResId
        {
            get { return this.microResId; }
            set { 
				if(this.microResId != value) {
					this.microResId = value;
					DataStateChanged(ObjectState.Modified, "MicroResId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Sens
        {
            get { return this.sens; }
            set { 
				if(this.sens != value) {
					this.sens = value;
					DataStateChanged(ObjectState.Modified, "Sens");
				}
			}
		}
		
		
		
		[DataMember]
		public Codif Antib
		{
			get { return this.antib; }
			set { 
				if(this.antib != value) {
					DataStateChanged(ObjectState.Modified, "Antib");
            				this.antib = value;
				}
			}
		}
		
		
		#endregion
	
		#region Methods
		
		protected void LoadAntibRes_GEN(IDataReader reader, string companyDb)
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
						case "ID":
							if (!reader.IsDBNull(i)) this.id = reader.GetInt64(i);
							break;
						case "MICRORESID":
							if (!reader.IsDBNull(i)) this.microResId = reader.GetInt64(i);
							break;
						case "SENS":
							if (!reader.IsDBNull(i)) this.sens = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			AntibRes antibres = obj as AntibRes;
			if (antibres == null)
				return false;
			return antibres.Id == Id;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


