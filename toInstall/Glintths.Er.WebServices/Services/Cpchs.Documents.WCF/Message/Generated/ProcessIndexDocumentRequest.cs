
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
	/// <summary>
	/// Service Contract Class - ProcessIndexDocumentRequest
	/// </summary>
    [WCF::MessageContract(WrapperName = "ProcessIndexDocumentRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
	public partial class ProcessIndexDocumentRequest
	{
		private string companyDb;
	 	private long indexId;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "IndexId")]
		public long IndexId
		{
			get { return indexId; }
			set { indexId = value; }
		}
	}
}

