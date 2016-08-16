using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using CPCHS.Common.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;
using System.Collections.Generic;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: sexta-feira, 26 de Dezembro de 2008
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class DocumentType : DocumentType_GEN
	{
        public long   DocumentTypeInstitutionId { get; set; }
        public string DocumentTypeInstitutionCode { get; set; }
        public string DocumentTypeInstitutionAcronym { get; set; }
        public string DocumentTypeInstitutionDesc { get; set; }

        public long   DocumentTypePlaceId { get; set; }
        public string DocumentTypePlaceCode { get; set; }
        public string DocumentTypePlaceAcronym { get; set; }
        public string DocumentTypePlaceDesc { get; set; }

        public string DocumentTypeApplicationCode { get; set; }
        public string DocumentTypeApplicationAcronym { get; set; }
        public string DocumentTypeApplicationDesc { get; set; }
        public bool HasForm { get; set; }
        public bool MandatoryForm { get; set; }
        public string FormDescription { get; set; }
        

        public List<DocumentWrapper> DocumentTypeIds { get; set; }
		
        /// <summary>
        /// Initialize an new empty DocumentType object.
        /// </summary>
        public DocumentType() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty DocumentType object.
        /// </summary>
        public DocumentType(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                {
                    case "DOCUMENTTYPEINSTITUTIONID":
                        if (!reader.IsDBNull(i)) this.DocumentTypeInstitutionId = reader.GetInt64(i);
                        break;
                    case "DOCUMENTTYPEINSTITUTIONCODE":
                        if (!reader.IsDBNull(i)) this.DocumentTypeInstitutionCode = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEINSTITUTIONACRONYM":
                        if (!reader.IsDBNull(i)) this.DocumentTypeInstitutionAcronym = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEINSTITUTIONDESC":
                        if (!reader.IsDBNull(i)) this.DocumentTypeInstitutionDesc = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEPLACEID":
                        if (!reader.IsDBNull(i)) this.DocumentTypePlaceId = reader.GetInt64(i);
                        break;
                    case "DOCUMENTTYPEPLACECODE":
                        if (!reader.IsDBNull(i)) this.DocumentTypePlaceCode = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEPLACEACRONYM":
                        if (!reader.IsDBNull(i)) this.DocumentTypePlaceAcronym = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEPLACEDESC":
                        if (!reader.IsDBNull(i)) this.DocumentTypePlaceDesc = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEAPPLICATIONCODE":
                        if (!reader.IsDBNull(i)) this.DocumentTypeApplicationCode = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEAPPLICATIONACRONYM":
                        if (!reader.IsDBNull(i)) this.DocumentTypeApplicationAcronym = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEAPPLICATIONDESC":
                        if (!reader.IsDBNull(i)) this.DocumentTypeApplicationDesc = reader.GetString(i);
                        break;
                    case "DOCUMENTTYPEHASFORM":
                        if (!reader.IsDBNull(i)) this.HasForm = reader.GetString(i) == "S";
                        break;
                    case "DOCUMENTTYPEMANDATORY":
                        if (!reader.IsDBNull(i)) this.MandatoryForm = reader.GetString(i) == "S";
                        break;
                    case "DOCUMENTFORMDESCRIPTION":
                        if (!reader.IsDBNull(i)) this.FormDescription = reader.GetString(i);
                        break;
                }
            }
        }
		
        /// <summary>
        /// Initialize a new  DocumentType object with the given parameters.
        /// </summary>
        public DocumentType(long documentTypeApplicationId, long documentTypeId, string documentTypeCode, string documentTypeAcronym, string documentTypeDescription, object documentTypeLogo, string documentTypeScope, long documentTypeEqGroup, string documentTypeXmlIdx, string documentTypeFileIdx, string documentTypeErrorIdx, string documentTypeArch, string documentTypeOrigin, string documentTypeLinks, bool documentTypeHasForm, bool documentTypeIsMandatory, string documentTypeFormDescription)
            : base(documentTypeApplicationId, documentTypeId, documentTypeCode, documentTypeAcronym, documentTypeDescription, documentTypeLogo, documentTypeScope, documentTypeEqGroup, documentTypeXmlIdx, documentTypeFileIdx, documentTypeErrorIdx, documentTypeArch, documentTypeOrigin, documentTypeLinks)
        {
            HasForm = documentTypeHasForm;
            MandatoryForm = documentTypeIsMandatory;
            FormDescription = documentTypeFormDescription;
        }

        public DocumentType(long documentTypeApplicationId, long documentTypeId)
            : base(documentTypeApplicationId, documentTypeId)
        {

        }
	}
}

