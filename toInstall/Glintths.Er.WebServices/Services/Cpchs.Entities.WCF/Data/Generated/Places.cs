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

namespace Cpchs.Entities.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - Places
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "Places")]
	public partial class Places 
	{
		private PlaceCollection placeCollection;
		
		[WcfSerialization::DataMember(Name = "PlaceCollection", IsRequired = false, Order = 0)]
		public PlaceCollection PlaceCollection
		{
		  get { return placeCollection; }
		  set { placeCollection = value; }
		}				
	}
}

