using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public class FileLogic
    {
        public static FileList GetDocumentFiles(string companyDb, long docId)
        {
            return FileManagementBER.Instance.GetDocumentFiles(companyDb, docId);
        }

        public static File GetFileByElementid(string companyDb, long elementId)
        {
            return FileManagementBER.Instance.GetFileByElementId(companyDb, elementId);
        }
    }
}