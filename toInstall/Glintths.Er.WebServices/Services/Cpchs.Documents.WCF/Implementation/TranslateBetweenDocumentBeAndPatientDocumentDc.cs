using System.Globalization;
using Cpchs.Documents.WCF.DataContracts;
using System.Collections.Generic;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentBeAndPatientDocumentDc
    {
        public static Eresults.Common.WCF.BusinessEntities.Document TranslatePatientDocumentToDocument(PatientDocument from)
        {
            Eresults.Common.WCF.BusinessEntities.Document to = new Eresults.Common.WCF.BusinessEntities.Document
                                                                   {
                                                                       DocumentId = long.Parse(from.DocId),
                                                                       ChildElemType = from.DocElemType
                                                                   };
            return to;
        }

        public static PatientDocument TranslateDocumentToPatientDocument(Eresults.Common.WCF.BusinessEntities.Document from)
        {
            PatientDocument to = new PatientDocument
                                     {DocId = from.DocumentId.ToString(CultureInfo.InvariantCulture), DocElemType = from.ChildElemType};
            if (from.DocumentDate != null)
            {
                Dictionary<string, string> dicInfo = new Dictionary<string, string>
                                                         {
                                                             {"Id. Doc.", from.DocumentRef},
                                                             {"T. Episódio", from.EpisodeType},
                                                             {
                                                                 "Data", from.DocumentDate.Value.Day.ToString("D2") + "-" +
                                                                         from.DocumentDate.Value.Month.ToString("D2") + "-" +
                                                                         from.DocumentDate.Value.Year.ToString("D4") + " " +
                                                                         from.DocumentDate.Value.Hour.ToString("D2") + ":" +
                                                                         from.DocumentDate.Value.Minute.ToString("D2") +
                                                                         ":" +
                                                                         from.DocumentDate.Value.Second.ToString("D2")
                                                                 }
                                                         };
                if (from.DocumentMaxValDate.HasValue)
                {
                    dicInfo.Add("Data Val.", from.DocumentMaxValDate.Value.Day.ToString("D2") + "-" +
                                             from.DocumentMaxValDate.Value.Month.ToString("D2") + "-" +
                                             from.DocumentMaxValDate.Value.Year.ToString("D4") + " " +
                                             from.DocumentMaxValDate.Value.Hour.ToString("D2") + ":" +
                                             from.DocumentMaxValDate.Value.Minute.ToString("D2") + ":" +
                                             from.DocumentMaxValDate.Value.Second.ToString("D2"));
                }
                else
                {
                    dicInfo.Add("Data Val.", "");
                }

                dicInfo.Add("Serv. Requisit.", from.ServiceReq);
                dicInfo.Add("Serv. Execut.", from.ServiceExec);
                dicInfo.Add("", from.DocumentDescription);
                dicInfo.Add("Aplicação", from.AppName);
                dicInfo.Add("T. Doc.", from.DocTypeName);
                dicInfo.Add("InstId", from.DocumentInst.ToString(CultureInfo.InvariantCulture));
                dicInfo.Add("PlaceId", from.DocumentLocal.ToString(CultureInfo.InvariantCulture));
                dicInfo.Add("AppId", from.DocumentApp.ToString(CultureInfo.InvariantCulture));
                dicInfo.Add("DocTypeId", from.DocTypeId.ToString(CultureInfo.InvariantCulture));
                to.DocInfo = dicInfo;
            }
            to.DocChilds = new PatientDocuments();
            foreach (Eresults.Common.WCF.BusinessEntities.Document doc in from.DocumentChilds.Items)
            {
                to.DocChilds.Add(TranslateDocumentToPatientDocument(doc));
            }
            return to;
        }
    }
}