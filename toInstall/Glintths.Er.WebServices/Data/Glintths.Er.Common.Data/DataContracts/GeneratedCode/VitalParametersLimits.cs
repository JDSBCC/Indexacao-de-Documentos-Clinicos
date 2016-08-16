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
	/// Data Contract Class - VitalParametersLimits
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "VitalParametersLimits")]
	public partial class VitalParametersLimits 
	{
		private bool isTemperatureField;
		private bool isPulseField;
		private bool isTA_SField;
		private string lim_Inf;
		private string lim_Sup;
		private string message;
		private string lim_Inf_Tol;
		private string lim_Sup_Tol;
		private string code;
		private string description;
		private bool isTA_DField;
		
		[WcfSerialization::DataMember(Name = "isTemperature", IsRequired = false, Order = 0)]
		public bool isTemperature
		{
		  get { return isTemperatureField; }
		  set { isTemperatureField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isPulse", IsRequired = false, Order = 1)]
		public bool isPulse
		{
		  get { return isPulseField; }
		  set { isPulseField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isTA_S", IsRequired = false, Order = 2)]
		public bool isTA_S
		{
		  get { return isTA_SField; }
		  set { isTA_SField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Lim_Inf", IsRequired = false, Order = 4)]
		public string Lim_Inf
		{
		  get { return lim_Inf; }
		  set { lim_Inf = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Lim_Sup", IsRequired = false, Order = 5)]
		public string Lim_Sup
		{
		  get { return lim_Sup; }
		  set { lim_Sup = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Message", IsRequired = false, Order = 8)]
		public string Message
		{
		  get { return message; }
		  set { message = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Lim_Inf_Tol", IsRequired = false, Order = 6)]
		public string Lim_Inf_Tol
		{
		  get { return lim_Inf_Tol; }
		  set { lim_Inf_Tol = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Lim_Sup_Tol", IsRequired = false, Order = 7)]
		public string Lim_Sup_Tol
		{
		  get { return lim_Sup_Tol; }
		  set { lim_Sup_Tol = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Code", IsRequired = false, Order = 9)]
		public string Code
		{
		  get { return code; }
		  set { code = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 10)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isTA_D", IsRequired = false, Order = 3)]
		public bool isTA_D
		{
		  get { return isTA_DField; }
		  set { isTA_DField = value; }
		}				
	}
}

