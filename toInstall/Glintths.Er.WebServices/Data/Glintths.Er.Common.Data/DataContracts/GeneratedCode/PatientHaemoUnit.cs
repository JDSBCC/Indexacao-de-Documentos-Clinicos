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
	/// Data Contract Class - PatientHaemoUnit
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "PatientHaemoUnit")]
	public partial class PatientHaemoUnit 
	{
		private Glintths.Er.Common.DataContracts.Patient patient;
		private Glintths.Er.Common.DataContracts.Service service;
		private int unitFractionCount;
		private HaemoUnitFractionList unitFractionList;
		private string unitStatus;
		
		[WcfSerialization::DataMember(Name = "Patient", IsRequired = false, Order = 0)]
		public Glintths.Er.Common.DataContracts.Patient Patient
		{
		  get { return patient; }
		  set { patient = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Service", IsRequired = false, Order = 1)]
		public Glintths.Er.Common.DataContracts.Service Service
		{
		  get { return service; }
		  set { service = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UnitFractionCount", IsRequired = false, Order = 2)]
		public int UnitFractionCount
		{
		  get { return unitFractionCount; }
		  set { unitFractionCount = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UnitFractionList", IsRequired = false, Order = 3)]
		public HaemoUnitFractionList UnitFractionList
		{
		  get { return unitFractionList; }
		  set { unitFractionList = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UnitStatus", IsRequired = false, Order = 4)]
		public string UnitStatus
		{
		  get { return unitStatus; }
		  set { unitStatus = value; }
		}				
	}
}

