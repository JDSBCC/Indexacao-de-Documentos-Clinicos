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

namespace Cpchs.Documents.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - DocumentIndexLog
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "DocumentIndexLog")]
	public partial class DocumentIndexLog 
	{
		private string level;
		private string message;
		private string procedure;
		private string exception;
		private System.Nullable<System.DateTime> regDate;
		private string detail;
		
		[WcfSerialization::DataMember(Name = "Level", IsRequired = true, Order = 0)]
		public string Level
		{
		  get { return level; }
		  set { level = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Message", IsRequired = true, Order = 1)]
		public string Message
		{
		  get { return message; }
		  set { message = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Procedure", IsRequired = true, Order = 2)]
		public string Procedure
		{
		  get { return procedure; }
		  set { procedure = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Exception", IsRequired = false, Order = 3)]
		public string Exception
		{
		  get { return exception; }
		  set { exception = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "RegDate", IsRequired = true, Order = 4)]
		public System.Nullable<System.DateTime> RegDate
		{
		  get { return regDate; }
		  set { regDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Detail", IsRequired = false, Order = 5)]
		public string Detail
		{
		  get { return detail; }
		  set { detail = value; }
		}				
	}
}
