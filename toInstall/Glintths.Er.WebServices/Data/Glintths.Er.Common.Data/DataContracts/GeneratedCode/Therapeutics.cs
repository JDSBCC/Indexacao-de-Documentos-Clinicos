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
	/// Data Contract Class - Therapeutics
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Therapeutics")]
	public partial class Therapeutics 
	{
		private TherapeuticList items;
		
		[WcfSerialization::DataMember(Name = "Items", IsRequired = true, Order = 0)]
		public TherapeuticList Items
		{
		  get { return items; }
		  set { items = value; }
		}				
	}
}

