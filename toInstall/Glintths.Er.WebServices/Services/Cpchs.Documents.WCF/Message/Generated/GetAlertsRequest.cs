
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
	/// Service Contract Class - GetAlertsRequest
	/// </summary>
    [WCF::MessageContract(WrapperName = "GetAlertsRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
	public partial class GetAlertsRequest
	{
		private Cpchs.Entities.WCF.DataContracts.PaginationRequest paginationInfo;
	 	private string companyDb;
	 	private Cpchs.Entities.WCF.DataContracts.AlertListSearchCriteria searchCriteria;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PaginationInfo")]
		public Cpchs.Entities.WCF.DataContracts.PaginationRequest PaginationInfo
		{
			get { return paginationInfo; }
			set { paginationInfo = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "SearchCriteria")]
		public Cpchs.Entities.WCF.DataContracts.AlertListSearchCriteria SearchCriteria
		{
			get { return searchCriteria; }
			set { searchCriteria = value; }
		}
	}
}

