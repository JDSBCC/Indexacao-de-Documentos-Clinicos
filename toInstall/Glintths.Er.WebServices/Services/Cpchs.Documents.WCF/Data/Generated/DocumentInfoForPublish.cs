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
	/// Data Contract Class - DocumentInfoForPublish
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "DocumentInfoForPublish")]
	public partial class DocumentInfoForPublish 
	{
		private long documentUniqueId;
		private bool publish;
		
		[WcfSerialization::DataMember(Name = "DocumentUniqueId", IsRequired = false, Order = 1)]
		public long DocumentUniqueId
		{
		  get { return documentUniqueId; }
		  set { documentUniqueId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Publish", IsRequired = false, Order = 2)]
		public bool Publish
		{
		  get { return publish; }
		  set { publish = value; }
		}				
	}
}
