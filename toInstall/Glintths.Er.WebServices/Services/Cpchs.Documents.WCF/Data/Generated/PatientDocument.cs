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
	/// Data Contract Class - PatientDocument
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "PatientDocument")]
	public partial class PatientDocument 
	{
		private string docId;
		private System.Collections.Generic.Dictionary<string, string> docInfo;
		private string docElemType;
		private PatientDocuments docChilds;
		
		[WcfSerialization::DataMember(Name = "DocId", IsRequired = false, Order = 0)]
		public string DocId
		{
		  get { return docId; }
		  set { docId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocInfo", IsRequired = false, Order = 1)]
		public System.Collections.Generic.Dictionary<string, string> DocInfo
		{
		  get { return docInfo; }
		  set { docInfo = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocElemType", IsRequired = false, Order = 2)]
		public string DocElemType
		{
		  get { return docElemType; }
		  set { docElemType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocChilds", IsRequired = false, Order = 3)]
		public PatientDocuments DocChilds
		{
		  get { return docChilds; }
		  set { docChilds = value; }
		}				
	}
}

