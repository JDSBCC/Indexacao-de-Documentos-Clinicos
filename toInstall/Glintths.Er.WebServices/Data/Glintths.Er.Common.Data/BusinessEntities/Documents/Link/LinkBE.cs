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
    /// Date Created: quarta-feira, 14 de Outubro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class Link : Link_GEN
	{

        public string InstatiatedArgs { get; set; }
        public List<string> ExternalParams { get; set; }
        public byte[] Thumb { get; set; }
        /// <summary>
        /// Initialize an new empty Link object.
        /// </summary>
        public Link() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty Link object.
        /// </summary>
        public Link(IDataReader reader, string companyDB) : base(reader, companyDB)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.LinkType linkType = new LinkType(reader, companyDB);
            this.LinkTypeBE = linkType;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                {
                    case "ELEMENTTHUMB":
                        if (!reader.IsDBNull(i)) this.Thumb = (byte[])reader.GetValue(i);
                        break;
                }
            }
        }
		
        /// <summary>
        /// Initialize a new  Link object with the given parameters.
        /// </summary>
        public  Link(long linkElemId, long linkVersionCode, long linkTypeId, string linkDesc) : base(linkElemId, linkVersionCode, linkTypeId, linkDesc)
        {
        }
		
        public Link(long linkElemId, long linkVersionCode) : base(linkElemId, linkVersionCode)
        {
			
        }
	}
}

