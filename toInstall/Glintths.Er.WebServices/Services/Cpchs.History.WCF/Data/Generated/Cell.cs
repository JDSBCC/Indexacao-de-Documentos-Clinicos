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

namespace Cpchs.History.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - Cell
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.History", Name = "Cell")]
	public partial class Cell 
	{
		private System.DateTime period;
		
		[WcfSerialization::DataMember(Name = "Period", IsRequired = false, Order = 0)]
		public System.DateTime Period
		{
		  get { return period; }
		  set { period = value; }
		}				
	}
}

