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
	/// Data Contract Class - RequisitionPlace
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "RequisitionPlace")]
	public partial class RequisitionPlace 
	{
		private string id;
		private string description;
		private string type;
		private string healthcareUnit;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = true, Order = 0)]
		public string Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = true, Order = 1)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Type", IsRequired = true, Order = 2)]
		public string Type
		{
		  get { return type; }
		  set { type = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "HealthcareUnit", IsRequired = false, Order = 3)]
		public string HealthcareUnit
		{
		  get { return healthcareUnit; }
		  set { healthcareUnit = value; }
		}				
	}
}
