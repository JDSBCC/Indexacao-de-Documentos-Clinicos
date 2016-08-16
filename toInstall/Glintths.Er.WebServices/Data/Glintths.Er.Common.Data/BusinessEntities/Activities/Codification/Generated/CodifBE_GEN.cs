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
    /// Date Created: quinta-feira, 5 de Julho de 2012
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract(Name = "Codif_GEN", Namespace = "http://glintt.com/types")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class Codif_GEN : AbstractEntity
    {	
		#region Variables
		
		private string code = string.Empty; ///
		private string descr = string.Empty; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty Codif object.
        /// </summary>
        public Codif_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty Codif object.
        /// </summary>
		/// <summary>
        /// Initialize an new empty Codif object.
        /// </summary>
        public Codif_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, reader, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  Codif object with the given parameters.
        /// </summary>
        public  Codif_GEN(string code, string descr) 
			: base(ObjectState.Added, null) 
        {	 
			this.code = code;
			this.descr = descr;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Code
        {
            get { return this.code; }
            set { 
				if(this.code != value) {
					this.code = value;
					DataStateChanged(ObjectState.Modified, "Code");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Descr
        {
            get { return this.descr; }
            set { 
				if(this.descr != value) {
					this.descr = value;
					DataStateChanged(ObjectState.Modified, "Descr");
				}
			}
		}
		
		
		
		#endregion
	
		#region Methods
		
		protected void LoadCodif_GEN(IDataReader reader, string companyDb)
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
						case "CODE":
							if (!reader.IsDBNull(i)) this.code = Convert.ToString(reader.GetValue(i));
							break;
						case "DESCR":
							if (!reader.IsDBNull(i)) this.descr = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


