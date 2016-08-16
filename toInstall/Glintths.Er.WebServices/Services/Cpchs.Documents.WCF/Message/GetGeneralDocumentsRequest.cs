﻿using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
    /// <summary>
    /// Service Contract Class - GetDocumentsByMultiCriteriaV2Request
    /// </summary>
    [WCF::MessageContract(WrapperName = "GetGeneralDocumentsRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class GetGeneralDocumentsRequest
    {
        private string companyDb;
        private string entitiesIds;
        private string patientId;
        private string patientType;
        private string episodeType;
        private string episodeId;
        private System.Nullable<System.DateTime> episodeStartDate;
        private System.Nullable<System.DateTime> episodeEndDate;
        private string institution;
        private string place;
        private string application;
        private string documentType;
        private string documentId;
        private System.Nullable<System.DateTime> executionStartDate;
        private System.Nullable<System.DateTime> executionEndDate;
        private System.Nullable<System.DateTime> validationStartDate;
        private System.Nullable<System.DateTime> validationEndDate;
        private System.Nullable<System.DateTime> emissionStartDate;
        private System.Nullable<System.DateTime> emissionEndDate;
        private System.Nullable<System.DateTime> cancelDate;
        private string externalId;
        private string reqService;
        private string execService;
        private string globalFilters;
        private string docsSessionFilters;
        private string servsSessionFilters;
        private string username;
        private Cpchs.Entities.WCF.DataContracts.PaginationRequest paginationInfo;
        private System.Nullable<bool> report;
        private System.Nullable<bool> publicField;
        private System.Nullable<long> workspaceId;
        private string filterString;
        private string period;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CompanyDb")]
        public string CompanyDb
        {
            get { return companyDb; }
            set { companyDb = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EntitiesIds")]
        public string EntitiesIds
        {
            get { return entitiesIds; }
            set { entitiesIds = value; }
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

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EpisodeStartDate")]
        public System.Nullable<System.DateTime> EpisodeStartDate
        {
            get { return episodeStartDate; }
            set { episodeStartDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EpisodeEndDate")]
        public System.Nullable<System.DateTime> EpisodeEndDate
        {
            get { return episodeEndDate; }
            set { episodeEndDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Institution")]
        public string Institution
        {
            get { return institution; }
            set { institution = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Place")]
        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Application")]
        public string Application
        {
            get { return application; }
            set { application = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocumentType")]
        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocumentId")]
        public string DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ExecutionStartDate")]
        public System.Nullable<System.DateTime> ExecutionStartDate
        {
            get { return executionStartDate; }
            set { executionStartDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ExecutionEndDate")]
        public System.Nullable<System.DateTime> ExecutionEndDate
        {
            get { return executionEndDate; }
            set { executionEndDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ValidationStartDate")]
        public System.Nullable<System.DateTime> ValidationStartDate
        {
            get { return validationStartDate; }
            set { validationStartDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ValidationEndDate")]
        public System.Nullable<System.DateTime> ValidationEndDate
        {
            get { return validationEndDate; }
            set { validationEndDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EmissionStartDate")]
        public System.Nullable<System.DateTime> EmissionStartDate
        {
            get { return emissionStartDate; }
            set { emissionStartDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "EmissionEndDate")]
        public System.Nullable<System.DateTime> EmissionEndDate
        {
            get { return emissionEndDate; }
            set { emissionEndDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CancelDate")]
        public System.Nullable<System.DateTime> CancelDate
        {
            get { return cancelDate; }
            set { cancelDate = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ExternalId")]
        public string ExternalId
        {
            get { return externalId; }
            set { externalId = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ReqService")]
        public string ReqService
        {
            get { return reqService; }
            set { reqService = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ExecService")]
        public string ExecService
        {
            get { return execService; }
            set { execService = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "GlobalFilters")]
        public string GlobalFilters
        {
            get { return globalFilters; }
            set { globalFilters = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocsSessionFilters")]
        public string DocsSessionFilters
        {
            get { return docsSessionFilters; }
            set { docsSessionFilters = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "ServsSessionFilters")]
        public string ServsSessionFilters
        {
            get { return servsSessionFilters; }
            set { servsSessionFilters = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Username")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PaginationInfo")]
        public Cpchs.Entities.WCF.DataContracts.PaginationRequest PaginationInfo
        {
            get { return paginationInfo; }
            set { paginationInfo = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Report")]
        public System.Nullable<bool> Report
        {
            get { return report; }
            set { report = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Public")]
        public System.Nullable<bool> Public
        {
            get { return publicField; }
            set { publicField = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "WorkspaceId")]
        public System.Nullable<long> WorkspaceId
        {
            get { return workspaceId; }
            set { workspaceId = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "FilterString")]
        public string FilterString
        {
            get { return filterString; }
            set { filterString = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Period")]
        public string Period
        {
            get { return period; }
            set { period = value; }
        }
    }
}

