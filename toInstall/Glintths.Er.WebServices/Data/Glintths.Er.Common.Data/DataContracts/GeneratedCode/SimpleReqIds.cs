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

namespace Glintths.Er.Common.DataContracts
{
	/// <summary>
	/// Data Contract Class - SimpleReqIds
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "SimpleReqIds")]
	public partial class SimpleReqIds 
	{
		private SimpleReqIdList items;
		
		[WcfSerialization::DataMember(Name = "Items", IsRequired = false, Order = 0)]
		public SimpleReqIdList Items
		{
		  get { return items; }
		  set { items = value; }
		}				
	}
}

