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
	/// Data Contract Class - RequisitionsInfo
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "RequisitionsInfo")]
	public partial class RequisitionsInfo 
	{
		private RequisitionInfoList requisitionInfoList;
		
		[WcfSerialization::DataMember(Name = "RequisitionInfoList", IsRequired = false, Order = 0)]
		public RequisitionInfoList RequisitionInfoList
		{
		  get { return requisitionInfoList; }
		  set { requisitionInfoList = value; }
		}				
	}
}

