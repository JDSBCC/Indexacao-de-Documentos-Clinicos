namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentIndexLogBeAndDocumentIndexLogDc
    {
        public static Eresults.Common.WCF.BusinessEntities.DocumentIndexLog TranslateDocumentIndexLogToDocumentIndexLog(DataContracts.DocumentIndexLog from)
        {
            Eresults.Common.WCF.BusinessEntities.DocumentIndexLog to =
                new Eresults.Common.WCF.BusinessEntities.DocumentIndexLog
                    {
                        DocIdxLogLevel = from.Level,
                        DocIdxLogMessage = from.Message,
                        DocIdxLogProcedure = from.Procedure,
                        DocIdxLogException = from.Exception,
                        DocIdxLogRegDate = from.RegDate,
                        DocIdxLogDetail = from.Detail
                    };
            return to;
        }

        public static DataContracts.DocumentIndexLog TranslateDocumentIndexLogToDocumentIndexLog(Eresults.Common.WCF.BusinessEntities.DocumentIndexLog from)
        {
            DataContracts.DocumentIndexLog to = new DataContracts.DocumentIndexLog
                                                    {
                                                        Exception = from.DocIdxLogException,
                                                        Level = from.DocIdxLogLevel,
                                                        Message = from.DocIdxLogMessage,
                                                        Procedure = from.DocIdxLogProcedure,
                                                        RegDate = from.DocIdxLogRegDate,
                                                        Detail = from.DocIdxLogDetail
                                                    };
            return to;
        }
    }
}