using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.Linq;

namespace Cpchs.Entities.WCF.BusinessLogic
{
    public class EntityLogic
    {

        private static void ParseCommonParameters(
            ref string globalFilters,
            ref string docsSessionFilters,
            ref string servsSessionFilters,
            ref string showArch,
            ref string showLinks,
            ref string showAnaRes,
            bool anaResAccess,
            string location)
        {
            // determinar os selectores de tipos de documentos
            showArch = "I";
            showLinks = "I";
            showAnaRes = "I";
            if (!string.IsNullOrEmpty(location))
            {
                switch (location.ToUpper())
                {
                    case "UPLOAD":
                        showArch = "S";
                        showAnaRes = "I";
                        showLinks = "I";
                        break;
                    case "EXAM":
                        showArch = "I";
                        showAnaRes = "S";
                        showLinks = "I";
                        break;
                }
            }

            // determinar as variáveis de filtros globais
            if (globalFilters == null || globalFilters == "N")
            {
                globalFilters = "N";
            }
            else
            {
                globalFilters = "S";
            }
            if (docsSessionFilters == null || docsSessionFilters == "N")
            {
                docsSessionFilters = "N";
            }
            else
            {
                docsSessionFilters = "S";
            }
            if (servsSessionFilters == null || servsSessionFilters == "N")
            {
                servsSessionFilters = "N";
            }
            else
            {
                servsSessionFilters = "S";
            }
        }

        public static bool GetFiltersActivity(string companyDB, string globalFilters, string docsSessionFilters, string servsSessionFilters, string userName, bool anaResAccess, string location)
        {
            string showArch = "";
            string showLinks = "";
            string showAnaRes = "";

            ParseCommonParameters(ref globalFilters, ref docsSessionFilters, ref servsSessionFilters, ref showArch, ref showLinks, ref showAnaRes, anaResAccess, location);

            return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetFiltersActivity(
                companyDB, globalFilters, docsSessionFilters, servsSessionFilters, userName, showArch, showAnaRes, showLinks) == "S" ? true : false;
        }

        public static InstitutionList GetInstPlaceAppDocType(string companyDB, string globalFilters, string docsSessionFilters, string servsSessionFilters, string userName, bool anaResAccess, string location)
        {
            string showArch = "";
            string showLinks = "";
            string showAnaRes = "";

            ParseCommonParameters(ref globalFilters, ref docsSessionFilters, ref servsSessionFilters, ref showArch, ref showLinks, ref showAnaRes, anaResAccess, location);

            // obter todos os tipos de documentos válidos para o ambiente de execução definido
            DocumentTypeList allDocTypes = new DocumentTypeList();
            allDocTypes = Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentManagementBER.Instance.GetAllDocTypes(companyDB, globalFilters, docsSessionFilters, userName, showArch, showAnaRes, showLinks);

            // obter todas as definições de hieraquias dos tipos de documentos
            DocHierarchyList allHierarchy = new DocHierarchyList();
            allHierarchy = Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentManagementBER.Instance.GetAllHierarchyForDocument(companyDB);

            // adicionar a própria identificação à lista de ids que representa
            foreach (DocumentType dt in allDocTypes.Items)
            {
                dt.DocumentTypeIds = new List<DocumentWrapper>();
                dt.DocumentTypeIds.Add(new DocumentWrapper(dt.DocumentTypeInstitutionId, dt.DocumentTypePlaceId, dt.DocumentTypeApplicationId, dt.DocumentTypeId, dt.DocumentTypeDescription));
            }

            // determinar tipos de documento filhos e pais
            DocumentTypeList childDocTypes = new DocumentTypeList();
            childDocTypes = TransferChilds(allDocTypes, allHierarchy);
            allDocTypes = DeleteChilds(allDocTypes, childDocTypes);

            // construir sub-hierarquia de tipos de documentos
            foreach (DocumentType doc in allDocTypes.Items)
            {
                AddChilds(doc, childDocTypes, allHierarchy);
            }

            // identificar as várias instituições
            List<long> tempIds = new List<long>();
            InstitutionList instList = new InstitutionList();
            foreach (DocumentType dt in allDocTypes.Items)
            {
                if (!tempIds.Contains(dt.DocumentTypeInstitutionId))
                {
                    instList.Add(new Institution(dt.DocumentTypeInstitutionId, dt.DocumentTypeInstitutionCode, dt.DocumentTypeInstitutionAcronym, dt.DocumentTypeInstitutionDesc));
                    tempIds.Add(dt.DocumentTypeInstitutionId);
                }
            }

            // determinar os locais de cada uma das instituições
            GetAllPlacesForInstitutions(allDocTypes, instList);

            // adicionar nó global às aplicações (para ser possível apresentar todos os tipos de documento sem apresentar as aplicações)
            AddGlobalNodeToApplications(instList);

            // adicionar nó global aos locais (para ser possível apresentar todos os tipos de documento sem apresentar os locais)
            AddGlobalNodeToPlaces(instList);

            // adicionar nó global às instituições (para ser possível apresentar todos os tipos de documento sem apresentar as instituições)
            AddGlobalNodeToInstitutions(instList);

            // ordenar alfabeticamente cada um dos níveis (os nós globais serão os primeiros das listas)
            foreach (Institution inst in instList.Items)
            {
                foreach (Place place in inst.InstitutionPlaceList.Items)
                {
                    foreach (Application app in place.PlaceApplicationList.Items)
                    {
                        app.ApplicationDocumentTypes.Sort(new DocumentTypeComparer());
                    }
                    place.PlaceApplicationList.Sort(new ApplicationComparer());
                }
                inst.InstitutionPlaceList.Sort(new PlaceComparer());
            }
            instList.Sort(new InstitutionComparer());

            return instList;
        }

        private static DocumentTypeList TransferChilds(DocumentTypeList allDocuments, DocHierarchyList hierarchy)
        {
            DocumentTypeList childDocuments = new DocumentTypeList();
            bool flag = false;
            foreach (DocumentType doc in allDocuments.Items)
            {
                foreach (DocHierarchy hier in hierarchy.Items)
                {
                    if (doc.DocumentTypeInstitutionId == hier.DocHierarchyInstId &&
                        doc.DocumentTypePlaceId == hier.DocHierarchyPlaceId &&
                        doc.DocumentTypeApplicationId == hier.DocHierarchyAppId &&
                        doc.DocumentTypeId == hier.DocHierarchyDocTypeId)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        childDocuments.Add(doc);
                        flag = false;
                    }
                }
            }
            return childDocuments;
        }

