namespace Cpchs.ER2Indexer.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentInfoBeAndDocumentInfoDc
    {
        public static BusinessEntities.DocumentInfo TranslateDocumentInfoToDocumentInfo(DataContracts.DocumentInfo from)
        {
            BusinessEntities.DocumentInfo to = new BusinessEntities.DocumentInfo {DocumentInfoXml = @from.IndexingInfo};
            foreach (DataContracts.FileStreamInfo fsi in from.FilesStreams)
            {
                to.FilesStreams.Items.Add(TranslateBetweenFileStreamInfoBeAndFileStreamInfoDc.TranslateFileStreamInfoToFileStreamInfo(fsi));
            }

            to.FormResponse = from.FormResponse;
            to.FormFilter = from.Filter;
            return to;
        }

        public static DataContracts.DocumentInfo TranslateDocumentInfoToDocumentInfo(BusinessEntities.DocumentInfo from)
        {
            DataContracts.DocumentInfo to = new DataContracts.DocumentInfo {IndexingInfo = @from.DocumentInfoXml};
            foreach (BusinessEntities.FileStreamInfo fsi in from.FilesStreams.Items)
            {
                to.FilesStreams.Add(TranslateBetweenFileStreamInfoBeAndFileStreamInfoDc.TranslateFileStreamInfoToFileStreamInfo(fsi));
            }

            to.FormResponse = from.FormResponse;
            to.Filter = from.FormFilter;
            return to;
        }
    }
}