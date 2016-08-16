//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Entities.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - DocumentType
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "DocumentType")]
	public partial class DocumentType 
	{
		private long id;
		private string code;
		private string acronym;
		private string description;
		private long institutionId;
		private long placeId;
		private long applicationId;
		private string archive;
		private string analyticalResult;
		private string link;
		private DocumentTypeCollection childs;
        private bool hasForm;
        private string formDescription;
        private bool mandatoryForm;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = false, Order = 0)]
		public long Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Code", IsRequired = false, Order = 1)]
		public string Code
		{
		  get { return code; }
		  set { code = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Acronym", IsRequired = false, Order = 2)]
		public string Acronym
		{
		  get { return acronym; }
		  set { acronym = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 3)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "InstitutionId", IsRequired = false, Order = 4)]
		public long InstitutionId
		{
		  get { return institutionId; }
		  set { institutionId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PlaceId", IsRequired = false, Order = 5)]
		public long PlaceId
		{
		  get { return placeId; }
		  set { placeId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ApplicationId", IsRequired = false, Order = 6)]
		public long ApplicationId
		{
		  get { return applicationId; }
		  set { applicationId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Archive", IsRequired = false, Order = 7)]
		public string Archive
		{
		  get { return archive; }
		  set { archive = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "AnalyticalResult", IsRequired = false, Order = 8)]
		public string AnalyticalResult
		{
		  get { return analyticalResult; }
		  set { analyticalResult = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Link", IsRequired = false, Order = 9)]
		public string Link
		{
		  get { return link; }
		  set { link = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Childs", IsRequired = false, Order = 10)]
		public DocumentTypeCollection Childs
		{
		  get { return childs; }
		  set { childs = value; }
		}

        [WcfSerialization::DataMember(Name = "HasForm", IsRequired = false, Order = 11)]
        public bool HasForm
        {
            get { return hasForm; }
            set { hasForm = value; }
        }

        [WcfSerialization::DataMember(Name = "FormDescription", IsRequired = false, Order = 12)]
        public string FormDescription
        {
            get { return formDescription; }
            set { formDescription = value; }
        }

        [WcfSerialization::DataMember(Name = "MandatoryForm", IsRequired = false, Order = 13)]
        public bool MandatoryForm
        {
            get { return mandatoryForm; }
            set { mandatoryForm = value; }
        }

            

	}
}
