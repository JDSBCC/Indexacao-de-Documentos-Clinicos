using System.Collections.Generic;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    class TranslateBetweenDocumentBeAndDocumentDc
    {
        public static Eresults.Common.WCF.BusinessEntities.Document TranslateDocumentToDocument(DataContracts.Document from)
        {
            Eresults.Common.WCF.BusinessEntities.Document to = new Eresults.Common.WCF.BusinessEntities.Document
                                                                   {DocumentId = @from.UniqueId};
            return to;
        }
        
        private static Dictionary<string, bool> cachedMergeLinkElementsConfigs = new Dictionary<string, bool>();
        public static bool MergeLinkElementsConfig(string companyDb)
        {
            if(cachedMergeLinkElementsConfigs.ContainsKey(companyDb))
                return cachedMergeLinkElementsConfigs[companyDb];

            Eresults.Common.WCF.BusinessEntities.ERConfigurationList confs = Eresults.Common.WCF.BusinessEntities.EntityManagementBER.Instance.GetConfigurationsByKey(companyDb, "MERGELINKELEMENTS");
            if (confs != null && confs.Count > 0)
            {
                bool value = string.Equals(confs[0].ErConfigValue, "S");
                cachedMergeLinkElementsConfigs.Add(companyDb, value);
                return value;
            }
            return false;
        }

        public static DataContracts.Document TranslateDocumentToDocument(string companyDb,Eresults.Common.WCF.BusinessEntities.Document from)
        {
            bool mergeLinks = MergeLinkElementsConfig(companyDb);
            
            DataContracts.Document to = new DataContracts.Document
                                            {
                                                IntitutionCode = from.InstitutionCode,
                                                PlaceCode = from.PlaceCode,
                                                Application = from.AppName,
                                                ApplicationCode = from.ApplicationCode,
                                                DocumentTypeId = from.DocumentType,
                                                ApplicationId = from.DocumentApp,
                                                Childs = new DataContracts.Documents()
                                            };
            foreach (Eresults.Common.WCF.BusinessEntities.Document doc in from.DocumentChilds.Items)
            {
                to.Childs.Add(TranslateDocumentToDocument(companyDb, doc));
            }
            to.PatientName = from.PatientName;
            to.Viewed = from.Viewed;

            to.DateCancel = from.DateCancel;
            to.JustifCancel = from.JustifCancel;
            to.JustifIdCancel = from.JustifIdCancel;
            to.JustifCancelValor = from.JustifCancelValor;
            to.UserCancel = from.UserCancel;
            to.ObsCancel = from.ObsCancel;

            to.Description = from.DocumentDescription;
            to.DocumentType = from.DocTypeName;
            to.DocumentTypeCode = from.DocumentTypeCode;
            to.HasForm = from.DocumentHasForm;
            to.MandatoryForm = from.DocumentMandatoryForm;
            to.FormDescription = from.DocumentFormDescription;
            to.Elements = new DataContracts.Elements();
            if(from.DocumentElements.Items.Count > 0 && from.ChildElemType != "exam")
            {
                foreach (Element elem in @from.DocumentElements.Items.TakeWhile(elem => !mergeLinks || !string.Equals(from.ChildElemType, "link") || to.Elements.Count <= 0).Where(elem => elem.ElementId > 0))
                {
                    to.Elements.Add(TranslateBetweenElementBeAndElementDc.TranslateElementToElement(elem));
                }
            }
            switch (from.ChildElemType)
            {
                case "exam":
                    to.ElementType = DataContracts.ElementType.AnalyticalResult;
                    break;
                case "file":
                    to.ElementType = DataContracts.ElementType.File;
                    break;
                case "link":
                    to.ElementType = DataContracts.ElementType.Link;
                    break;
                case "video":
                    to.ElementType = DataContracts.ElementType.Video;
                    break;
                case "mix":
                    to.ElementType = DataContracts.ElementType.Mix;
                    break;
                default:
                    to.ElementType = DataContracts.ElementType.Unknown;
                    break;
            }
            to.EpisodeType = from.EpisodeType;
            to.ExecService = from.ServiceExec;
            to.ThumbUrlQuery = from.ThumbUrlQuery;
            to.ThumbClass = from.ThumbClass;
            to.Id = from.DocumentRef;
            to.ReqService = from.ServiceReq;
            to.UniqueId = from.DocumentId;
            if (from.DocumentDate != null) to.ExecutionDate = from.DocumentDate.Value;
            if (from.DocumentMaxValDate != null) to.ValidationDate = from.DocumentMaxValDate.Value;
            to.EmissionDate = from.DocumentEmissionDate;
            to.Public = from.DocumentPublic == "S";
            to.PatientId = from.PatientId;
            to.PatientType = from.PatientType;
            to.EpisodeId = from.EpisodeId;
            to.EpisodeTypeCode = from.EpisodeTypeCode;

            //cancelation verification
            to.DocumentTypeCode = from.DocumentStatus;

            return to;
        }

        public static DataContracts.Document TranslateDocumentToDocumentForEPR(string companyDb, Eresults.Common.WCF.BusinessEntities.Document from)
        {
            DataContracts.Document to = new DataContracts.Document
            {
                IntitutionCode = from.InstitutionCode,
                PlaceCode = from.PlaceCode,
                Application = from.AppName,
                ApplicationCode = from.ApplicationCode,
                Childs = new DataContracts.Documents()
            };
            foreach (Eresults.Common.WCF.BusinessEntities.Document doc in from.DocumentChilds.Items)
            {
                to.Childs.Add(TranslateDocumentToDocument(companyDb, doc));
            }
            to.Description = from.DocumentDescription;
            to.DocumentType = from.DocTypeName;
            to.DocumentTypeCode = from.DocumentTypeCode;
            to.Elements = new DataContracts.Elements();
            if (from.DocumentElements.Items.Count > 0 && from.ChildElemType != "exam")
            {
                foreach (Element elem in @from.DocumentElements.Items.TakeWhile(elem => !string.Equals(from.ChildElemType, "link") || to.Elements.Count <= 0).Where(elem => elem.ElementId > 0))
                {
                    to.Elements.Add(TranslateBetweenElementBeAndElementDc.TranslateElementToElement(elem));
                }
            }
            switch (from.ChildElemType)
            {
                case "exam":
                    to.ElementType = DataContracts.ElementType.AnalyticalResult;
                    break;
                case "file":
                    to.ElementType = DataContracts.ElementType.File;
                    break;
                case "link":
                    to.ElementType = DataContracts.ElementType.Link;
                    break;
                case "video":
                    to.ElementType = DataContracts.ElementType.Video;
                    break;
                case "mix":
                    to.ElementType = DataContracts.ElementType.Mix;
                    break;
                default:
                    to.ElementType = DataContracts.ElementType.Unknown;
                    break;
            }
            to.EpisodeType = from.EpisodeType;
            to.ExecService = from.ServiceExec;
            to.ThumbUrlQuery = from.ThumbUrlQuery;
            to.ThumbClass = from.ThumbClass;
            to.Id = from.DocumentRef;
            to.ReqService = from.ServiceReq;
            to.UniqueId = from.DocumentId;
            to.DocumentURL = from.DocumentURL;
            to.DocumentURLType = from.DocumentURLType;
            if (from.DocumentDate != null) to.ExecutionDate = from.DocumentDate.Value;
            if (from.DocumentMaxValDate != null) to.ValidationDate = from.DocumentMaxValDate.Value;
            to.EmissionDate = from.DocumentEmissionDate;
            to.Public = from.DocumentPublic == "S";
            return to;
        }


        public static List<Document> TranslateDocumentInfoForPublish(DataContracts.DocumentInfoForPublishList from)
        {
            List<Document> to = new List<Document>();
            if (from != null && from.Documents != null)
            {
                to.AddRange(from.Documents.Select(item => new Document
                                                              {
                                                                   DocumentId = item.DocumentUniqueId, DocumentPublic = item.Publish ? "S" : "N"
                                                               }));
            }
            return to;
        }
    }
}