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

namespace Glintths.Base.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - Codifications
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Glintths.Base", Name = "Codifications")]
	public partial class Codifications 
	{
		private CodificationCollection codificationList;
		
		[WcfSerialization::DataMember(Name = "CodificationList", IsRequired = false, Order = 0)]
		public CodificationCollection CodificationList
		{
		  get { return codificationList; }
		  set { codificationList = value; }
		}				
	}
}
