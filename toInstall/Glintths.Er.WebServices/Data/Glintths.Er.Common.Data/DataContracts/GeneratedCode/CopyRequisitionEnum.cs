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
	/// Data Contract Enum - CopyRequisitionEnum
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "CopyRequisitionEnum")]
	public enum CopyRequisitionEnum 
	{
		
		[WcfSerialization::EnumMember(Value="CopyClinicInfo")]
		CopyClinicInfo = 0,		
		
		[WcfSerialization::EnumMember(Value="CopyLastRequisition")]
		CopyLastRequisition = 1,		
		
		[WcfSerialization::EnumMember(Value="SelectRequisition")]
		SelectRequisition = 2,		
	}
}

