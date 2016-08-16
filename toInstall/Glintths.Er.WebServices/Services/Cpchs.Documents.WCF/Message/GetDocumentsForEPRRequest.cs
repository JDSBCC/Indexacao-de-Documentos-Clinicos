
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
	/// <summary>
    /// Service Contract Class - GetDocumentsForEPRRequest
	/// </summary>
    [WCF::MessageContract(WrapperName = "GetDocumentsForEPRRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class GetDocumentsForEPRRequest
	{
		private string companyDb;
        private string patientId;
        private string patientType;
	 	private string episodeType;
	 	private string episodeId;
	 	private Cpchs.Entities.WCF.DataContracts.PaginationRequest paginationInfo;
        private string period;
        private string eResultsVersion;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PatientId")]
        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PatientType")]
        public string PatientType
        {
            get { return patientType; }
            set { patientType = value; }
        }
			
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EpisodeType")]
		public string EpisodeType
		{
			get { return episodeType; }
			set { episodeType = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EpisodeId")]
		public string EpisodeId
		{
			get { return episodeId; }
			set { episodeId = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PaginationInfo")]
		public Cpchs.Entities.WCF.DataContracts.PaginationRequest PaginationInfo
		{
			get { return paginationInfo; }
			set { paginationInfo = value; }
		}

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Period")]
        public string Period
        {
            get { return period; }
            set { period = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EResultsVersion")]
        public string EResultsVersion
        {
            get { return eResultsVersion; }
            set { eResultsVersion = value; }
        }
	}
}
