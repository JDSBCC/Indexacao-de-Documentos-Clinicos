using Cpchs.ER2Indexer.WCF.BusinessEntities.Generated;
using System;
using System.Data;
	
namespace Cpchs.ER2Indexer.WCF.BusinessEntities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class DocumentInfo : DocumentInfo_GEN
	{
        #region Variables

        private object documentInfoPartialStream;

        #endregion

        #region Properties

        public object DocumentInfoPartialStream
        {
            get { return documentInfoPartialStream; }
            set { documentInfoPartialStream = value; }
        }

        public string DocumentInfoInstCod { get; set; }
        public string DocumentInfoPlaceCod { get; set; }
        public string DocumentInfoAppCod { get; set; }
        public string DocumentInfoDocTypeCod { get; set; }

        public string ElementoId { get; set; }
        public string CodVersao { get; set; }


        public string FormResponse { get; set; }
        public string FormFilter { get; set; }

        public string DocumentInfoInstCodConv { get; set; }
        public string DocumentInfoPlaceCodConv { get; set; }
        public string DocumentInfoAppCodConv { get; set; }
        public string DocumentInfoDocTypeCodConv { get; set; }


        #endregion

        public DocumentInfo()
        {
        }
		
        public DocumentInfo(
            IDataReader reader, 
            string companyDB) 
            : base(
            reader, 
            companyDB)
        {
            LoadFromReader(
                reader);
        }

        public  DocumentInfo(object documentInfoXml, long documentInfoInstId, long documentInfoPlaceId, long documentInfoAppId, long documentInfoDocTypeId, string documentInfoDocRef, Nullable<DateTime> documentInfoDocDate, string documentInfoProcState, long documentInfoDocId) : base(documentInfoXml, documentInfoInstId, documentInfoPlaceId, documentInfoAppId, documentInfoDocTypeId, documentInfoDocRef, documentInfoDocDate, documentInfoProcState, documentInfoDocId)
        {
        }
		
        public DocumentInfo(decimal documentInfoXmlId) : base(documentInfoXmlId)
        {
			
        }

        private void LoadFromReader(IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                    {
                        case "DOCUMENTINFOINSTCOD":
                            if (!reader.IsDBNull(i)) this.DocumentInfoInstCod = reader.GetString(i);
                            break;
                        case "DOCUMENTINFOPLACECOD":
                            if (!reader.IsDBNull(i)) this.DocumentInfoPlaceCod = reader.GetString(i);
                            break;
                        case "DOCUMENTINFOAPPCOD":
                            if (!reader.IsDBNull(i)) this.DocumentInfoAppCod = reader.GetString(i);
                            break;
                        case "DOCUMENTINFODOCTYPECOD":
                            if (!reader.IsDBNull(i)) this.DocumentInfoDocTypeCod = reader.GetString(i);
                            break;
                        case "ELEMENTOID":
                            if (!reader.IsDBNull(i)) this.ElementoId = reader.GetValue(i).ToString();
                            break;
                        case "CODVERSAO":
                            if (!reader.IsDBNull(i)) this.CodVersao = reader.GetValue(i).ToString();
                            break;

                        case "DTCRI":
                            if (!reader.IsDBNull(i)) this.DtCri = reader.GetDateTime(i);
                            break;
                        case "DOCUMENTINFOINSTCODCONV":
                            if (!reader.IsDBNull(i)) this.DocumentInfoInstCodConv = reader.GetValue(i).ToString();
                            break;
                        case "DOCUMENTINFOPLACECODCONV":
                            if (!reader.IsDBNull(i)) this.DocumentInfoPlaceCodConv = reader.GetValue(i).ToString();
                            break;
                        case "DOCUMENTINFOAPPCODCONV":
                            if (!reader.IsDBNull(i)) this.DocumentInfoAppCodConv = reader.GetValue(i).ToString();
                            break;
                        case "DOCUMENTINFODOCTYPECODCONV":
                            if (!reader.IsDBNull(i)) this.DocumentInfoDocTypeCodConv = reader.GetValue(i).ToString();
                            break;
                    }
                }
            }
        }
	}
}