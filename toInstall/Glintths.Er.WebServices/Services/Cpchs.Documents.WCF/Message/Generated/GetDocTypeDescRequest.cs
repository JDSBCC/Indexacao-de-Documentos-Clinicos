
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
	/// Service Contract Class - GetDocTypeDescRequest
	/// </summary>
    [WCF::MessageContract(WrapperName = "GetDocTypeDescRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
	public partial class GetDocTypeDescRequest
	{
		private string companyDb;
	 	private string instCode;
	 	private string placeCode;
	 	private string appCode;
	 	private string docTypeCode;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "InstCode")]
		public string InstCode
		{
			get { return instCode; }
			set { instCode = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PlaceCode")]
		public string PlaceCode
		{
			get { return placeCode; }
			set { placeCode = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "AppCode")]
		public string AppCode
		{
			get { return appCode; }
			set { appCode = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocTypeCode")]
		public string DocTypeCode
		{
			get { return docTypeCode; }
			set { docTypeCode = value; }
		}
	}
}

