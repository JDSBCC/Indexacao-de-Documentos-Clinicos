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

namespace Cpchs.Entities.WCF.ServiceImplementation
{	
	/// <summary>
	/// Service Class - EntitiesManagementWs
	/// </summary>
	[WCF::ServiceBehavior(Name = "EntitiesManagementWs", 
		Namespace = "urn:Cpchs.Entities", 
		InstanceContextMode = WCF::InstanceContextMode.PerSession, 
		ConcurrencyMode = WCF::ConcurrencyMode.Single )]
	public abstract class EntitiesManagementWsBase : Cpchs.Entities.WCF.ServiceContracts.IEntitiesManagementWsContract
	{
		#region EntitiesManagementWsContract Members

        public virtual Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataResponse GetSearchFiltersData(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataRequest request)
        {
            return null;
        }

        public virtual Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersResponse GetServicesForFilters(Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersRequest request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersResponse GetDocTypesForFilters(Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersRequest request)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersResponse GetDocTypesTreeForFilters(Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersRequest request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentResponse GetPatientByDocument(Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentRequest request)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse SimplePatientFind(Cpchs.Entities.WCF.MessageContracts.SimplePatientFindRequest request)
        {
            return null;
        }

        public virtual Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse SimplePatientFindV2(Cpchs.Entities.WCF.MessageContracts.SimplePatientFindV2Request request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Response GetPatientByDocumentV2(Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Request request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorResponse GetFavoriteServicesForDoctor(Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsResponse GetGroupsBySpecs(Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesResponse GetPatientsBySamples(Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GenericPatientFindResponse GenericPatientFind(Cpchs.Entities.WCF.MessageContracts.GenericPatientFindRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorResponse GetServicesForDoctor(Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Response GetSearchFiltersDataV2(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Request request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersResponse GetApplicationsForFilters(Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientByIdResponse GetPatientById(Cpchs.Entities.WCF.MessageContracts.GetPatientByIdRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsResponse GetPatientEpisodeByIds(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionResponse GetEntityInSession(Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientResponse GetOrInsertPatient(Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityResponse GetOrInsertEntity(Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeResponse GetOrInsertEpisode(Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientResponse GetPatientEpisodesByGenericPatient(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataResponse GetPatientEpisodesBySearchData(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesResponse GetPatientEpisodes(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesRequest request)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Response GetSearchFiltersDataV3(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Request request)
        {
            return null;
        }

        public virtual Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Response GetSearchFiltersDataV4(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Request request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestResponse GetPatientByRequest(Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsResponse GetTerapeutics(Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupResponse GetGroupExamsByGroup(Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaResponse GetExamsBySearchCriteria(Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaRequest request)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyResponse GetDocumentTypesHierarchy(Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyRequest request)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderResponse GetPatientsFromExternalProvider(Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderRequest request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationResponse ExternalAccessInitialization(Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationRequest request)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderResponse GetPatientEpisodesFromExternalProvider(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderRequest request)
        {
            return null;
        }

        //public virtual Cpchs.Entities.WCF.DataContracts.EpisodeTypeCollection GetEpisodeTypesFromExternalProvider(string companyDB)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection GetEpisodesFromExternalProvider(string companyDB, string tDoente, string doente, string tEpisodio, string episodio)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.DataContracts.PatientEpisode ImportEpisodeToPatient(string companyDB, string tDoente, string doente, string tEpisode, string episode)
        //{
        //    return null;
        //}

        //public virtual Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection SearchEpisode(string companyDB, string tEpisode, string episode)
        //{
        //    return null;
        //}

        public virtual Cpchs.Entities.WCF.MessageContracts.CheckUserSettingsResponse CheckUserSettings(Cpchs.Entities.WCF.MessageContracts.CheckUserSettingsRequest request)
        {
            return null;
        }

        public virtual Cpchs.Entities.WCF.MessageContracts.GetEpisodeTypeByCodeResponse GetEpisodeTypeByCode(Cpchs.Entities.WCF.MessageContracts.GetEpisodeTypeByCodeRequest req)
        {
            return null;
        }

        public virtual Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsResponse GetPatientsBySpecs(Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsRequest request)
        {
            return null;
        }
		#endregion		
		
	}
	
	public partial class EntitiesManagementWs : EntitiesManagementWsBase
	{
	}
	
}

