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

namespace Cpchs.Entities.WCF.FaultContracts
{
	/// <summary>
	/// Data Contract Class - ServiceEmpty
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "ServiceEmpty")]
	public partial class ServiceEmpty 
	{
		private string message;
		
		[WcfSerialization::DataMember(Name = "Message", IsRequired = false, Order = 0)]
		public string Message
		{
		  get { return message; }
		  set { message = value; }
		}				
	}
}

