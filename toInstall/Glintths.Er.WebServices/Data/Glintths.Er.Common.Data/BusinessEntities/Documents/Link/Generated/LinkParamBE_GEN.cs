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
    /// Date Created: quarta-feira, 14 de Outubro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class LinkParam_GEN : AbstractEntity
    {	
		#region Variables
	
		
		private long linkParamElemId; ///
		private long linkParamVersionCode; ///
		private long linkParamId; ///
		private string linkParamArg; ///
		private string linkParamValue; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty LinkParam object.
        /// </summary>
        public LinkParam_GEN() : base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty LinkParam object.
        /// </summary>
        public LinkParam_GEN(long linkParamElemId, long linkParamVersionCode, long linkParamId) : base(ObjectState.Added, null) 
        {
			this.linkParamElemId = linkParamElemId;
			this.linkParamVersionCode = linkParamVersionCode;
			this.linkParamId = linkParamId;
        }
		/// <summary>
        /// Initialize an new empty LinkParam object.
        /// </summary>
        public LinkParam_GEN(IDataReader reader, string companyDB) : base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  LinkParam object with the given parameters.
        /// </summary>
        public  LinkParam_GEN(long linkParamElemId, long linkParamVersionCode, long linkParamId, string linkParamArg, string linkParamValue) : base(ObjectState.Added, null) 
        {	 
			this.linkParamElemId = linkParamElemId;
			this.linkParamVersionCode = linkParamVersionCode;
			this.linkParamId = linkParamId;
			this.linkParamArg = linkParamArg;
			this.linkParamValue = linkParamValue;
        }
		#endregion
		
		#region Properties
		

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long LinkParamElemId
        {
            get { return this.linkParamElemId; }
            set { 
				if(this.linkParamElemId != value) {
					DataStateChanged(ObjectState.Modified, "LinkParamElemId");
            		this.linkParamElemId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long LinkParamVersionCode
        {
            get { return this.linkParamVersionCode; }
            set { 
				if(this.linkParamVersionCode != value) {
					DataStateChanged(ObjectState.Modified, "LinkParamVersionCode");
            		this.linkParamVersionCode = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long LinkParamId
        {
            get { return this.linkParamId; }
            set { 
				if(this.linkParamId != value) {
					DataStateChanged(ObjectState.Modified, "LinkParamId");
            		this.linkParamId = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string LinkParamArg
        {
            get { return this.linkParamArg; }
            set { 
				if(this.linkParamArg != value) {
					DataStateChanged(ObjectState.Modified, "LinkParamArg");
            		this.linkParamArg = value;
				}
			}
		}
		
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string LinkParamValue
        {
            get { return this.linkParamValue; }
            set { 
				if(this.linkParamValue != value) {
					DataStateChanged(ObjectState.Modified, "LinkParamValue");
            		this.linkParamValue = value;
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
						case "LINKPARAMELEMID":
							if (!reader.IsDBNull(i)) this.linkParamElemId = reader.GetInt64(i);
							break;
						case "LINKPARAMVERSIONCODE":
							if (!reader.IsDBNull(i)) this.linkParamVersionCode = reader.GetInt64(i);
							break;
						case "LINKPARAMID":
							if (!reader.IsDBNull(i)) this.linkParamId = reader.GetInt64(i);
							break;
						case "LINKPARAMARG":
							if (!reader.IsDBNull(i)) this.linkParamArg = Convert.ToString(reader.GetValue(i));
							break;
						case "LINKPARAMVALUE":
							if (!reader.IsDBNull(i)) this.linkParamValue = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			LinkParam linkparam = obj as LinkParam;
			if (linkparam == null)
				return false;
			return linkparam.LinkParamId == LinkParamId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


