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
	/// Data Contract Class - SimpleSampleIds
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "SimpleSampleIds")]
	public partial class SimpleSampleIds 
	{
		private SimpleSampleIdList items;
		
		[WcfSerialization::DataMember(Name = "Items", IsRequired = false, Order = 0)]
		public SimpleSampleIdList Items
		{
		  get { return items; }
		  set { items = value; }
		}				
	}
}