        private static DocumentTypeList DeleteChilds(DocumentTypeList allDocuments, DocumentTypeList childDocuments)
        {
            DocumentTypeList allDocs = new DocumentTypeList();
            bool flag = false;
            foreach (DocumentType docPar in allDocuments.Items)
            {
                foreach (DocumentType docChi in childDocuments.Items)
                {
                    if (docPar.DocumentTypeInstitutionId == docChi.DocumentTypeInstitutionId &&
                        docPar.DocumentTypePlaceId == docChi.DocumentTypePlaceId &&
                        docPar.DocumentTypeApplicationId == docChi.DocumentTypeApplicationId &&
                        docPar.DocumentTypeId == docChi.DocumentTypeId)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    allDocs.Add(docPar);
                }
                flag = false;
            }
            return allDocs;
        }

        private static void AddChilds(DocumentType document, DocumentTypeList childDocuments, DocHierarchyList hierarchy)
        {
            foreach (DocumentType doc in childDocuments.Items)
            {
                foreach (DocHierarchy hier in hierarchy.Items)
                {
                    if (doc.DocumentTypeInstitutionId == hier.DocHierarchyInstId &&
                        doc.DocumentTypePlaceId == hier.DocHierarchyPlaceId &&
                        doc.DocumentTypeApplicationId == hier.DocHierarchyAppId &&
                        doc.DocumentTypeId == hier.DocHierarchyDocTypeId &&
                        hier.DocHierarchyParentId == document.DocumentTypeId)
                    {
                        AddChilds(doc, childDocuments, hierarchy);
                        document.DocumentTypeChilds.Add(doc);
                    }
                }
            }
        }

        private static void GetAllPlacesForInstitutions(DocumentTypeList allDocTypes, InstitutionList instList)
        {
            List<long> ids;
            foreach (Institution inst in instList.Items)
            {
                ids = new List<long>();
                inst.InstitutionPlaceList = new PlaceList();
                foreach (DocumentType dt in allDocTypes.Items)
                {
                    if (dt.DocumentTypeInstitutionId == inst.InstitutionId &&
                        !ids.Contains(dt.DocumentTypePlaceId))
                    {
                        inst.InstitutionPlaceList.Add(new Place(dt.DocumentTypeInstitutionId, dt.DocumentTypePlaceId, dt.DocumentTypePlaceCode, dt.DocumentTypePlaceAcronym, dt.DocumentTypePlaceDesc));
                        ids.Add(dt.DocumentTypePlaceId);
                    }
                }

                // determinar as aplicações de cada um dos locais
                GetAllApplicationsForPlaces(allDocTypes, inst.InstitutionPlaceList);
            }
        }

        private static void GetAllApplicationsForPlaces(DocumentTypeList allDocTypes, PlaceList placeList)
        {
            List<long> ids;
            foreach (Place place in placeList.Items)
            {
                ids = new List<long>();
                place.PlaceApplicationList = new ApplicationList();
                foreach (DocumentType dt in allDocTypes.Items)
                {
                    if (dt.DocumentTypeInstitutionId == place.PlaceInstitutionId &&
                        dt.DocumentTypePlaceId == place.PlaceId &&
                        !ids.Contains(dt.DocumentTypeApplicationId))
                    {
                        place.PlaceApplicationList.Add(new Application(dt.DocumentTypeInstitutionId, dt.DocumentTypePlaceId, dt.DocumentTypeApplicationId, dt.DocumentTypeApplicationCode, dt.DocumentTypeApplicationAcronym, dt.DocumentTypeApplicationDesc));
                        ids.Add(dt.DocumentTypeApplicationId);
                    }
                }

                // determinar os tipos de documento para cada uma das aplicações
                GetAllDocumentTypesForApplication(allDocTypes, place.PlaceApplicationList);
            }
        }

        private static void GetAllDocumentTypesForApplication(DocumentTypeList allDocTypes, ApplicationList applicationList)
        {
            List<long> ids;
            foreach (Application app in applicationList.Items)
            {
                ids = new List<long>();
                app.ApplicationDocumentTypes = new DocumentTypeList();
                foreach (DocumentType dt in allDocTypes.Items)
                {
                    if (dt.DocumentTypeInstitutionId == app.ApplicationInstId &&
                        dt.DocumentTypePlaceId == app.ApplicationPlaceId &&
                        dt.DocumentTypeApplicationId == app.ApplicationId &&
                        !ids.Contains(dt.DocumentTypeId))
                    {
                        app.ApplicationDocumentTypes.Add(dt);
                        ids.Add(dt.DocumentTypeId);
                    }
                }
            }
        }

        private static void AddGlobalNodeToApplications(InstitutionList instList)
        {
            foreach (Institution inst in instList.Items)
            {
                foreach (Place place in inst.InstitutionPlaceList.Items)
                {
                    Application newApp = new Application();
                    newApp.ApplicationInstId = inst.InstitutionId;
                    newApp.ApplicationPlaceId = place.PlaceId;
                    newApp.ApplicationId = -1;
                    newApp.ApplicationCode = "GLOBAL";
                    newApp.ApplicationAcronym = "GLOBAL";
                    newApp.ApplicationDescription = "Todas";
                    newApp.ApplicationDocumentTypes = new DocumentTypeList();
                    foreach (Application app in place.PlaceApplicationList.Items)
                    {
                        newApp.ApplicationDocumentTypes.AddRange(app.ApplicationDocumentTypes);
                    }
                    place.PlaceApplicationList.Add(newApp);
                }
            }
        }

        private static void AddGlobalNodeToPlaces(InstitutionList instList)
        {
            foreach (Institution inst in instList.Items)
            {
                Place newPlace = new Place();
                newPlace.PlaceInstitutionId = inst.InstitutionId;
                newPlace.PlaceId = -1;
                newPlace.PlaceCode = "GLOBAL";
                newPlace.PlaceAcronym = "GLOBAL";
                newPlace.PlaceDescription = "Todos";
                newPlace.PlaceApplicationList = new ApplicationList();
                List<long> appIds = new List<long>();
                foreach (Place place in inst.InstitutionPlaceList.Items)
                {
                    foreach (Application app in place.PlaceApplicationList.Items)
                    {
                        if (!appIds.Contains(app.ApplicationId))
                        {
                            newPlace.PlaceApplicationList.Add(app);
                            appIds.Add(app.ApplicationId);
                        }
                        else
                        {
                            TransferDocumentTypes(newPlace.PlaceApplicationList, app);
                        }
                    }
                }
                inst.InstitutionPlaceList.Add(newPlace);
            }
        }

