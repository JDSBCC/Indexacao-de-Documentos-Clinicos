using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using CPCHS.Common.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: quarta-feira, 29 de Outubro de 2008
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class File : File_GEN
	{

        public long DocumentUniqueId { get; set; }
        public byte[] Thumb { get; set; }
        public string ElementTitle { get; set; }
        public string ElementDescription { get; set; }
        /// <summary>
        /// Initialize an new empty File object.
        /// </summary>
        public File() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty File object.
        /// </summary>
        public File(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                {
                    case "DOCUMENTUNIQUEID":
                        if (!reader.IsDBNull(i)) this.DocumentUniqueId = reader.GetInt64(i);
                        break;
                    case "FILETHUMB":
                        if (!reader.IsDBNull(i)) this.Thumb = (byte[])reader.GetValue(i);
                        break;
                    case "ELEMDESCRIPTION":
                        if (!reader.IsDBNull(i)) this.ElementDescription = Convert.ToString(reader.GetValue(i));
                        break;
                    case "ELEMTITLE":
                        if (!reader.IsDBNull(i)) this.ElementTitle = Convert.ToString(reader.GetValue(i));
                        break;
                }
            }
        }
		
        /// <summary>
        /// Initialize a new  File object with the given parameters.
        /// </summary>
        public  File(long fileElemId, long fileVersionCode, string filePath, string fileThumbPath, string fileOriginalName, string fileEncrypted, string fileXmlInfo, object fileStream, object fileThumbStream) : base(fileElemId, fileVersionCode, filePath, fileThumbPath, fileOriginalName, fileEncrypted, fileXmlInfo, fileStream, fileThumbStream)
        {
        }
		
        public File(long fileElemId, long fileVersionCode) : base(fileElemId, fileVersionCode)
        {
			
        }
	}
}


