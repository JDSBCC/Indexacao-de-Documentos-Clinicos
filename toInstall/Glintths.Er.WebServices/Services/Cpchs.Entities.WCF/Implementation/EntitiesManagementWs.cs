using System;
using Cpchs.Entities.WCF.FaultContracts;
using Cpchs.Entities.WCF.BusinessLogic;
using System.ServiceModel;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.Collections.Generic;
using Cpchs.Entities.WCF.MessageContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public partial class EntitiesManagementWs
    {
        public override Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataResponse GetSearchFiltersData(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataRequest request)
        {
            Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataResponse response = new Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataResponse();
            Cpchs.Entities.WCF.DataContracts.FiltersData filtersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();

            if (!string.IsNullOrEmpty(request.PatientTypeScope))
            {
                filtersData.PatientTypes =
               TranslateBetweenPatientTypeListAndPatientTypeCollection.TranslatePatientTypesToPatientTypes(
                   Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPatientTypes(request.CompanyDb, request.UserName));
                filtersData.PatientTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.PatientType()
                {
                    Id = -99,
                    Code = " ",
                    Acronym = " ",
                    Description = ""
                });

                response.FiltersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();
                response.FiltersData = filtersData;
                return response;
            }

            filtersData.Genders =
                TranslateBetweenGenderListAndGenderCollection.TranslateGenresToGenres(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllGenders(request.CompanyDb));
            filtersData.Genders.Insert(0, new Cpchs.Entities.WCF.DataContracts.Gender()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.EpisodeTypes =
                TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection.TranslateEpisodeTypesToEpisodeTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetAllEpisodeTypes(request.CompanyDb));
            filtersData.EpisodeTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.EpisodeType()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.PatientTypes =
                TranslateBetweenPatientTypeListAndPatientTypeCollection.TranslatePatientTypesToPatientTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPatientTypes(request.CompanyDb, request.UserName));
            filtersData.PatientTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.PatientType()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.Institutions =
                TranslateBetweenInstitutionListAndInstitutionCollection.TranslateInstitutionsToInstitutions(
                    Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetDocTypesTreeForSearchBar(
                        request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
                        request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location));

            filtersData.FiltersEffect =
                Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetFiltersActivity(
                        request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
                        request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location);

            filtersData.Services =
                TranslateBetweenServiceListAndServiceCollection.TranslateServicesToServices(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllServices(request.CompanyDb));

            string stateScope = "EXAME_REQ";
            if (!string.IsNullOrEmpty(request.StateScope))
                stateScope = request.StateScope;

            filtersData.States =
                TranslateBetweenStateListAndStateCollection.TranslateStatesToStates(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetStates(request.CompanyDb, stateScope));

            response.FiltersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();
            response.FiltersData = filtersData;
            return response;

        }

        // Com tipos de alerta
        public override Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Response GetSearchFiltersDataV3(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Request request)
        {
            Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Response response = new Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV3Response();
            Cpchs.Entities.WCF.DataContracts.FiltersData filtersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();

            filtersData.Genders =
                TranslateBetweenGenderListAndGenderCollection.TranslateGenresToGenres(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllGenders(request.CompanyDb));
            filtersData.Genders.Insert(0, new Cpchs.Entities.WCF.DataContracts.Gender()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.EpisodeTypes =
                TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection.TranslateEpisodeTypesToEpisodeTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetAllEpisodeTypes(request.CompanyDb));
            filtersData.EpisodeTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.EpisodeType()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.PatientTypes =
                TranslateBetweenPatientTypeListAndPatientTypeCollection.TranslatePatientTypesToPatientTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPatientTypes(request.CompanyDb, request.UserName));
            filtersData.PatientTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.PatientType()
            {
                Id = -99,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.Institutions =
                TranslateBetweenInstitutionListAndInstitutionCollection.TranslateInstitutionsToInstitutions(
                    Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetDocTypesTreeForSearchBar(
                        request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
                        request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location));

            filtersData.FiltersEffect =
                Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetFiltersActivity(
                        request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
                        request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location);


            response.FiltersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();
            response.FiltersData = filtersData;

            filtersData.States =
                TranslateBetweenStateListAndStateCollection.TranslateStatesToStates(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetStates(request.CompanyDb, request.StateScope)
                );
            filtersData.States.Insert(0, new Cpchs.Entities.WCF.DataContracts.State()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = "",
                Scope = " "
            });

            filtersData.AlertTypes =
                TranslateBetweenAlertTypeListAndAlertTypeCollection.TranslateAlertTypesToAlertTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllAlertTypes(request.CompanyDb)
                );
            filtersData.AlertTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.AlertType()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = "",
            });

            return response;

        }


        public override Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersResponse GetServicesForFilters(Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersRequest request)
        {
            Cpchs.Entities.WCF.DataContracts.ServiceCollection services = new Cpchs.Entities.WCF.DataContracts.ServiceCollection();
            services = TranslateBetweenServiceListAndServiceCollection.TranslateServicesToServices(Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllServices(request.companyDb));
            Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersResponse response = new Cpchs.Entities.WCF.MessageContracts.GetServicesForFiltersResponse();
            response.ServicesList = new Cpchs.Entities.WCF.DataContracts.Services();
            response.ServicesList.ServiceCollection = services;
            return response;
        }

        //public override Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersResponse GetDocTypesForFilters( Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersRequest request )
        //{
        //    Cpchs.Entities.WCF.DataContracts.PlaceCollection treePlaces = new Cpchs.Entities.WCF.DataContracts.PlaceCollection();
        //    treePlaces = TranslateBetweenPlaceListAndPlaceCollection.TranslatePlacesToPlaces( EntityLogic.GetDocTypesForFilters( request.companyDb, request.institutionId ) );
        //    Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersResponse response = new Cpchs.Entities.WCF.MessageContracts.GetDocTypesForFiltersResponse();
        //    response.DocTypesTree = new Cpchs.Entities.WCF.DataContracts.Places();
        //    response.DocTypesTree.PlaceCollection = treePlaces;
        //    return response;
        //}

        public override Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersResponse GetDocTypesTreeForFilters(Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersRequest request)
        {
            //FIX ME
            Cpchs.Entities.WCF.DataContracts.MyNodeCollection treeInsts = new Cpchs.Entities.WCF.DataContracts.MyNodeCollection();
            treeInsts = TranslateBetweenInstitutionListAndMyNodeCollection.TranslateInstitutionsToMyNodes(EntityLogic.GetDocTypesTreeForFilters(request.companyDb, request.username));
            Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersResponse response = new Cpchs.Entities.WCF.MessageContracts.GetDocTypesTreeForFiltersResponse();
            response.NodesTree = new Cpchs.Entities.WCF.DataContracts.MyNodes();
            response.NodesTree.MyNodeCollection = treeInsts;
            return response;
        }

        //public override Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyResponse GetDocumentTypesHierarchy(Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyRequest request)
        //{
        //    Cpchs.Entities.WCF.DataContracts.InstitutionCollection treeInsts = new Cpchs.Entities.WCF.DataContracts.InstitutionCollection();
        //    treeInsts = TranslateBetweenInstitutionListAndInstitutionCollection.TranslateInstitutionsToInstitutions(EntityLogic.GetDocTypesTreeForFilters(request.CompanyDb));
        //    Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyResponse response = new Cpchs.Entities.WCF.MessageContracts.GetDocumentTypesHierarchyResponse();
        //    response.Institutions = new Cpchs.Entities.WCF.DataContracts.InstitutionCollection();
        //    response.Institutions = treeInsts;
        //    return response;
        //}

        public override Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse SimplePatientFind(Cpchs.Entities.WCF.MessageContracts.SimplePatientFindRequest request)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
                   Cpchs.Entities.WCF.BusinessLogic.EntityLogic.SimplePatientFind(
                       request.CompanyDb,
                       request.PatType,
                       request.PatId,
                       request.PatNProc,
                       request.PatName,
                       request.PatNsns,
                       request.PatBirthDate,
                       request.PatSex,
                       request.EpisodeTypeId,
                       request.EpisodeId,
                       request.EpiDateBegin,
                       request.EpiDateEnd,
                       request.Doc,
                       request.ExtId,
                       request.DocType,
                       request.AppId,
                       request.LocalId,
                       request.InstId,
                       request.DocDateBegin,
                       request.DocEndDate,
                       request.ValDateBegin,
                       request.ValDateEnd,
                       request.SearchType,
                       request.GlobalFilters,
                       request.DocsSessionFilters,
                       request.ServsSessionFilters,
                       request.UserName,
                       request.UserResAna);

                Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
                patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

                foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items)
                {
                    patients.PatientCollection.Add(TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient(p));
                }

                Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse response =
                    new Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse();
                response.PatientList = patients;

                return response;
            }
            catch (Exception e)
            {
                PatientNotFound pnf = new PatientNotFound();
                pnf.Message = e.Message;
                throw new FaultException<PatientNotFound>(pnf, pnf.Message);
            }
        }

        public override Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse SimplePatientFindV2(Cpchs.Entities.WCF.MessageContracts.SimplePatientFindV2Request request)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
                   Cpchs.Entities.WCF.BusinessLogic.EntityLogic.SimplePatientFindV3(
                       request.CompanyDb,
                       request.PatType,
                       request.PatId,
                       request.PatNProc,
                       request.PatName,
                       request.PatNsns,
                       request.PatBirthDate,
                       request.PatSex,
                       request.EpisodeTypeId,
                       request.EpisodeTypeCode,
                       request.EpisodeId,
                       request.EpiDateBegin,
                       request.EpiDateEnd,
                       request.Doc,
                       request.ExtId,
                       request.DocTypeId,
                       request.DocTypeCode,
                       request.AppId,
                       request.AppCode,
                       request.LocalId,
                       request.LocalCode,
                       request.InstId,
                       request.InstCode,
                       request.DocDateBegin,
                       request.DocEndDate,
                       request.ValDateBegin,
                       request.ValDateEnd,
                       request.ProcDateBegin,
                       request.ProcDateEnd,
                       request.SearchType,
                       request.GlobalFilters,
                       request.DocsSessionFilters,
                       request.ServsSessionFilters,
                       request.UserName,
                       request.UserResAna);

                Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
                patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

                foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items)
                {
                    patients.PatientCollection.Add(TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient(p));
                }

                Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse response =
                    new Cpchs.Entities.WCF.MessageContracts.SimplePatientFindResponse();
                response.PatientList = patients;

                return response;
            }
            catch (Exception e)
            {
                PatientNotFound pnf = new PatientNotFound();
                pnf.Message = e.Message;
                throw new FaultException<PatientNotFound>(pnf, pnf.Message);
            }
        }

        public override Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderResponse GetPatientsFromExternalProvider(Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderRequest request)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatientList patList =
                   Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientsFromExternalProvider(
                       request.CompanyDb,
                       request.PatName,
                       request.PatId,
                       request.PatProcessNum,
                       request.PatSNS,
                       request.PatBirthDate);

                Cpchs.Entities.WCF.DataContracts.Patients patients =
                    TranslateBetweenExternalPatientListBEAndPatientListDC.TranslateExternalPatientListToPatientList(patList);

                Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderResponse response =
                    new Cpchs.Entities.WCF.MessageContracts.GetPatientsFromExternalProviderResponse() { PatientList = patients };

                return response;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public override Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderResponse GetPatientEpisodesFromExternalProvider(Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderRequest request)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalEpisodeList patList =
                   Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientEpisodesFromExternalProvider(
                       request.CompanyDB,
                       request.PatType,
                       request.PatId,
                       request.UserID);

                Cpchs.Entities.WCF.DataContracts.PatientEpisodes episodes =
                    TranslateBetweenExternalEpisodesListBEAndPatientEpisodesDC.TranslateExternalEpisodeListToPatientEpisodes(patList);

                Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderResponse response =
                    new Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesFromExternalProviderResponse() { PatientEpisodesList = episodes };

                return response;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentResponse GetPatientByDocument( Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentRequest request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
        //           Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientByDocument(
        //               request.CompanyDb,
        //               request.Doc,
        //               request.AppId,
        //               request.LocalId,
        //               request.InstId );

        //        Cpchs.Entities.WCF.DataContracts.Patients patients =
        //        new Cpchs.Entities.WCF.DataContracts.Patients();
        //        patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items )
        //            patients.PatientCollection.Add( TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient( p ) );

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentResponse response =
        //            new Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentResponse();
        //        response.PatientList = patients;

        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Response GetPatientByDocumentV2( Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Request request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
        //           Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientByDocumentV2(
        //               request.CompanyDb,
        //               request.Document,
        //               request.DocumentTypeId,
        //               request.ApplicationId,
        //               request.PlaceId,
        //               request.InstitutionId );

        //        Cpchs.Entities.WCF.DataContracts.Patients patients =
        //        new Cpchs.Entities.WCF.DataContracts.Patients();
        //        patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items )
        //            patients.PatientCollection.Add( TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient( p ) );

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Response response =
        //            new Cpchs.Entities.WCF.MessageContracts.GetPatientByDocumentV2Response();
        //        response.PatientList = patients;

        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorResponse GetFavoriteServicesForDoctor( Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorRequest request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.ServiceList slist = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetFavoriteServicesForDoctor(
        //            request.CompanyDb,
        //            request.NMecan,
        //            request.ReqService,
        //            request.Place,
        //            request.Institution
        //            );

        //        Cpchs.Entities.WCF.DataContracts.Services services = new Cpchs.Entities.WCF.DataContracts.Services();
        //        services.ServiceCollection = new Cpchs.Entities.WCF.DataContracts.ServiceCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Service p in slist.Items )
        //        {
        //            services.ServiceCollection.Add( TranslateBetweenServiceBEAndServiceDC.TranslateServiceToService( p ) );
        //        }

        //        Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorResponse response = new Cpchs.Entities.WCF.MessageContracts.GetFavoriteServicesForDoctorResponse();
        //        response.ServiceList = services;


        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        ServiceEmpty pnf = new ServiceEmpty();
        //        pnf.Message = e.Message;
        //        throw new FaultException<ServiceEmpty>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsResponse GetGroupsBySpecs( Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsRequest request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.GroupList glist = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetGroupsBySpecs(
        //            request.CompanyDb,
        //            request.Place,
        //            request.Institution,
        //            request.ServExec,
        //            request.ServReq,
        //            request.NMecan,
        //            request.Scope
        //            );

        //        Cpchs.Entities.WCF.DataContracts.GroupList groups = new Cpchs.Entities.WCF.DataContracts.GroupList();
        //        groups.Groups = new Cpchs.Entities.WCF.DataContracts.GroupCollection();

                

        //        // types
        //        /*Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList reqTypes = new Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList();

        //        reqTypes = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetTypes( request.CompanyDb );

        //        Cpchs.Entities.WCF.DataContracts.ExamTypeCollection tplist = new Cpchs.Entities.WCF.DataContracts.ExamTypeCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionType p in reqTypes.Items )
        //        {
        //            tplist.Add( TranslateBetweenRequisitionTypeBeAndExamTypeDc.TranslateRequisitionTypeBeToTypeDc( p ) );
        //        }*/

        //        // priorities
        //        /*Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList priorities = new Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList();

        //        priorities = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetPriorities( request.CompanyDb );

        //        Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection plist = new Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection();
                
        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Priority p in priorities.Items )
        //        {
        //            plist.Add( TranslateBetweenPriorityBeAndExamPriorityDc.TranslatePriorityBeToPriorityDc( p ) );
        //        }*/

        //        // exec places
        //        /*Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList execplace = new Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList();

        //        execplace = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetExecPlaces(request.CompanyDb);

        //        Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection elist = new Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection();

        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlace e in execplace.Items)
        //        {
        //            elist.Add(TranslateBetweenExecPlaceBeAndExecPlaceDc.TranslateExecPlaceBeToExecPlaceDc(e));
        //        }*/

        //        // extract places
        //        /*Cpchs.Eresults.Common.WCF.BusinessEntities.ExtractPlaceList extplace = new Cpchs.Eresults.Common.WCF.BusinessEntities.ExtractPlaceList();

        //        extplace = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetExtPlaces(request.CompanyDb);

        //        Cpchs.Entities.WCF.DataContracts.ExamExtractPlaceCollection exlist = new Cpchs.Entities.WCF.DataContracts.ExamExtractPlaceCollection();

        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.ExtractPlace pe in extplace.Items)
        //        {
        //            exlist.Add(TranslateBetweenExtractPlaceBeAndExtractPlaceDc.TranslateExtractPlaceBeToExtractPlaceDc(pe));
        //        }*/


        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Group p in glist.Items )
        //        {
        //            groups.Groups.Add( TranslateBetweenGroupBEAndGroupDC.TranslateGroupToGroup( p , null, null, null /*tplist, plist, elist, exlist */) );
        //        }

        //        //exames do grupo

        //        /*Cpchs.Eresults.Common.WCF.BusinessEntities.GroupExamList gexlist = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetGroupExamsBySpecs(
        //            request.CompanyDb,
        //            request.LocalId.Value,
        //            request.InstId,
        //            request.ServExecId,
        //            request.ServReqId,
        //            request.DocEntId,
        //            request.Scope
        //            );

        //        Cpchs.Entities.WCF.DataContracts.GroupExamList groupsExams = new Cpchs.Entities.WCF.DataContracts.GroupExamList();
        //        groupsExams.GroupExams = new Cpchs.Entities.WCF.DataContracts.GroupExamCollection();

        //        foreach (Cpchs.Entities.WCF.DataContracts.Group g in groups.Groups)
        //        {
        //            g.GroupExamList = new Cpchs.Entities.WCF.DataContracts.GroupExamList();
        //            g.GroupExamList.GroupExams = new Cpchs.Entities.WCF.DataContracts.GroupExamCollection();
        //            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.GroupExam p in gexlist.Items)
        //            {
        //                if (p.GroupExamId == g.GroupId)
        //                {
        //                    g.GroupExamList.GroupExams.Add(TranslateBetweenGroupExamBEAndGroupExamDC.TranslateGroupExamToGroupExam(p));
        //                }
        //            }
        //        }*/

        //        Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsResponse response = new Cpchs.Entities.WCF.MessageContracts.GetGroupsBySpecsResponse();
        //        response.GroupList = groups;


        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        ServiceEmpty pnf = new ServiceEmpty();
        //        pnf.Message = e.Message;
        //        throw new FaultException<ServiceEmpty>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GenericPatientFindResponse GenericPatientFind( Cpchs.Entities.WCF.MessageContracts.GenericPatientFindRequest request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
        //           Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GenericPatientFind(
        //               request.CompanyDb,
        //               request.PatType,
        //               request.PatPatId,
        //               request.PatNProc,
        //               request.PatName,
        //               request.PatNsns,
        //               request.PatBirthDate,
        //               request.PatSex,
        //               request.EpisodeType,
        //               request.EpisodeId,
        //               request.EpiDateBegin,
        //               request.EpiDateEnd,
        //               request.Doc,
        //               request.ExtId,
        //               request.DocType,
        //               request.Application,
        //               request.Place,
        //               request.Institution,
        //               request.DocDateBegin,
        //               request.DocEndDate,
        //               request.ValDateBegin,
        //               request.ValDateEnd,
        //               request.SearchType,
        //               request.GlobalFilters,
        //               request.DocsSessionFilters,
        //               request.ServsSessionFilters,
        //               request.UserName,
        //               request.UserResAna,
        //               request.PatID);

        //        Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
        //        patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items )
        //            patients.PatientCollection.Add( TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient( p ) );

        //        Cpchs.Entities.WCF.MessageContracts.GenericPatientFindResponse response =
        //            new Cpchs.Entities.WCF.MessageContracts.GenericPatientFindResponse();
        //        response.PatientList = patients;

        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesResponse GetPatientsBySamples( Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesRequest request )
        //{
        //   /* try
        //    {*/
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
        //           Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientBySamples(
        //               request.CompanyDb,
        //               request.Application,
        //               request.Place,
        //               request.Institution,
        //               request.MinDate,
        //               request.MaxDate,
        //               request.ReqService,
        //               request.ExecService,
        //               request.State,
        //               request.NMecan);

        //        Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
        //        patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

        //        IOrderedEnumerable<Patient> pats = patList.Items.OrderBy(f => f.Order);

        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in pats)
        //            patients.PatientCollection.Add( TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient( p ) );

                

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesResponse response =
        //            new Cpchs.Entities.WCF.MessageContracts.GetPatientsBySamplesResponse();
        //        response.PatientList = patients;

        //        return response;
        //    /*}
        //    catch( Exception e )
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>( pnf, pnf.Message );
        //    }*/
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorResponse GetServicesForDoctor( Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorRequest request )
        //{
        //    Cpchs.Entities.WCF.DataContracts.ServiceCollection services = new Cpchs.Entities.WCF.DataContracts.ServiceCollection();
        //    services = TranslateBetweenServiceListAndServiceCollection.TranslateServicesToServices( Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetServicesForDoctor( request.CompanyDb, request.NMecan, request.Place, request.Institution, request.ReqService, request.Scope ) );
        //    Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorResponse response = new Cpchs.Entities.WCF.MessageContracts.GetServicesForDoctorResponse();
        //    response.ServiceList = new Cpchs.Entities.WCF.DataContracts.Services();
        //    response.ServiceList.ServiceCollection = services;
        //    return response;
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Response GetSearchFiltersDataV2( Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Request request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Response response = new Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV2Response();
        //    Cpchs.Entities.WCF.DataContracts.FiltersData filtersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();


        //    filtersData.Genders =
        //        TranslateBetweenGenderListAndGenderCollection.TranslateGenresToGenres(
        //            Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllGenders( request.CompanyDb ) );
        //    filtersData.Genders.Insert( 0, new Cpchs.Entities.WCF.DataContracts.Gender()
        //    {
        //        Id = -1,
        //        Code = " ",
        //        Acronym = " ",
        //        Description = ""
        //    } );


        //    filtersData.EpisodeTypes =
        //        TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection.TranslateEpisodeTypesToEpisodeTypes(
        //            Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetEpiTypesForExternalAccess(
        //                request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId,
        //                request.DocTypeId, request.DocRef, request.ElemType ) );
        //    filtersData.EpisodeTypes.Insert( 0, new Cpchs.Entities.WCF.DataContracts.EpisodeType()
        //    {
        //        Id = -1,
        //        Code = " ",
        //        Acronym = " ",
        //        Description = ""
        //    } );


        //    filtersData.PatientTypes =
        //        TranslateBetweenPatientTypeListAndPatientTypeCollection.TranslatePatientTypesToPatientTypes(
        //            Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPatientTypes( request.CompanyDb ) );
        //    filtersData.PatientTypes.Insert( 0, new Cpchs.Entities.WCF.DataContracts.PatientType()
        //    {
        //        Id = -1,
        //        Code = " ",
        //        Acronym = " ",
        //        Description = ""
        //    } );


        //    filtersData.Institutions =
        //        TranslateBetweenInstitutionListAndInstitutionCollection.TranslateInstitutionsToInstitutions(
        //            Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetDocTypesTreeForExternalAccess(
        //                request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId,
        //                request.DocTypeId, request.DocRef, request.ElemType ) );

        //    filtersData.FiltersEffect = false;
        //    /*
        //    filtersData.FiltersEffect =
        //        Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetFiltersActivity(
        //                request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
        //                request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location);
        //    */

        //    filtersData.Services =
        //        TranslateBetweenServiceListAndServiceCollection.TranslateServicesToServices(
        //            Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServicesForExternalAccess(
        //                request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId,
        //                request.DocTypeId, request.DocRef, request.ElemType ) );
        //    filtersData.Services.Insert( 0, new Cpchs.Entities.WCF.DataContracts.Service()
        //    {
        //        Id = -1,
        //        Code = " ",
        //        Description = ""
        //    } );

        //    response.FiltersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();
        //    response.FiltersData = filtersData;
        //    return response;
        //}


        // para acesso externo em que não temos os docstypes. vai-se buscar mesmo todas as instituições
        public override Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Response GetSearchFiltersDataV4(Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Request request)
        {
            Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Response response = new Cpchs.Entities.WCF.MessageContracts.GetSearchFiltersDataV4Response();
            Cpchs.Entities.WCF.DataContracts.FiltersData filtersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();

            filtersData.EpisodeTypes =
                TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection.TranslateEpisodeTypesToEpisodeTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetEpiTypesForExternalAccess(
                        request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId,
                        request.DocTypeId, request.DocRef, request.ElemType));
            filtersData.EpisodeTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.EpisodeType()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.Genders =
                        TranslateBetweenGenderListAndGenderCollection.TranslateGenresToGenres(
                            Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllGenders(request.CompanyDb));
            filtersData.Genders.Insert(0, new Cpchs.Entities.WCF.DataContracts.Gender()
            {
                Id = -1,
                Code = " ",
                Acronym = " ",
                Description = ""
            });

            filtersData.PatientTypes =
                TranslateBetweenPatientTypeListAndPatientTypeCollection.TranslatePatientTypesToPatientTypes(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPatientTypes(request.CompanyDb, request.UserName));
            filtersData.PatientTypes.Insert(0, new Cpchs.Entities.WCF.DataContracts.PatientType()
            {
                Id = -99,
                Code = " ",
                Acronym = " ",
                Description = ""
            });


            filtersData.Institutions =
                TranslateBetweenInstitutionListAndInstitutionCollection.TranslateInstitutionsToInstitutions(
                    Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetInstitutionsForExternalAccess(
                        request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId, request.DocTypeId, request.DocRef, request.ElemType));

            filtersData.FiltersEffect = false;
            /*
            filtersData.FiltersEffect =
                Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetFiltersActivity(
                        request.CompanyDb, request.GlobalFilters, request.DocsSessionFilters,
                        request.ServsSessionFilters, request.UserName, request.AnaResAccess, request.Location);
            */

            filtersData.Services =
                TranslateBetweenServiceListAndServiceCollection.TranslateServicesToServices(
                    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServicesForExternalAccess(
                        request.CompanyDb, request.EntId, request.EpisodeTypeId, request.EpisodeId, request.InstId, request.PlaceId, request.AppId,
                        request.DocTypeId, request.DocRef, request.ElemType));
            filtersData.Services.Insert(0, new Cpchs.Entities.WCF.DataContracts.Service()
            {
                Id = -1,
                Code = " ",
                Description = ""
            });

            response.FiltersData = new Cpchs.Entities.WCF.DataContracts.FiltersData();
            response.FiltersData = filtersData;
            return response;
        }


        //public override Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersResponse GetApplicationsForFilters( Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersResponse response = new Cpchs.Entities.WCF.MessageContracts.GetApplicationsForFiltersResponse();

        //    Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllApplications( request.CompanyDb );
        //    response.Applications = new Cpchs.Entities.WCF.DataContracts.Applications();
        //    response.Applications.ApplicationCollection = TranslateBetweenApplicationListAndApplicationCollection.TranslateApplicationsToApplications( Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllApplications( request.CompanyDb ) );

        //    return response;
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientByIdResponse GetPatientById( Cpchs.Entities.WCF.MessageContracts.GetPatientByIdRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetPatientByIdResponse response = new Cpchs.Entities.WCF.MessageContracts.GetPatientByIdResponse();

        //    try
        //    {

        //        response.Patient = TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient( Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientById( request.CompanyDb, request.EntId ) );
        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsResponse GetPatientEpisodeByIds( Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsResponse response = new Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodeByIdsResponse();

        //    try
        //    {
        //        response.Episode = TranslateBetweenVisitBeAndPatientEpisodeDC.TranslateVisitToPatientEpisode( Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientEpisodeByIds( request.CompanyDb, request.Episode, request.EpisodeType ) );
        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        EpisodeNotFound pnf = new EpisodeNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<EpisodeNotFound>( pnf, pnf.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionResponse GetEntityInSession( Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionResponse response = new Cpchs.Entities.WCF.MessageContracts.GetEntityInSessionResponse();
        //    try
        //    {
        //        ErEntity entity = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetEntityInSession( request.CompanyDb, request.Session, request.Section );
        //        response.Entity = TranslateBetweenErEntityBEAndEntityDC.TranslateErEntityToEntity(entity);
        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityResponse GetOrInsertEntity( Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityResponse response = new Cpchs.Entities.WCF.MessageContracts.GetOrInsertEntityResponse();

        //    try
        //    {
        //        response.Entity = new Cpchs.Entities.WCF.DataContracts.Entity();

        //        ErEntity ent = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertEntity( request.CompanyDb, request.Name, request.NMecan, Convert.ToString(request.ReqService), request.Type, request.InstId, request.PlaceId, request.AppId );

        //        response.Entity.EntityId = Convert.ToString( ent.EntId );
        //        response.Entity.EntityName = ent.EntName;

        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeResponse GetOrInsertEpisode( Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeResponse response = new Cpchs.Entities.WCF.MessageContracts.GetOrInsertEpisodeResponse();

        //    try
        //    {
        //        response.Episode = new Cpchs.Entities.WCF.DataContracts.PatientEpisode();

        //        Cpchs.Eresults.Common.WCF.BusinessEntities.Visit ent = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertEpisode( request.CompanyDb, request.Episode, null, request.PatEntId, null, request.InstId, request.PlaceId, request.AppId );

        //        response.Episode.Episode = ent.VisitEpisode;
        //        response.Episode.EpisodeTypeId = ent.VisitEpiTypeId;
        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientResponse GetOrInsertPatient( Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientResponse response = new Cpchs.Entities.WCF.MessageContracts.GetOrInsertPatientResponse();

        //    try
        //    {
        //        response.Patient = new Cpchs.Entities.WCF.DataContracts.Patient();

        //        Cpchs.Eresults.Common.WCF.BusinessEntities.Patient ent = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertPatient( request.CompanyDb, request.PatientId, request.PatientType, request.InstId, request.PlaceId, request.AppId );

        //        response.Patient.EntityIds = ent.patEntIds;

        //        return response;
        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientResponse GetPatientEpisodesByGenericPatient( Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientRequest request )
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientResponse response = new Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesByGenericPatientResponse();

        //    try
        //    {
        //        response.Episodes = new Cpchs.Entities.WCF.DataContracts.PatientEpisodes();

        //        var result =
        //            Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientEpisodesByGenericPatient( request.CompanyDb, request.TDoente, request.Doente );

        //        response.Episodes.EpisodesList = TranslateBetweenVisitListBEAndPatientEpisodeCollectionDC.TranslateVisitListToPatientEpisodeCollection( result );

        //        return response;
        //    }
        //    catch(Exception e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesResponse GetPatientEpisodes( Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesRequest request )
        //{

        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList visits = new Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList();

        //        visits = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientEpisodes( request.CompanyDb, request.PatientId, request.LocalId, request.InstId );

        //        Cpchs.Entities.WCF.DataContracts.PatientEpisodes pes = new Cpchs.Entities.WCF.DataContracts.PatientEpisodes();
        //        pes.EpisodesList = new Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Visit v in visits.Items )
        //        {
        //            pes.EpisodesList.Add( TranslateBetweenVisitBeAndPatientEpisodeExamDC.TranslateVisitToPatientEpisode( v ) );
        //        }

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesResponse response = new Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesResponse();

        //        response.EpisodesList = pes;

        //        return response;

        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException( e.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataResponse GetPatientEpisodesBySearchData( Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataRequest request )
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList visits = new Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList();

        //        visits = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientEpisodesBySearchData( request.CompanyDb, request.PatientName, request.PatientNr, request.PatientProcessNr, request.PatientSns, request.Institution, request.Place, request.PatientID );

        //        Cpchs.Entities.WCF.DataContracts.PatientEpisodes pes = new Cpchs.Entities.WCF.DataContracts.PatientEpisodes();
        //        pes.EpisodesList = new Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection();

        //        foreach( Cpchs.Eresults.Common.WCF.BusinessEntities.Visit v in visits.Items )
        //        {
        //            pes.EpisodesList.Add( TranslateBetweenVisitBeAndPatientEpisodeExamDC.TranslateVisitToPatientEpisode( v ) );
        //        }

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataResponse response = new Cpchs.Entities.WCF.MessageContracts.GetPatientEpisodesBySearchDataResponse();

        //        response.EpisodesList = pes;

        //        return response;

        //    }
        //    catch( Exception e )
        //    {
        //        throw new FaultException( e.Message );
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestResponse GetPatientByRequest(Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestRequest request)
        //{
        //    try
        //    {
        //        Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
        //           Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientByRequest(
        //               request.CompanyDb,
        //               request.RequisitionId,
        //               request.ElementId
        //               );

        //        Cpchs.Entities.WCF.DataContracts.Patients patients =
        //        new Cpchs.Entities.WCF.DataContracts.Patients();
        //        patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items)
        //            patients.PatientCollection.Add(TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient(p));

        //        Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestResponse response =
        //            new Cpchs.Entities.WCF.MessageContracts.GetPatientByRequestResponse();
        //        response.PatientList = patients;

        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        PatientNotFound pnf = new PatientNotFound();
        //        pnf.Message = e.Message;
        //        throw new FaultException<PatientNotFound>(pnf, pnf.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsResponse GetTerapeutics(Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsRequest request)
        //{
        //    Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsResponse response = new Cpchs.Entities.WCF.MessageContracts.GetTerapeuticsResponse();

        //    try
        //    {
        //        response.TerapeuticsList = new Cpchs.Entities.WCF.DataContracts.TerapeuticList();

        //        Cpchs.Eresults.Common.WCF.BusinessEntities.TerapeuticList result =
        //            Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetTerapeutics(request.CompanyDb);

        //        response.TerapeuticsList.Terapeutics = TranslateBetweenTerapeuticListBEAndTerapeuticCollectionDC.TranslateTerapeuticListToTerapeuticCollection(result);

        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new FaultException(e.Message);
        //    }
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupResponse GetGroupExamsByGroup(Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupRequest request)
        //{
        //    /*Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList reqTypes = new Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList();

        //    reqTypes = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetTypes(request.CompanyDb);

        //    Cpchs.Entities.WCF.DataContracts.ExamTypeCollection tplist = new Cpchs.Entities.WCF.DataContracts.ExamTypeCollection();

        //    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionType p in reqTypes.Items)
        //    {
        //        tplist.Add(TranslateBetweenRequisitionTypeBeAndExamTypeDc.TranslateRequisitionTypeBeToTypeDc(p));
        //    }

        //    // priorities
        //    Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList priorities = new Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList();

        //    priorities = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetPriorities(request.CompanyDb);

        //    Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection plist = new Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection();

        //    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Priority p in priorities.Items)
        //    {
        //        plist.Add(TranslateBetweenPriorityBeAndExamPriorityDc.TranslatePriorityBeToPriorityDc(p));
        //    }

        //    // exec places
        //    Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList execplace = new Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList();

        //    execplace = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetExecPlaces(request.CompanyDb);

        //    Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection elist = new Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection();

        //    foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlace e in execplace.Items)
        //    {
        //        elist.Add(TranslateBetweenExecPlaceBeAndExecPlaceDc.TranslateExecPlaceBeToExecPlaceDc(e));
        //    }
        //    */
        //    GroupExamList gel = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetGroupExamsByGroup(request.CompanyDb, request.GroupId, request.Inst, request.Place);

        //    Cpchs.Entities.WCF.DataContracts.GroupExamList gelist = new Cpchs.Entities.WCF.DataContracts.GroupExamList();
        //    gelist.GroupExams = new Cpchs.Entities.WCF.DataContracts.GroupExamCollection();

        //    if (gel.Count != 0)
        //    {
        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.GroupExam ge in gel.Items)
        //        {
        //            Cpchs.Entities.WCF.DataContracts.GroupExam gexam = TranslateBetweenGroupExamBEAndGroupExamDC.TranslateGroupExamToGroupExam(ge);
        //            //gexam.ExamTypeCollection = tplist;
        //            //gexam.ExamPriorityCollection = plist;
        //            //gexam.ExamExecPlaceCollection = elist;
        //            gelist.GroupExams.Add(gexam);
        //        }
        //    }

        //    Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupResponse response = new Cpchs.Entities.WCF.MessageContracts.GetGroupExamsByGroupResponse();
        //    response.ExamsList = gelist;
        //    return response;
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaResponse GetExamsBySearchCriteria(Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaRequest request)
        //{
        //    GroupExamList gel = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetExamsBySearchCriteria(request.CompanyDb, request.Institution, request.Place, request.SearchKey.ToUpper(), request.NMecan, request.ReqService, request.ExecService, request.PaginationInfo != null ? request.PaginationInfo.PageNumber : null, request.PaginationInfo != null ? request.PaginationInfo.ItemsPerPage : null, request.PaginationInfo != null ? request.PaginationInfo.OrderField : null, request.PaginationInfo != null ? request.PaginationInfo.OrderType.ToString() : null);

        //    Cpchs.Entities.WCF.DataContracts.GroupExamList gelist = new Cpchs.Entities.WCF.DataContracts.GroupExamList();
        //    gelist.GroupExams = new Cpchs.Entities.WCF.DataContracts.GroupExamCollection();

        //    if (gel.Count != 0)
        //    {
        //        foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.GroupExam ge in gel.Items)
        //        {
        //            Cpchs.Entities.WCF.DataContracts.GroupExam gexam = TranslateBetweenGroupExamBEAndGroupExamDC.TranslateGroupExamToGroupExam(ge);
        //            //gexam.ExamTypeCollection = tplist;
        //            //gexam.ExamPriorityCollection = plist;
        //            //gexam.ExamExecPlaceCollection = elist;
        //            gelist.GroupExams.Add(gexam);
        //        }
        //    }

        //    //types 
        //    //Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList reqTypes = new Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionTypeList();

        //    //reqTypes = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetTypes(request.CompanyDb);

        //    //Cpchs.Entities.WCF.DataContracts.ExamTypeCollection tplist = new Cpchs.Entities.WCF.DataContracts.ExamTypeCollection();

        //    //foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.RequisitionType p in reqTypes.Items)
        //    //{
        //    //    tplist.Add(TranslateBetweenRequisitionTypeBeAndExamTypeDc.TranslateRequisitionTypeBeToTypeDc(p));
        //    //}

        //    //Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList priorities = new Cpchs.Eresults.Common.WCF.BusinessEntities.PriorityList();

        //    //priorities = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetPriorities(request.CompanyDb);

        //    //Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection plist = new Cpchs.Entities.WCF.DataContracts.ExamPriorityCollection();

        //    //foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Priority p in priorities.Items)
        //    //{
        //    //    plist.Add(TranslateBetweenPriorityBeAndExamPriorityDc.TranslatePriorityBeToPriorityDc(p));
        //    //}

        //    // exec places
        //    //Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList execplace = new Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlaceList();

        //    //execplace = Cpchs.Documents.WCF.BusinessLogic.RequisitionLogic.GetExecPlaces(request.CompanyDb);

        //    //Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection elist = new Cpchs.Entities.WCF.DataContracts.ExamExecPlaceCollection();

        //    //foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.ExecPlace e in execplace.Items)
        //    //{
        //    //    elist.Add(TranslateBetweenExecPlaceBeAndExecPlaceDc.TranslateExecPlaceBeToExecPlaceDc(e));
        //    //}

        //    Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaResponse response = new Cpchs.Entities.WCF.MessageContracts.GetExamsBySearchCriteriaResponse();
        //    response.ExamsList = gelist;
        //    //response.ExamTypes = tplist;
        //    //response.ExecPlaces = elist;
        //    //response.Priorities = plist;
        //    return response;
        //}

        //public override Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationResponse ExternalAccessInitialization(Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationRequest request)
        //{
        //    Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationResponse mc = new Cpchs.Entities.WCF.MessageContracts.ExternalAccessInitializationResponse();
        //    mc.Response = new Cpchs.Entities.WCF.DataContracts.ExternalAccessData();

        //    if (!request.RequiresGetOrInsert)
        //    {

        //        #region EpisodeType
        //        if (!string.IsNullOrEmpty(request.EpisodeType))
        //        {
        //            EpisodeType episodeType = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetEpisodeTypeByCode(request.CompanyDB, request.EpisodeType);
        //            if (episodeType != null)
        //            {
        //                mc.Response.EpisodeTypeId = episodeType.EpiTypeId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received episodeType is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region Institution
        //        if (!string.IsNullOrEmpty(request.Institution))
        //        {
        //            Institution inst = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetInstitutionByCode(request.CompanyDB, request.Institution);
        //            if (inst != null)
        //            {
        //                mc.Response.InstitutionId = inst.InstitutionId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received institution is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region Place
        //        if (!string.IsNullOrEmpty(request.Place))
        //        {
        //            Place place = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPlaceByCode(request.CompanyDB, request.Place);
        //            if (place != null)
        //            {
        //                mc.Response.PlaceId = place.PlaceId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received place is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region Application
        //        if (!string.IsNullOrEmpty(request.Application))
        //        {
        //            Application app = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetApplicationByCode(request.CompanyDB, request.Application);
        //            if (app != null)
        //            {
        //                mc.Response.ApplicationId = app.ApplicationId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received application is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region DocumentType
        //        if (!string.IsNullOrEmpty(request.DocumentType))
        //        {
        //            DocumentType docType = Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentManagementBER.Instance.GetDocTypeByCode(request.CompanyDB, mc.Response.ApplicationId.Value, request.DocumentType);
        //            if (docType != null)
        //            {
        //                mc.Response.DocumentTypeId = docType.DocumentTypeId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received documentType is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region RequisitionService
        //        if (!string.IsNullOrEmpty(request.RequisitionService))
        //        {
        //            Service serv = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServiceByCode(request.CompanyDB, request.RequisitionService);
        //            if (serv != null)
        //            {
        //                mc.Response.RequisitionServiceId = serv.ServiceId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received requisitionService is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region ExecutionService
        //        if (!string.IsNullOrEmpty(request.ExecutionService))
        //        {
        //            Service serv = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServiceByCode(request.CompanyDB, request.ExecutionService);
        //            if (serv != null)
        //            {
        //                mc.Response.ExecutionServiceId = serv.ServiceId;
        //                mc.Response.ExecutionServiceDesc = serv.ServiceDescription;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "The received executionService is not valid";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //    }
        //    else
        //    {

        //        #region DoctorId
                
        //        if (request.RequiresGetOrInsert && !string.IsNullOrEmpty(request.NMecan) && !string.IsNullOrEmpty(request.RequisitionService))
        //        {
        //            ErEntity ent = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertEntity(request.CompanyDB, string.Empty, request.NMecan, request.RequisitionService, request.NMecanType, mc.Response.InstitutionId.HasValue ? mc.Response.InstitutionId.Value : -1, mc.Response.PlaceId.HasValue ? mc.Response.PlaceId.Value : -1, -1);
        //            if (ent != null)
        //            {
        //                mc.Response.DoctorId = ent.EntId;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "O número mecanográfico recebido não é válido.";
        //                return mc;
        //            }
        //        }
        //        #endregion

        //        #region EntityId
                
        //        if (request.RequiresGetOrInsert && !string.IsNullOrEmpty(request.PatientType) && !string.IsNullOrEmpty(request.PatientId))
        //        {
        //            var pat = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertPatient(request.CompanyDB, request.PatientId, request.PatientType, mc.Response.InstitutionId.HasValue ? mc.Response.InstitutionId.Value : -1, mc.Response.PlaceId.HasValue ? mc.Response.PlaceId.Value : -1, -1);
        //            if (pat != null)
        //            {
        //                mc.Response.EntityId = pat.patEntIds;
        //            }
        //            else
        //            {
        //                mc.Response.Error = "O paciente recebido não é válido.";
        //                return mc;
        //            }
        //        }
                
        //        #endregion

        //        #region Episode
        //        if (request.RequiresGetOrInsert && !string.IsNullOrEmpty(request.EpisodeId) && !string.IsNullOrEmpty(request.EpisodeType))
        //        {
        //            try
        //            {
        //                var visit = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetOrInsertEpisode(request.CompanyDB, request.EpisodeId, request.EpisodeType, request.PatientId, request.PatientType, mc.Response.InstitutionId.HasValue ? mc.Response.InstitutionId.Value : -1, mc.Response.PlaceId.HasValue ? mc.Response.PlaceId.Value : -1, -1);
        //                if (visit != null)
        //                {
        //                    mc.Response.EpisodeInserted = true;
        //                    mc.Response.EpiTypeExt = visit.EpiTypeExt;
        //                }
        //                else
        //                {
        //                    mc.Response.Error = "O episódio recebido não é válido";
        //                    return mc;
        //                }
        //            }
        //            catch(Exception e)
        //            {
        //                mc.Response.Error = "A importação dos dados do doente/episódio falhou.";
        //                return mc;
        //            }
        //        }
        //        #endregion
        //    }

        //    return mc;
        //}

        //public override Cpchs.Entities.WCF.DataContracts.EpisodeTypeCollection GetEpisodeTypesFromExternalProvider(string companyDB)
        //{
        //    Cpchs.Entities.WCF.DataContracts.EpisodeTypeCollection to = new DataContracts.EpisodeTypeCollection();

        //    EpisodeTypeList list = EpisodeManagementBER.Instance.GetEpisodeTypesFromExternalProvider(companyDB);

        //    if (list != null)
        //        to = TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection.TranslateEpisodeTypesToEpisodeTypes(list);

        //    return to;
        //}

        //public override Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection GetEpisodesFromExternalProvider(string companyDB, string tDoente, string doente, string tEpisode, string episode)
        //{
        //    Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection to = new DataContracts.PatientEpisodeCollection();

        //    VisitList list = EpisodeManagementBER.Instance.GetEpisodesFromExternalProvider(companyDB, tDoente, doente, tEpisode, episode);

        //    if (list != null)
        //        to = TranslateBetweenVisitListBEAndPatientEpisodeCollectionDC.TranslateVisitListToPatientEpisodeCollection(list);

        //    return to;
        //}

        //public override Cpchs.Entities.WCF.DataContracts.PatientEpisode ImportEpisodeToPatient(string companyDB, string tDoente, string doente, string tEpisode, string episode)
        //{
        //    Cpchs.Entities.WCF.DataContracts.PatientEpisode to = new DataContracts.PatientEpisode();

        //    VisitList list = EpisodeManagementBER.Instance.ImportEpisodeToPatientFromExternalProvider(companyDB, tDoente, doente, tEpisode, episode);

        //    if (list != null && list.Count > 0)
        //    {
        //        to = TranslateBetweenVisitBeAndPatientEpisodeDC.TranslateVisitToPatientEpisode(list[0]);
        //        return to;
        //    }
        //    else
        //        return null;
        //}

        //public override Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection SearchEpisode(string companyDB, string tEpisode, string episode)
        //{
        //    Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection to = new DataContracts.PatientEpisodeCollection();

        //    VisitList list = EpisodeManagementBER.Instance.SearchEpisode(companyDB, tEpisode, episode);

        //    if (list != null)
        //    {
        //        Cpchs.Entities.WCF.DataContracts.PatientEpisodeCollection listAux =
        //            TranslateBetweenVisitListBEAndPatientEpisodeCollectionDC.TranslateVisitListToPatientEpisodeCollection(list);

        //        if (listAux.Count == 1)
        //            to = listAux;
        //        else
        //        {
        //            to.Add(listAux[0]);
        //            if (to[0] != null)
        //            {
        //                to[0].Patient = string.Empty;
        //                to[0].PatientType = string.Empty;
        //            }
        //        }
        //    }

        //    return to;
        //}

        public override CheckUserSettingsResponse CheckUserSettings(CheckUserSettingsRequest request)
        {
            CheckUserSettingsResponse resp = new CheckUserSettingsResponse();
            resp.HasSettings = Cpchs.Entities.WCF.BusinessLogic.EntityLogic.CheckUserSettings(
                           request.CompanyDb,
                           request.Username,
                           request.Application,
                           request.Module);
            return resp;
        }

        public override GetEpisodeTypeByCodeResponse GetEpisodeTypeByCode(GetEpisodeTypeByCodeRequest req)
        {
            GetEpisodeTypeByCodeResponse resp = new GetEpisodeTypeByCodeResponse();
            resp.EpisodeType = TranslateBetweenEpisodeTypeBEAndEpisodeTypeDC.TranslateEpisodeTypeToEpisodeType(
                EntityLogic.GetEpisodeTypeByCode(req.CompanyDb, req.EpisodeCode)
            );
            return resp;
        }

        public override Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsResponse GetPatientsBySpecs(Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsRequest request)
        {
            try
            {
                Cpchs.Eresults.Common.WCF.BusinessEntities.PatientList patList =
                   Cpchs.Entities.WCF.BusinessLogic.EntityLogic.GetPatientsBySpecs(
                       request.CompanyDb,
                       request.Search,
                       request.GlobalFilters,
                   //    request.DocsSessionFilters,
                       request.ServsSessionFilters,
                       request.UserName);

                Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
                patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

                foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.Patient p in patList.Items)
                {
                    patients.PatientCollection.Add(TranslateBetweenPatientBEAndPatientDC.TranslatePatientToPatient(p));
                }

                Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsResponse response =
                    new Cpchs.Entities.WCF.MessageContracts.GetPatientsBySpecsResponse();
                response.PatientList = patients;

                return response;
            }
            catch (Exception e)
            {
                PatientNotFound pnf = new PatientNotFound();
                pnf.Message = e.Message;
                throw new FaultException<PatientNotFound>(pnf, pnf.Message);
            }
        }
    }
}
