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

namespace Cpchs.Entities.WCF.MessageContracts
{
	/// <summary>
	/// Service Contract Class - GetSearchFiltersDataV4Request
	/// </summary>
    [WCF::MessageContract(WrapperName = "GetSearchFiltersDataV4Request", WrapperNamespace = "urn:Cpchs.Entities", IsWrapped = true)] 
	public partial class GetSearchFiltersDataV4Request
	{
		private string companyDb;
	 	private string globalFilters;
	 	private string docsSessionFilters;
	 	private string servsSessionFilters;
	 	private bool anaResAccess;
	 	private string location;
	 	private string entId;
	 	private System.Nullable<long> episodeTypeId;
	 	private string episodeId;
	 	private System.Nullable<long> instId;
	 	private System.Nullable<long> placeId;
	 	private System.Nullable<long> appId;
	 	private System.Nullable<long> docTypeId;
	 	private string docRef;
	 	private string elemType;
	 	private string stateScope;
	 	private string userName;
	 		
		[WCF::MessageBodyMember(Name = "CompanyDb")] 
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Name = "GlobalFilters")] 
		public string GlobalFilters
		{
			get { return globalFilters; }
			set { globalFilters = value; }
		}
			
		[WCF::MessageBodyMember(Name = "DocsSessionFilters")] 
		public string DocsSessionFilters
		{
			get { return docsSessionFilters; }
			set { docsSessionFilters = value; }
		}
			
		[WCF::MessageBodyMember(Name = "ServsSessionFilters")] 
		public string ServsSessionFilters
		{
			get { return servsSessionFilters; }
			set { servsSessionFilters = value; }
		}
			
		[WCF::MessageBodyMember(Name = "AnaResAccess")] 
		public bool AnaResAccess
		{
			get { return anaResAccess; }
			set { anaResAccess = value; }
		}
			
		[WCF::MessageBodyMember(Name = "Location")] 
		public string Location
		{
			get { return location; }
			set { location = value; }
		}
			
		[WCF::MessageBodyMember(Name = "EntId")] 
		public string EntId
		{
			get { return entId; }
			set { entId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "EpisodeTypeId")] 
		public System.Nullable<long> EpisodeTypeId
		{
			get { return episodeTypeId; }
			set { episodeTypeId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "EpisodeId")] 
		public string EpisodeId
		{
			get { return episodeId; }
			set { episodeId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "InstId")] 
		public System.Nullable<long> InstId
		{
			get { return instId; }
			set { instId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "PlaceId")] 
		public System.Nullable<long> PlaceId
		{
			get { return placeId; }
			set { placeId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "AppId")] 
		public System.Nullable<long> AppId
		{
			get { return appId; }
			set { appId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "DocTypeId")] 
		public System.Nullable<long> DocTypeId
		{
			get { return docTypeId; }
			set { docTypeId = value; }
		}
			
		[WCF::MessageBodyMember(Name = "DocRef")] 
		public string DocRef
		{
			get { return docRef; }
			set { docRef = value; }
		}
			
		[WCF::MessageBodyMember(Name = "ElemType")] 
		public string ElemType
		{
			get { return elemType; }
			set { elemType = value; }
		}
			
		[WCF::MessageBodyMember(Name = "StateScope")] 
		public string StateScope
		{
			get { return stateScope; }
			set { stateScope = value; }
		}
			
		[WCF::MessageBodyMember(Name = "UserName")] 
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}
	}
}

