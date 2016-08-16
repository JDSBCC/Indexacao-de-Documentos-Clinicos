using Cpchs.Documents.WCF.DataContracts;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentLogBeAndDocumentLogDc
    {
        public static Eresults.Common.WCF.BusinessEntities.DocumentLog TranslateDocumentLogToDocumentLog(DocumentLog from)
        {
            Eresults.Common.WCF.BusinessEntities.DocumentLog to = new Eresults.Common.WCF.BusinessEntities.DocumentLog
                                                                      {
                                                                          DocLogPatTypeId = from.PatTypeId,
                                                                          DocLogPatId = from.PatId,
                                                                          DocLogInstId = from.InstId,
                                                                          DocLogPlaceId = from.PlaceId,
                                                                          DocLogAppId = from.AppId,
                                                                          DocLogDocTypeId = from.DocTypeId,
                                                                          DocLogDoc = from.Doc,
                                                                          DocLogDataDoc = from.DocDate,
                                                                          DocLogArrivDate = from.ArrivDate,
                                                                          DocLogProcDate = from.ProcDate,
                                                                          DocLogStatusId = from.StatusId,
                                                                          DocLogDocId = from.DocId,
                                                                          DocLogXmlId = from.XmlId
                                                                      };
            return to;
        }

        public static DocumentLog TranslateDocumentLogToDocumentLog(Eresults.Common.WCF.BusinessEntities.DocumentLog from)
        {
            DocumentLog to = new DocumentLog
                                 {
                                     AppDesc = from.docLogAppDesc,
                                     AppId = from.DocLogAppId,
                                     ArrivDate = from.DocLogArrivDate,
                                     Doc = from.DocLogDoc,
                                     DocDate = from.DocLogDataDoc,
                                     DocId = from.DocLogDocId,
                                     DocTypeDesc = from.docLogDocTypeDesc,
                                     DocTypeId = from.DocLogDocTypeId,
                                     InstDesc = from.docLogInstDesc,
                                     InstId = from.DocLogInstId,
                                     PatId = from.DocLogPatId,
                                     PatTypeDesc = from.docLogPatTypeDesc,
                                     PatTypeId = from.DocLogPatTypeId,
                                     PlaceDesc = from.docLogPlaceDesc,
                                     PlaceId = from.DocLogPlaceId,
                                     ProcDate = from.DocLogProcDate,
                                     StatusDesc = from.docLogStatusDesc,
                                     StatusId = from.DocLogStatusId,
                                     XmlId = from.DocLogXmlId,
                                     DocLogId = from.DocLogId,
                                     IsProcessable = from.docLogIsProcessable == "S",
                                     IsIgnorable = from.docLogIsIgnorable == "S",
                                     DocumentEpisodeLogs = new DocumentEpisodeLogs(),
                                     DocumentIndexLogs = new DocumentIndexLogs()
                                 };
            foreach (Eresults.Common.WCF.BusinessEntities.DocumentEpisodeLog docEpiLog in from.DocLogEpisodeLog.Items)
            {
                to.DocumentEpisodeLogs.Add(TranslateBetweenDocumentEpisodeLogBeAndDocumentEpisodeLogDc.TranslateDocumentEpisodeLogToDocumentEpisodeLog(docEpiLog));
            }
            foreach (Eresults.Common.WCF.BusinessEntities.DocumentIndexLog docIdxLog in from.DocLogIndexLog.Items)
            {
                to.DocumentIndexLogs.Add(TranslateBetweenDocumentIndexLogBeAndDocumentIndexLogDc.TranslateDocumentIndexLogToDocumentIndexLog(docIdxLog));
            }
            return to;
        }
    }
}