        private static void TransferDocumentTypes(ApplicationList appList, Application app)
        {
            foreach (Application appIt in appList.Items)
            {
                if (appIt.ApplicationId == app.ApplicationId)
                {
                    foreach (DocumentType dt in app.ApplicationDocumentTypes.Items)
                    {
                        if (!DocTypeAlreadyExists(appIt.ApplicationDocumentTypes, dt))
                        {
                            appIt.ApplicationDocumentTypes.Add(dt);
                        }
                    }
                }
            }
        }

        private static bool DocTypeAlreadyExists(DocumentTypeList dtList, DocumentType dt)
        {
            foreach (DocumentType dtIt in dtList.Items)
            {
                if (dtIt.DocumentTypeApplicationId == dt.DocumentTypeApplicationId &&
                    dtIt.DocumentTypeId == dt.DocumentTypeId)
                {
                    dtIt.DocumentTypeChilds.AddRange(dt.DocumentTypeChilds);
                    DocumentWrapper temp = new DocumentWrapper(dt.DocumentTypeInstitutionId, dt.DocumentTypePlaceId, dt.DocumentTypeApplicationId, dt.DocumentTypeId, dt.DocumentTypeDescription);
                    if (!DocTypeIdAlreadyExists(dtIt.DocumentTypeIds, temp))
                    {
                        dtIt.DocumentTypeIds.Add(temp);
                    }
                    return true;
                }
            }
            return false;
        }

        private static bool DocTypeIdAlreadyExists(List<DocumentWrapper> dwList, DocumentWrapper dw)
        {
            foreach (DocumentWrapper dwIt in dwList)
            {
                if (dwIt.documentInst == dw.documentInst &&
                    dwIt.documentPlace == dw.documentPlace &&
                    dwIt.documentApp == dw.documentApp &&
                    dwIt.documentId == dw.documentId)
                {
                    return true;
                }
            }
            return false;
        }

        private static void AddGlobalNodeToInstitutions(InstitutionList instList)
        {
            bool globalNodeInserted = false;
            Institution newInst = new Institution();
            newInst.InstitutionId = -1;
            newInst.InstitutionCode = "GLOBAL";
            newInst.InstitutionAcronym = "GLOBAL";
            newInst.InstitutionDesc = "Todas";
            newInst.InstitutionPlaceList = new PlaceList();
            foreach (Institution inst in instList.Items)
            {
                foreach (Place place in inst.InstitutionPlaceList.Items)
                {
                    if (place.PlaceId != -1)
                    {
                        newInst.InstitutionPlaceList.Add(place);
                    }
                    else
                    {
                        if (!globalNodeInserted)
                        {
                            newInst.InstitutionPlaceList.Add(place);
                            globalNodeInserted = true;
                            place.PlaceInstitutionId = -1;
                        }
                        else
                        {
                            TransferApplications(inst.InstitutionPlaceList, place);
                        }
                    }
                }
            }
            instList.Add(newInst);
        }

        private static void TransferApplications(PlaceList placeList, Place place)
        {
            foreach (Place placeIt in placeList.Items)
            {
                if (placeIt.PlaceInstitutionId == place.PlaceInstitutionId && placeIt.PlaceId == place.PlaceId)
                {
                    List<long> appIds = new List<long>();
                    foreach (Application app in placeIt.PlaceApplicationList.Items)
                    {
                        appIds.Add(app.ApplicationId);
                    }
                    foreach (Application app in place.PlaceApplicationList.Items)
                    {
                        if (!appIds.Contains(app.ApplicationId))
                        {
                            placeIt.PlaceApplicationList.Add(app);
                        }
                        else
                        {
                            TransferDocumentTypes(placeIt.PlaceApplicationList, app);
                        }
                    }
                }
            }
        }

 //       public static PlaceList GetDocTypesForFilters(string companyDb, long institutionId)
 //       {
 //           PlaceList finalPlaces = new PlaceList();
 //           return finalPlaces;
 //       }

        public static InstitutionList GetDocTypesTreeForFilters(string companyDb, string username)
        {
            return GetInstPlaceAppDocType(companyDb, "N", "N", "N", username, true, "Filters");
        }

        public static InstitutionList GetDocTypesTreeForSearchBar(string companyDB, string globalFilters, string docsSessionFilters, string servsSessionFilters, string userName, bool anaResAccess, string location)
        {
            InstitutionList myInstList = new InstitutionList();
            myInstList = GetInstPlaceAppDocType(companyDB, globalFilters, docsSessionFilters, servsSessionFilters, userName, anaResAccess, location);

            // ordenar alfabeticamente cada um dos níveis (os nós globais serão os primeiros das listas)
            foreach (Institution inst in myInstList.Items)
            {
                if (inst.InstitutionId == -1)
                {
                    inst.InstitutionDesc = "";
                }
                foreach (Place place in inst.InstitutionPlaceList.Items)
                {
                    if (place.PlaceId == -1)
                    {
                        place.PlaceDescription = "";
                    }
                    foreach (Application app in place.PlaceApplicationList.Items)
                    {
                        if (app.ApplicationId == -1)
                        {
                            app.ApplicationDescription = "";
                        }
                        if (!AlreadyHasGhostNode(app.ApplicationDocumentTypes))
                        {
                            DocumentType dt = new DocumentType();
                            dt.DocumentTypeInstitutionId = inst.InstitutionId;
                            dt.DocumentTypeApplicationId = app.ApplicationId;
                            dt.DocumentTypePlaceId = place.PlaceId;
                            dt.DocumentTypeId = -1;
                            dt.DocumentTypeCode = "GLOBAL";
                            dt.DocumentTypeAcronym = "GLOBAL";
                            dt.DocumentTypeDescription = "";
                            dt.DocumentTypeArch = "N";
                            dt.DocumentTypeLinks = "N";
                            dt.DocumentTypeOrigin = "N";
                            dt.DocumentTypeChilds = new DocumentTypeList();
                            app.ApplicationDocumentTypes.Items.Add(dt);
                            app.ApplicationDocumentTypes.Sort(new DocumentTypeComparer());
                        }
                    }
                }
            }
            return myInstList;
        }

        public static InstitutionList GetInstitutionsForExternalAccess(string companyDB, string entIds, long? epiTypeId, string epiId, long? instId, long? placeId, long? appId, long? docTypeId, string docRef, string elemType)
        {
            InstitutionList myInstList = new InstitutionList();
            myInstList = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllInstitutions(companyDB);

            GetAllPlacesForInstitutions(companyDB, myInstList);

            return myInstList;
        }

