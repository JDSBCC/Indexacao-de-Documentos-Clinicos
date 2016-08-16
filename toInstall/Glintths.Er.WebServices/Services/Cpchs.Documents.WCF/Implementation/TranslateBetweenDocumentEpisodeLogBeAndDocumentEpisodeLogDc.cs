namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentEpisodeLogBeAndDocumentEpisodeLogDc
    {
        public static Eresults.Common.WCF.BusinessEntities.DocumentEpisodeLog TranslateDocumentEpisodeLogToDocumentEpisodeLog(Cpchs.Documents.WCF.DataContracts.DocumentEpisodeLog from)
        {
            Eresults.Common.WCF.BusinessEntities.DocumentEpisodeLog to =
                new Eresults.Common.WCF.BusinessEntities.DocumentEpisodeLog
                    {
                        DocEpiLogEpiTypeId = from.EpiTypeId,
                        DocEpiLogEpiId = from.EpiId,
                        DocEpiLogInstId = from.InstId,
                        DocEpiLogPlaceId = from.PlaceId
                    };
            return to;
        }

        public static DataContracts.DocumentEpisodeLog TranslateDocumentEpisodeLogToDocumentEpisodeLog(Eresults.Common.WCF.BusinessEntities.DocumentEpisodeLog from)
        {
            DataContracts.DocumentEpisodeLog to = new DataContracts.DocumentEpisodeLog
                                                      {
                                                          EpiId = from.DocEpiLogEpiId,
                                                          EpiTypeDesc = from.docEpiLogEpiTypeDesc,
                                                          EpiTypeId = from.DocEpiLogEpiTypeId,
                                                          InstDesc = from.docEpiLogInstDesc,
                                                          InstId = from.DocEpiLogInstId,
                                                          PlaceDesc = from.docEpiLogPlaceDesc,
                                                          PlaceId = from.DocEpiLogPlaceId
                                                      };
            return to;
        }
    }
}

