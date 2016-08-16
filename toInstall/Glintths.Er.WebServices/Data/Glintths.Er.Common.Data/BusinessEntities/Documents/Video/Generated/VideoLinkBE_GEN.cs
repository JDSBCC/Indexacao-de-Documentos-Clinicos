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
    /// Date Created: quinta-feira, 7 de Outubro de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class VideoLink_GEN : AbstractEntity
    {	
		#region Variables
		
		private long videoLinkElemId = 0; ///
		private long videoLinkVersionCode = 0; ///
		private long videoLinkId = 0; ///
		private string videoLinkUrl = string.Empty; ///
		private long videoLinkHResolution = 0; ///
		private long videoLinkVResolution = 0; ///
		private long videoLinkSize = 0; ///
		private string videoLinkType = string.Empty; ///
		
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty VideoLink object.
        /// </summary>
        public VideoLink_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty VideoLink object.
        /// </summary>
        public VideoLink_GEN(long videoLinkElemId, long videoLinkVersionCode, long videoLinkId) 
			: base(ObjectState.Added , null) 
        {
			this.videoLinkElemId = videoLinkElemId;
			this.videoLinkVersionCode = videoLinkVersionCode;
			this.videoLinkId = videoLinkId;
        }
		/// <summary>
        /// Initialize an new empty VideoLink object.
        /// </summary>
        public VideoLink_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, null, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  VideoLink object with the given parameters.
        /// </summary>
        public  VideoLink_GEN(long videoLinkElemId, long videoLinkVersionCode, long videoLinkId, string videoLinkUrl, long videoLinkHResolution, long videoLinkVResolution, long videoLinkSize, string videoLinkType) 
			: base(ObjectState.Added, null) 
        {	 
			this.videoLinkElemId = videoLinkElemId;
			this.videoLinkVersionCode = videoLinkVersionCode;
			this.videoLinkId = videoLinkId;
			this.videoLinkUrl = videoLinkUrl;
			this.videoLinkHResolution = videoLinkHResolution;
			this.videoLinkVResolution = videoLinkVResolution;
			this.videoLinkSize = videoLinkSize;
			this.videoLinkType = videoLinkType;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkElemId
        {
            get { return this.videoLinkElemId; }
            set { 
				if(this.videoLinkElemId != value) {
					this.videoLinkElemId = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkElemId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkVersionCode
        {
            get { return this.videoLinkVersionCode; }
            set { 
				if(this.videoLinkVersionCode != value) {
					this.videoLinkVersionCode = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkVersionCode");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkId
        {
            get { return this.videoLinkId; }
            set { 
				if(this.videoLinkId != value) {
					this.videoLinkId = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string VideoLinkUrl
        {
            get { return this.videoLinkUrl; }
            set { 
				if(this.videoLinkUrl != value) {
					this.videoLinkUrl = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkUrl");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkHResolution
        {
            get { return this.videoLinkHResolution; }
            set { 
				if(this.videoLinkHResolution != value) {
					this.videoLinkHResolution = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkHResolution");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkVResolution
        {
            get { return this.videoLinkVResolution; }
            set { 
				if(this.videoLinkVResolution != value) {
					this.videoLinkVResolution = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkVResolution");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long VideoLinkSize
        {
            get { return this.videoLinkSize; }
            set { 
				if(this.videoLinkSize != value) {
					this.videoLinkSize = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkSize");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string VideoLinkType
        {
            get { return this.videoLinkType; }
            set { 
				if(this.videoLinkType != value) {
					this.videoLinkType = value;
					DataStateChanged(ObjectState.Modified, "VideoLinkType");
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
						case "VIDEOLINKELEMID":
							if (!reader.IsDBNull(i)) this.videoLinkElemId = reader.GetInt64(i);
							break;
						case "VIDEOLINKVERSIONCODE":
							if (!reader.IsDBNull(i)) this.videoLinkVersionCode = reader.GetInt64(i);
							break;
						case "VIDEOLINKID":
							if (!reader.IsDBNull(i)) this.videoLinkId = reader.GetInt64(i);
							break;
						case "VIDEOLINKURL":
							if (!reader.IsDBNull(i)) this.videoLinkUrl = Convert.ToString(reader.GetValue(i));
							break;
						case "VIDEOLINKHRESOLUTION":
							if (!reader.IsDBNull(i)) this.videoLinkHResolution = reader.GetInt64(i);
							break;
						case "VIDEOLINKVRESOLUTION":
							if (!reader.IsDBNull(i)) this.videoLinkVResolution = reader.GetInt64(i);
							break;
						case "VIDEOLINKSIZE":
							if (!reader.IsDBNull(i)) this.videoLinkSize = reader.GetInt64(i);
							break;
						case "VIDEOLINKTYPE":
							if (!reader.IsDBNull(i)) this.videoLinkType = Convert.ToString(reader.GetValue(i));
							break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			VideoLink videolink = obj as VideoLink;
			if (videolink == null)
				return false;
			return videolink.VideoLinkId == VideoLinkId;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}

