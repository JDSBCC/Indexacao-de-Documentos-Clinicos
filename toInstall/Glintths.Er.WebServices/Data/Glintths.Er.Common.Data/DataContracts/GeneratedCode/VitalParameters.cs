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
	/// Data Contract Class - VitalParameters
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "VitalParameters")]
	public partial class VitalParameters 
	{
		private long uniqueId;
		private long unitFractionId;
		private string temperature;
		private string pulse;
		private string tA_S;
		private bool isStartTimeField;
		private bool isAfter15MinField;
		private bool isEndTimeField;
		private Glintths.Er.Common.DataContracts.CrudOperation crudOperation;
		private string tA_D;
		
		[WcfSerialization::DataMember(Name = "UniqueId", IsRequired = false, Order = 0)]
		public long UniqueId
		{
		  get { return uniqueId; }
		  set { uniqueId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "UnitFractionId", IsRequired = false, Order = 1)]
		public long UnitFractionId
		{
		  get { return unitFractionId; }
		  set { unitFractionId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Temperature", IsRequired = false, Order = 2)]
		public string Temperature
		{
		  get { return temperature; }
		  set { temperature = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Pulse", IsRequired = false, Order = 3)]
		public string Pulse
		{
		  get { return pulse; }
		  set { pulse = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TA_S", IsRequired = false, Order = 4)]
		public string TA_S
		{
		  get { return tA_S; }
		  set { tA_S = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isStartTime", IsRequired = false, Order = 6)]
		public bool isStartTime
		{
		  get { return isStartTimeField; }
		  set { isStartTimeField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isAfter15Min", IsRequired = false, Order = 7)]
		public bool isAfter15Min
		{
		  get { return isAfter15MinField; }
		  set { isAfter15MinField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "isEndTime", IsRequired = false, Order = 8)]
		public bool isEndTime
		{
		  get { return isEndTimeField; }
		  set { isEndTimeField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CrudOperation", IsRequired = false, Order = 9)]
		public Glintths.Er.Common.DataContracts.CrudOperation CrudOperation
		{
		  get { return crudOperation; }
		  set { crudOperation = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TA_D", IsRequired = false, Order = 5)]
		public string TA_D
		{
		  get { return tA_D; }
		  set { tA_D = value; }
		}				
	}
}