        private static void GetAllPlacesForInstitutions(string companyDB, InstitutionList myInstList)
        {
            PlaceList myPlaceList = new PlaceList();
            myPlaceList = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllPlaces(companyDB);

            GetAllApplicationsForPlaces(companyDB, myPlaceList);

            if (myInstList.Count != 0 && myPlaceList.Count != 0)
            {
                foreach (Place a in myPlaceList.Items)
                {
                    Institution i = myInstList.Items.Where(f => f.InstitutionId == a.PlaceInstitutionId).FirstOrDefault();
                    if (i != null)
                        i.InstitutionPlaceList.Add(a);
                }
            }
        }

        private static void GetAllApplicationsForPlaces(string companyDB, PlaceList myPlaceList)
        {
            ApplicationList appList = new ApplicationList();
            appList = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllApplications(companyDB);

            if (appList.Count != 0 && myPlaceList.Count != 0)
            {
                foreach (Application a in appList.Items)
                {
                    Place p = myPlaceList.Items.Where(f => f.PlaceId == a.ApplicationPlaceId && f.PlaceInstitutionId == a.ApplicationInstId).FirstOrDefault();
                    if (p != null)
                        p.PlaceApplicationList.Add(a);
                }
            }
        }

 //       public static InstitutionList GetDocTypesTreeForExternalAccess(string companyDB, string entIds, long? epiTypeId, string epiId, long? instId, long? placeId, long? appId, long? docTypeId, string docRef, string elemType)
 //       {
 //           InstitutionList myInstList = new InstitutionList();
 //           myInstList = GetInstPlaceAppDocTypeForExternalAccess(companyDB, entIds, epiTypeId, epiId, instId, placeId, appId, docTypeId, docRef, elemType);

 //           // ordenar alfabeticamente cada um dos níveis (os nós globais serão os primeiros das listas)
 //           foreach (Institution inst in myInstList.Items)
 //           {
 //               if (inst.InstitutionId == -1)
 //               {
 //                   inst.InstitutionDesc = " ";
 //               }
 //               foreach (Place place in inst.InstitutionPlaceList.Items)
 //               {
 //                   if (place.PlaceId == -1)
 //                   {
 //                       place.PlaceDescription = " ";
 //                   }
 //                   foreach (Application app in place.PlaceApplicationList.Items)
 //                   {
 //                       if (app.ApplicationId == -1)
 //                       {
 //                           app.ApplicationDescription = " ";
 //                       }
 //                       if (!AlreadyHasGhostNode(app.ApplicationDocumentTypes))
 //                       {
 //                           DocumentType dt = new DocumentType();
 //                           dt.DocumentTypeInstitutionId = inst.InstitutionId;
 //                           dt.DocumentTypeApplicationId = app.ApplicationId;
 //                           dt.DocumentTypePlaceId = place.PlaceId;
 //                           dt.DocumentTypeId = -1;
 //                           dt.DocumentTypeCode = "GLOBAL";
 //                           dt.DocumentTypeAcronym = "GLOBAL";
 //                           dt.DocumentTypeDescription = " ";
 //                           dt.DocumentTypeArch = "N";
 //                           dt.DocumentTypeLinks = "N";
 //                           dt.DocumentTypeOrigin = "N";
 //                           dt.DocumentTypeChilds = new DocumentTypeList();
 //                           app.ApplicationDocumentTypes.Items.Add(dt);
 //                           app.ApplicationDocumentTypes.Sort(new DocumentTypeComparer());
 //                       }
 //                   }
 //               }
 //           }
 //           return myInstList;
 //       }

 //       public static InstitutionList GetInstPlaceAppDocTypeForExternalAccess(string companyDB, string entIds, long? epiTypeId, string epiId, long? instId, long? placeId, long? appId, long? docTypeId, string docRef, string elemType)
 //       {
 //           // obter todos os tipos de documentos válidos para o ambiente de execução definido
 //           DocumentTypeList allDocTypes = new DocumentTypeList();
 //           allDocTypes = Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentManagementBER.Instance.GetDocTypesForExternalAccess(companyDB, entIds, epiTypeId, epiId, instId, placeId, appId, docTypeId, docRef, elemType);

 //           // obter todas as definições de hieraquias dos tipos de documentos
 //           DocHierarchyList allHierarchy = new DocHierarchyList();
 //           allHierarchy = Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentManagementBER.Instance.GetHierarchyForExternalAccess(companyDB, entIds, epiTypeId, epiId, instId, placeId, appId, docTypeId, docRef, elemType);

 //           // adicionar a própria identificação à lista de ids que representa
 //           foreach (DocumentType dt in allDocTypes.Items)
 //           {
 //               dt.DocumentTypeIds = new List<DocumentWrapper>();
 //               dt.DocumentTypeIds.Add(new DocumentWrapper(dt.DocumentTypeInstitutionId, dt.DocumentTypePlaceId, dt.DocumentTypeApplicationId, dt.DocumentTypeId, dt.DocumentTypeDescription));
 //           }

 //           // determinar tipos de documento filhos e pais
 //           DocumentTypeList childDocTypes = new DocumentTypeList();
 //           childDocTypes = TransferChilds(allDocTypes, allHierarchy);
 //           allDocTypes = DeleteChilds(allDocTypes, childDocTypes);

 //           // construir sub-hierarquia de tipos de documentos
 //           foreach (DocumentType doc in allDocTypes.Items)
 //           {
 //               AddChilds(doc, childDocTypes, allHierarchy);
 //           }

 //           // identificar as várias instituições
 //           List<long> tempIds = new List<long>();
 //           InstitutionList instList = new InstitutionList();
 //           foreach (DocumentType dt in allDocTypes.Items)
 //           {
 //               if (!tempIds.Contains(dt.DocumentTypeInstitutionId))
 //               {
 //                   instList.Add(new Institution(dt.DocumentTypeInstitutionId, dt.DocumentTypeInstitutionCode, dt.DocumentTypeInstitutionAcronym, dt.DocumentTypeInstitutionDesc));
 //                   tempIds.Add(dt.DocumentTypeInstitutionId);
 //               }
 //           }

 //           // determinar os locais de cada uma das instituições
 //           GetAllPlacesForInstitutions(allDocTypes, instList);

