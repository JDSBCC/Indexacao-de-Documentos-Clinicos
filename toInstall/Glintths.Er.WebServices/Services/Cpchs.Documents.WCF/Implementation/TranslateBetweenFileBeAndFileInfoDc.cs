namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenFileBeAndFileInfoDc
    {
        public static Eresults.Common.WCF.BusinessEntities.File TranslateFileInfoToFile(DataContracts.FileInfo from)
        {
            Eresults.Common.WCF.BusinessEntities.File to = new Eresults.Common.WCF.BusinessEntities.File
                                                               {FileOriginalName = from.FileExtension};
            to.ElementDescription = from.ElementDescription;
            to.FileElemId = from.FileElemId;
            to.DocumentUniqueId = from.FileDocumentId;
            return to;
        }

        public static DataContracts.FileInfo TranslateFileToFileInfo(Eresults.Common.WCF.BusinessEntities.File from)
        {
            DataContracts.FileInfo to = new DataContracts.FileInfo();
            if (from != null)
            {
                if (from.FileOriginalName != null)
                {
                    to.FileExtension = from.FileOriginalName.Contains(".") ? from.FileOriginalName.Substring(from.FileOriginalName.LastIndexOf('.'), from.FileOriginalName.Length - from.FileOriginalName.LastIndexOf('.')) : ".qqc";
                }
                else
                {
                    to.FileExtension = from.FileOriginalName;
                }
            }

            to.ElementTitle = from.ElementTitle;
            to.ElementDescription = from.ElementDescription;
            to.FileElemId = from.FileElemId;
            to.FileDocumentId = from.DocumentUniqueId;
            return to;
        }
    }
}