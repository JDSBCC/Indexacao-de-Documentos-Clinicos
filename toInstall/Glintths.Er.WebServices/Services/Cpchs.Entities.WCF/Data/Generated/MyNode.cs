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

namespace Cpchs.Entities.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - MyNode
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "MyNode")]
	public partial class MyNode 
	{
		private string myNodeDescription;
		private string myNodeIds;
		private long myNodeOriginalId;
		private MyNodeCollection myNodeChilds;
		
		[WcfSerialization::DataMember(Name = "MyNodeDescription", IsRequired = false, Order = 0)]
		public string MyNodeDescription
		{
		  get { return myNodeDescription; }
		  set { myNodeDescription = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MyNodeIds", IsRequired = false, Order = 1)]
		public string MyNodeIds
		{
		  get { return myNodeIds; }
		  set { myNodeIds = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MyNodeOriginalId", IsRequired = false, Order = 2)]
		public long MyNodeOriginalId
		{
		  get { return myNodeOriginalId; }
		  set { myNodeOriginalId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MyNodeChilds", IsRequired = false, Order = 3)]
		public MyNodeCollection MyNodeChilds
		{
		  get { return myNodeChilds; }
		  set { myNodeChilds = value; }
		}				
	}
}
