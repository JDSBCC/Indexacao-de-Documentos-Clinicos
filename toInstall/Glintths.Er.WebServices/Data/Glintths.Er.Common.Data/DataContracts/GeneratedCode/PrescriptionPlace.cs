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
	/// Data Contract Class - PrescriptionPlace
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "PrescriptionPlace")]
	public partial class PrescriptionPlace 
	{
		private string localPrescr;
		private string description;
		private string codInstSaude;
		
		[WcfSerialization::DataMember(Name = "LocalPrescr", IsRequired = false, Order = 0)]
		public string LocalPrescr
		{
		  get { return localPrescr; }
		  set { localPrescr = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 1)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CodInstSaude", IsRequired = false, Order = 2)]
		public string CodInstSaude
		{
		  get { return codInstSaude; }
		  set { codInstSaude = value; }
		}				
	}
}