 //           // adicionar nó global às aplicações (para ser possível apresentar todos os tipos de documento sem apresentar as aplicações)
 //           AddGlobalNodeToApplications(instList);

 //           // adicionar nó global aos locais (para ser possível apresentar todos os tipos de documento sem apresentar os locais)
 //           AddGlobalNodeToPlaces(instList);

 //           // adicionar nó global às instituições (para ser possível apresentar todos os tipos de documento sem apresentar as instituições)
 //           AddGlobalNodeToInstitutions(instList);

 //           // ordenar alfabeticamente cada um dos níveis (os nós globais serão os primeiros das listas)
 //           foreach (Institution inst in instList.Items)
 //           {
 //               foreach (Place place in inst.InstitutionPlaceList.Items)
 //               {
 //                   foreach (Application app in place.PlaceApplicationList.Items)
 //                   {
 //                       app.ApplicationDocumentTypes.Sort(new DocumentTypeComparer());
 //                   }
 //                   place.PlaceApplicationList.Sort(new ApplicationComparer());
 //               }
 //               inst.InstitutionPlaceList.Sort(new PlaceComparer());
 //           }
 //           instList.Sort(new InstitutionComparer());

 //           return instList;
 //       }

        private static bool AlreadyHasGhostNode(DocumentTypeList docTypes)
        {
            foreach (DocumentType dt in docTypes.Items)
            {
                if (dt.DocumentTypeId == -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static PatientList SimplePatientFind(string companyDb, string patType, string patId, string patNProc, string patName, string patNsns, DateTime? patBirthDate, string patSex, long? tEpisodio, string episodeId, DateTime? epiDateBegin, DateTime? epiDateEnd, string doc, string extId, long? docType, long? appId, long? localId, long? instId, DateTime? docDateBegin, DateTime? docDateEnd, DateTime? valDateBegin, DateTime? valDateEnd, string searchType, string globalFilters, string myDocsSessionFilters, string myServsSessionFilters, string userName, bool myUserAnaRes)
        {
            string docsSessionFilters;
            string servsSessionFilters;
            if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            { docsSessionFilters = "N"; }
            else
            { docsSessionFilters = "S"; }
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            { servsSessionFilters = "N"; }
            else
            { servsSessionFilters = "S"; }

            string userAnaRes = "S";
            if (!myUserAnaRes)
            { userAnaRes = "N"; }

            PatientList patients = new PatientList();
            patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.SimplePatientFind(companyDb, patNProc, patNsns, patBirthDate, patId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);

            /*
            LocalPatientList localpatients = new LocalPatientList();
            localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIds(companyDb, patNProc, patNsns, patBirthDate, patId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);
            return LinkIds(patients, localpatients);
            */

            return ProcessIds(patients);
        }

        public static PatientList SimplePatientFindV2(
            string companyDb, 
            string patType, 
            string patId, 
            string patNProc, 
            string patName, 
            string patNsns, 
            DateTime? patBirthDate, 
            string patSex, 
            long? tEpisodioId, 
            string tEpisodioCode,
            string episodeId, 
            DateTime? epiDateBegin,
            DateTime? epiDateEnd,
            string doc, 
            string extId, 
            long? docTypeId, 
            string docTypeCode,
            long? appId, 
            string appCode,
            long? localId, 
            string localCode,
            long? instId, 
            string instCode,
            DateTime? docDateBegin, 
            DateTime? docDateEnd, 
            DateTime? valDateBegin, 
            DateTime? valDateEnd, 
            string searchType, 
            string globalFilters, 
            string myDocsSessionFilters,
            string myServsSessionFilters,
            string userName, 
            bool myUserAnaRes)
        {
            string docsSessionFilters;
            string servsSessionFilters;
            if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            { docsSessionFilters = "N"; }
            else
            { docsSessionFilters = "S"; }
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            { servsSessionFilters = "N"; }
            else
            { servsSessionFilters = "S"; }

            string userAnaRes = "S";
            if (!myUserAnaRes)
            { userAnaRes = "N"; }

            PatientList patients = new PatientList();
            patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.SimplePatientFindV2(
                companyDb, 
                patNProc, 
                patNsns, 
                patBirthDate, 
                patId, 
                patType, 
                patName, 
                patSex, 
                tEpisodioId,
                tEpisodioCode,
                episodeId, 
                epiDateBegin,
                epiDateEnd, 
                doc, 
                extId, 
                docTypeId,
                docTypeCode,
                appId, 
                appCode,
                localId,
                localCode,
                instId, 
                instCode,
                docDateBegin, 
                docDateEnd, 
                valDateBegin, 
                valDateEnd, 
                searchType, 
                globalFilters, 
                docsSessionFilters, 
                servsSessionFilters, 
                userName, 
                userAnaRes);

            /*
            LocalPatientList localpatients = new LocalPatientList();
            localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIds(companyDb, patNProc, patNsns, patBirthDate, patId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);
            return LinkIds(patients, localpatients);
            */

            return ProcessIds(patients);
        }

        public static PatientList SimplePatientFindV3(
            string companyDb,
            string patType,
            string patId,
            string patNProc,
            string patName,
            string patNsns,
            DateTime? patBirthDate,
            string patSex,
            long? tEpisodioId,
            string tEpisodioCode,
            string episodeId,
            DateTime? epiDateBegin,
            DateTime? epiDateEnd,
            string doc,
            string extId,
            long? docTypeId,
            string docTypeCode,
            long? appId,
            string appCode,
            long? localId,
            string localCode,
            long? instId,
            string instCode,
            DateTime? docDateBegin,
            DateTime? docDateEnd,
            DateTime? valDateBegin,
            DateTime? valDateEnd,
            DateTime? procDateBegin,
            DateTime? procDateEnd,
            string searchType,
            string globalFilters,
            string myDocsSessionFilters,
            string myServsSessionFilters,
            string userName,
            bool myUserAnaRes)
        {
            string docsSessionFilters;
            string servsSessionFilters;
            if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            { docsSessionFilters = "N"; }
            else
            { docsSessionFilters = "S"; }
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            { servsSessionFilters = "N"; }
            else
            { servsSessionFilters = "S"; }

            string userAnaRes = "S";
            if (!myUserAnaRes)
            { userAnaRes = "N"; }

            PatientList patients = new PatientList();
            patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.SimplePatientFindV3(
                companyDb,
                patNProc,
                patNsns,
                patBirthDate,
                patId,
                patType,
                patName,
                patSex,
                tEpisodioId,
                tEpisodioCode,
                episodeId,
                epiDateBegin,
                epiDateEnd,
                doc,
                extId,
                docTypeId,
                docTypeCode,
                appId,
                appCode,
                localId,
                localCode,
                instId,
                instCode,
                docDateBegin,
                docDateEnd,
                valDateBegin,
                valDateEnd,
                procDateBegin,
                procDateEnd,
                searchType,
                globalFilters,
                docsSessionFilters,
                servsSessionFilters,
                userName,
                userAnaRes);

            /*
            LocalPatientList localpatients = new LocalPatientList();
            localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIds(companyDb, patNProc, patNsns, patBirthDate, patId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);
            return LinkIds(patients, localpatients);
            */

            return ProcessIds(patients);
        }

        private static PatientList ProcessIds(PatientList patients)
        {
            // doentes não repetidos
            PatientList selectedPats = new PatientList();
            // doentes para processamento posterior
            PatientList otherPats = new PatientList();

            // triar os diversos pacientes
            foreach (Patient pat in patients.Items)
            {
                if (!PreviouslySelectedPatient(selectedPats, pat))
                {
                    selectedPats.Items.Add(pat);
                }
                else
                {
                    otherPats.Items.Add(pat);
                }
            }

            // acrescentar identificações descartadas aos respectivos pacientes
            foreach (Patient selPat in selectedPats.Items)
            {
                foreach (Patient otherPat in otherPats.Items)
                {
                    if (selPat.PatEntityId == otherPat.PatEntityId)
                    {
                        selPat.PatLocalPatients.Items.Add(otherPat.PatLocalPatients[0]);
                    }
                }
            }

            return selectedPats;
        }

        private static bool PreviouslySelectedPatient(PatientList selectedPatients, Patient patient)
        {
            foreach (Patient pat in selectedPatients.Items)
            {
                if (pat.PatEntityId == patient.PatEntityId)
                    return true;
            }
            return false;
        }


 //       public static PatientList GenericPatientFind(string companyDb, string patType, string patPatId, string patNProc, string patName, string patNsns, DateTime? patBirthDate, string patSex, string tEpisodio, string episodeId, DateTime? epiDateBegin, DateTime? epiDateEnd, string doc, string extId, string docType, string appId, string localId, string instId, DateTime? docDateBegin, DateTime? docDateEnd, DateTime? valDateBegin, DateTime? valDateEnd, string searchType, string globalFilters, string myDocsSessionFilters, string myServsSessionFilters, string userName, bool myUserAnaRes, string patID)
 //       {
 //           string docsSessionFilters;
 //           string servsSessionFilters;
 //           if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
 //           { docsSessionFilters = "N"; }
 //           else
 //           { docsSessionFilters = "S"; }
 //           if (myServsSessionFilters == null || myServsSessionFilters == "N")
 //           { servsSessionFilters = "N"; }
 //           else
 //           { servsSessionFilters = "S"; }

 //           string userAnaRes = "S";
 //           if (!myUserAnaRes)
 //           { userAnaRes = "N"; }

 //           PatientList patients = new PatientList();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GenericPatientFind(companyDb, patNProc, patNsns, patBirthDate, patPatId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes, patID);

 //           if (patients != null && patients.Count != 0)
 //           {

 //               foreach (Patient p in patients.Items)
 //               {
 //                   VisitList visits = new VisitList();
 //                   if ((!string.IsNullOrEmpty(episodeId) || !string.IsNullOrEmpty(tEpisodio)))
 //                   {
 //                       visits.Add(new Visit() { VisitEpisode = episodeId, Episode = episodeId, EpisodeType = tEpisodio, VisitEpisodeType = new EpisodeType() { EpiTypeCode = tEpisodio, EpiTypeDescription = tEpisodio } });
 //                   }
 //                   else
 //                   {
 //                       if (p.PatLocalPatients != null)
 //                           visits = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodesBySearchDat3(companyDb, p.PatLocalPatients[0].LocpatPatientType.PattypCode, p.PatLocalPatients[0].LocpatPatientId);
 //                   }

 //                   p.Episodes = visits;
 //               }
 //           }
 //           /*LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GenericPatientIds(companyDb, patNProc, patNsns, patBirthDate, patPatId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes, patID);

 //           PatientList p = LinkIds(patients, localpatients);*/

 //           //VisitList visits = new VisitList();
 //           //visits = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodesBySearchData(companyDb, patName, patPatId, patNProc, patNsns, instId, localId, patID);

 //           //PatientList pats = LinkEpisodes(patients, visits);
 //           return patients;
 //       }



 //       public static PatientList GetPatientBySamples(string companyDb, string appId, string localId, string instId, Nullable<DateTime> minDate, Nullable<DateTime> maxDate, string reqservice, string execservice, string state, string nMecan)
 //       {
 //           PatientList patients = new PatientList();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientBySamples(companyDb, appId, localId, instId, minDate, maxDate, reqservice, execservice, state, nMecan);

 //           /*LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientBySamplesPatientIds(companyDb, appId, localId, instId, minDate, maxDate, service);

 //           return LinkIds(patients, localpatients);*/

 //           return patients;
 //       }

 //       private static PatientList LinkIds(PatientList patients, LocalPatientList localpatients)
 //       {
 //           //associa pares doente/tipo doente originais
 //           foreach (Patient pat in patients.Items)
 //           {
 //               pat.PatLocalPatients = new LocalPatientList();
 //               foreach (LocalPatient locpat in localpatients.Items)
 //               {
 //                   if (pat.PatEntity.EntId == locpat.LocpatEntityId)
 //                   {
 //                       pat.PatLocalPatients.Add(locpat);
 //                   }
 //               }
 //           }

 //           //associa pares doente/tipo doente derivados
 //           foreach (Patient pat in patients.Items)
 //           {
 //               pat.PatLocalPatients.AddRange(GetChildLocalPatients(pat.PatLocalPatients, localpatients));
 //           }
 //           return patients;
 //       }

 //       private static PatientList LinkEpisodes(PatientList patients, VisitList visits)
 //       {
 //           foreach (Patient pat in patients.Items)
 //           {
 //               pat.Episodes = new VisitList();
 //               foreach (Visit vis in visits.Items)
 //               {
 //                   if (pat.PatEntity.EntId == vis.VisitEntId)
 //                   {
 //                       pat.Episodes.Add(vis);
 //                   }
 //               }
 //           }

 //           return patients;
 //       }

 //       private static Patient LinkIds(Patient patient, LocalPatientList localpatients)
 //       {
 //           //associa pares doente/tipo doente originais
 //           patient.PatLocalPatients = new LocalPatientList();
 //           foreach (LocalPatient locpat in localpatients.Items)
 //           {
 //               if (patient.PatEntity.EntId == locpat.LocpatEntityId)
 //               {
 //                   patient.PatLocalPatients.Add(locpat);
 //               }
 //           }

 //           patient.PatLocalPatients.AddRange(GetChildLocalPatients(patient.PatLocalPatients, localpatients));
 //           return patient;
 //       }

 //       private static LocalPatientList GetChildLocalPatients(LocalPatientList original, LocalPatientList all)
 //       {
 //           LocalPatientList childs = new LocalPatientList();
 //           foreach (LocalPatient locpat in original.Items)
 //           {
 //               foreach (LocalPatient item in all.Items)
 //               {
 //                   if (locpat.LocpatEntityId != item.LocpatEntityId &&
 //                       locpat.LocpatPatientType.PattypId == item.LocpatPatientType.PattypId &&
 //                       locpat.LocpatPatientId == item.LocpatPatientId)
 //                   {
 //                       childs.Add(item);
 //                   }
 //               }
 //           }
 //           return childs;
 //       }

 //       public static PatientList GetPatientByDocument(string companyDb, string doc, long appId, long localId, long instId)
 //       {
 //           PatientList patients = new PatientList();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientByDocument(companyDb, doc, appId, localId, instId);

 //           LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIdsByDocument(companyDb, doc, appId, localId, instId);

 //           return LinkIds(patients, localpatients);
 //       }

 //       public static PatientList GetPatientByDocumentV2(string companyDb, string doc, long docTypeId, long appId, long localId, long instId)
 //       {
 //           PatientList patients = new PatientList();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientByDocumentV2(companyDb, doc, docTypeId, appId, localId, instId);

 //           LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIdsByDocumentV2(companyDb, doc, docTypeId, appId, localId, instId);

 //           return LinkIds(patients, localpatients);
 //       }

 //       /*
 //       private static PatientList FinalPatients(PatientList pats)
 //       {
 //           List<long> entIds = new List<long>();
 //           PatientList newPats = new PatientList();

 //           foreach (Patient pat in pats.Items)
 //           {
 //               if (!entIds.Contains(pat.PatEntity.EntId))
 //               {
 //                   entIds.Add(pat.PatEntity.EntId);
 //                   newPats.Add(pat);
 //               }
 //           }
 //           return newPats;
 //       }
 //       */

 //       public static ServiceList GetFavoriteServicesForDoctor(string companyName, string doctorid, string serviceid, string localid, string instid)
 //       {
 //           ServiceList slist = new ServiceList();
 //           slist = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetFavoriteServicesForDoctor(companyName, doctorid, serviceid, localid, instid);

 //           return slist;
 //       }

 //       public static GroupList GetGroupsBySpecs(string companyDB, string place, string institution, string servexec, string servreq, string nmecan, string scope)
 //       {
 //           GroupList glist = new GroupList();

 //           glist = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllGroupsBySpecs(companyDB, scope, place, institution, nmecan, servreq, servexec);

 //           GroupList temp = new GroupList();

 //           if (glist.Count != 0)
 //           {
 //               /*GroupExamList gel = GetGroupExamsBySpecs(companyDB, localId.Value, instId.Value, servexecId, servreqId, docEntId, scope);

 //               if (gel.Count != 0)
 //                   foreach (Group g in glist.Items)
 //                   {
 //                       g.GroupExams = new GroupExamList();
 //                       foreach (GroupExam ge in gel.Items)
 //                       {
 //                           if (ge.GroupExamId == g.GroupId)
 //                           {
 //                               g.GroupExams.Add(ge);
 //                           }
 //                       }
 //                   }*/

 //               temp = GetGroupHierarchy(glist, null, null);
 //           }

 //           return temp;
 //       }

 //       private static GroupList GetGroupHierarchy(GroupList glist, Group parent, Group parentParent)
 //       {
 //           long parentEmployeeId = 0;

 //           if (parent != null)
 //               parentEmployeeId = parent.GroupId;

 //           GroupList childEmployees = new GroupList();

 //           foreach (Group g in glist.Items)
 //           {
 //               if ((g.GroupParentId != 0 && g.GroupParentId == parentEmployeeId) || (parentEmployeeId == 0 && g.GroupParentId == 0))

 //                   childEmployees.Add(g);
 //           }

 //           GroupList hierarchy = new GroupList();

 //           if (childEmployees.Items.Count != 0)
 //           {
 //               foreach (Group emp in childEmployees.Items)
 //               {
 //                   /*                    Group aux = new Group();
 //                                       aux.EntId = emp.EntId;
 //                                       aux.GroupCode = emp.GroupCode;
 //                                       aux.GroupDescription = emp.GroupDescription;
 //                                       aux.GroupId = emp.GroupId;
 //                                       aux.GroupParent = parentParent;
 //                                       aux.GroupParentId = parentParent != null ? parentParent.GroupParentId : 0;*/

 //                   //emp.GroupParent = parentParent;
 //                   emp.GroupChilds = GetGroupHierarchy(glist, emp, emp);
 //                   hierarchy.Add(emp);
 //                   //hierarchy.Add(new EmployeeHierarchy() { Employee = emp, Employees = GetEmployeesHierachy(allEmployees, emp) });
 //               }
 //           }

 //           return hierarchy;
 //       }

 //       public static GroupExamList GetGroupExamsBySpecs(string companyDB, long localId, long instId, long? servexecId, long? servreqId, long? docEntId, string scope)
 //       {
 //           GroupExamList glist = new GroupExamList();
 //           glist = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetGroupExamsBySpecs(companyDB, instId, localId, scope, servexecId, servreqId, docEntId);

 //           return glist;
 //       }

 //       public static ServiceList GetServicesForDoctor(string CompanyDb, string doctorId, string localId, string instId, string reqServiceId, string mode)
 //       {
 //           ServiceList slist = new ServiceList();
 //           if (mode == "D")
 //           {
 //               slist = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServicesForDoctor(CompanyDb, doctorId, reqServiceId, localId, instId);
 //           }
 //           else if (mode == "S")
 //           {
 //               slist = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetServicesForSampleCol(CompanyDb, doctorId, reqServiceId, localId, instId);
 //           }
 //           return slist;
 //       }


 //       public static Patient GetPatientById(string companyDb, string entid)
 //       {
 //           Patient patients = new Patient();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientById(companyDb, entid);

 //           LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientByIdIds(companyDb, entid);

 //           return LinkIds(patients, localpatients);
 //       }

 //       public static Visit GetPatientEpisodeByIds(string companyDb, string episode, long episodeType)
 //       {
 //           Visit visit = new Visit();

 //           visit = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodeByIds(companyDb, episodeType, episode);
 //           return visit;
 //       }

 //       public static ErEntity GetEntityInSession(string companyDb, string session, string section)
 //       {
 //           ErEntity entity = new ErEntity();

 //           entity = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetEntityBySession(companyDb, session, section);
 //           if (entity == null || entity.EntId == 0)
 //               return null;

 //           return entity;
 //       }

 //       public static ErEntity GetOrInsertEntity(string CompanyDb, string Name, string NMecan, string ReqService, string Type, long InstId, long PlaceId, long AppId)
 //       {
 //           ErEntity entity = new ErEntity();

 //           Type = Type == "M" ? "MED" :
 //                  Type == "E" ? "ENF" :
 //                  Type == "T" ? "TEC" :
 //                           "ADM";

 //           entity = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetOrInsertEntity(CompanyDb, Name, NMecan, ReqService, Type, InstId, PlaceId, AppId);

 //           return entity;
 //       }

 //       public static Visit GetOrInsertEpisode(string CompanyDb, string Episode, string EpisodeType, string PatId, string PatType, long InstId, long PlaceId, long AppId)
 //       {
 //           Visit visit = new Visit();
 //           visit = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetOrInsertEpisode(CompanyDb, Episode, EpisodeType, PatId, PatType, InstId, PlaceId, AppId);

 //           return visit;
 //       }


 //       public static Patient GetOrInsertPatient(string CompanyDb, string PatientId, string PatientType, long InstId, long PlaceId, long AppId)
 //       {
 //           Patient patient = new Patient();
 //           patient = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetOrInsertPatient(CompanyDb, PatientId, PatientType, InstId, PlaceId, AppId);

 //           return patient;

 //       }

 //       public static VisitList GetPatientEpisodesByGenericPatient(string companyDb, string tDoente, long doente)
 //       {
 //           VisitList list =
 //               Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientEpisodesByGenericPatient(companyDb, tDoente, doente);
 //           return list;
 //       }

 //       public static VisitList GetPatientEpisodes(string company, string patient, long local, long inst)
 //       {
 //           Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList docList = new Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList();

 //           docList = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodes(company, inst, local, patient);

 //           return docList;
 //       }

 //       public static VisitList GetPatientEpisodesBySearchData(string company, string pname, string pnumber, string pprocess, string psns, string inst, string place, string patID)
 //       {
 //           Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList docList = new Cpchs.Eresults.Common.WCF.BusinessEntities.VisitList();

 //           docList = Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodesBySearchData(company, pname, pnumber, pprocess, psns, inst, place, patID);

 //           return docList;
 //       }




 //       public static PatientList GetPatientByRequest(string companyDb, long? requisition, long? element)
 //       {
 //           PatientList patients = new PatientList();
 //           patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientByRequest(companyDb, requisition, element);

 //           LocalPatientList localpatients = new LocalPatientList();
 //           localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIdsByRequest(companyDb, requisition, element);

 //           return LinkIds(patients, localpatients);
 //       }

 //       public static TerapeuticList GetTerapeutics(string p)
 //       {
 //           return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetAllTerapeutics(p);
 //       }

 //       public static GroupExamList GetGroupExamsByGroup(string companyDb, long groupId, string inst, string place)
 //       {
 //           return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetGroupExamsByGroup(companyDb, groupId, inst, place);
 //       }

 //       public static GroupExamList GetExamsBySearchCriteria(string companyDb, string instId, string placeId, string key, string docEntId, string reqServId, string execServId, long? pageNumber, long? itemsPerPage, string orderField, string orderType)
 //       {
 //           return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetExamsBySearchCriteria(companyDb, instId, placeId, key, docEntId, reqServId,execServId, pageNumber, itemsPerPage, orderField, orderType);
 //       }

        public static ExternalPatientList GetPatientsFromExternalProvider(string companyDb, string patName, string patId, string patProcessNum, string patSNS, DateTime? patBirthDate)
        {
            return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientsFromExtProvider(companyDb, patId, patName, patBirthDate, patSNS, patProcessNum);
        }

        public static ExternalEpisodeList GetPatientEpisodesFromExternalProvider(string companyDb, string pattype, string patid, string userId)
        {
            return Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeManagementBER.Instance.GetPatientEpisodesFromExternalProvider(companyDb, pattype, patid, userId);
        }

        public static bool? CheckUserSettings(string companyDB, string username, string application, string module)
        {
            return Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.CheckUserSettings(companyDB, username, application, module);
        }
        
        public static EpisodeType GetEpisodeTypeByCode(string companyDb, string code)
        {
            return EpisodeManagementBER.Instance.GetEpisodeTypeByCode(companyDb, code);
        }

        public static PatientList GetPatientsBySpecs(
            string companyDb,
            string search,
            string globalFilters,
         //   string myDocsSessionFilters,
            string myServsSessionFilters,
            string userName)
        {
        //    string docsSessionFilters;
            string servsSessionFilters;
         /*   if (myDocsSessionFilters == null || myDocsSessionFilters == "N")
            { docsSessionFilters = "N"; }
            else
            { docsSessionFilters = "S"; }*/
            if (myServsSessionFilters == null || myServsSessionFilters == "N")
            { servsSessionFilters = "N"; }
            else
            { servsSessionFilters = "S"; }

          /*  string userAnaRes = "S";
            if (!myUserAnaRes)
            { userAnaRes = "N"; }*/

            PatientList patients = new PatientList();
            patients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientsBySpecs(
                companyDb,
                search,
                globalFilters,
               // docsSessionFilters,
                servsSessionFilters,
                userName);

            /*
            LocalPatientList localpatients = new LocalPatientList();
            localpatients = Cpchs.Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetPatientIds(companyDb, patNProc, patNsns, patBirthDate, patId, patType, patName, patSex, tEpisodio, episodeId, epiDateBegin, epiDateEnd, doc, extId, docType, appId, localId, instId, docDateBegin, docDateEnd, valDateBegin, valDateEnd, searchType, globalFilters, docsSessionFilters, servsSessionFilters, userName, userAnaRes);
            return LinkIds(patients, localpatients);
            */
            return patients;
          //  return ProcessIds(patients);
        }
    }
}

