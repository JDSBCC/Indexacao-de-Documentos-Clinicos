
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
	/// Service Contract Class - GetDocumentFilesRequest
	/// </summary>
    [WCF::MessageContract(WrapperName = "GetDocumentFilesRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
	public partial class GetDocumentFilesRequest
	{
		private string companyDb;
	 	private long docId;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocId")]
		public long DocId
		{
			get { return docId; }
			set { docId = value; }
		}
	}
}
