//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Documents.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - DocumentAccess
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "DocumentAccess")]
	public partial class DocumentAccess 
	{
		private string sessionId;
		private string userName;
		private long artifactId;
		private long versionId;
		private long appOrigin;
		private long docType;
		private long userId;
		private long docId;
		private string docRef;
		
		[WcfSerialization::DataMember(Name = "SessionId", IsRequired = false, Order = 0)]
		public string SessionId
		{
		  get { return sessionId; }
		  set { sessionId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UserName", IsRequired = false, Order = 1)]
		public string UserName
		{
		  get { return userName; }
		  set { userName = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ArtifactId", IsRequired = false, Order = 2)]
		public long ArtifactId
		{
		  get { return artifactId; }
		  set { artifactId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "VersionId", IsRequired = false, Order = 3)]
		public long VersionId
		{
		  get { return versionId; }
		  set { versionId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "AppOrigin", IsRequired = false, Order = 4)]
		public long AppOrigin
		{
		  get { return appOrigin; }
		  set { appOrigin = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocType", IsRequired = false, Order = 5)]
		public long DocType
		{
		  get { return docType; }
		  set { docType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UserId", IsRequired = false, Order = 6)]
		public long UserId
		{
		  get { return userId; }
		  set { userId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocId", IsRequired = false, Order = 7)]
		public long DocId
		{
		  get { return docId; }
		  set { docId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocRef", IsRequired = false, Order = 8)]
		public string DocRef
		{
		  get { return docRef; }
		  set { docRef = value; }
		}				
	}
}

