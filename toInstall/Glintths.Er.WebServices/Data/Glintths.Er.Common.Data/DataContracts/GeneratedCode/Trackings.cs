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
	/// Data Contract Class - Trackings
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Trackings")]
	public partial class Trackings 
	{
		private TrackingList trackingList;
		
		[WcfSerialization::DataMember(Name = "TrackingList", IsRequired = false, Order = 0)]
		public TrackingList TrackingList
		{
		  get { return trackingList; }
		  set { trackingList = value; }
		}				
	}
}

