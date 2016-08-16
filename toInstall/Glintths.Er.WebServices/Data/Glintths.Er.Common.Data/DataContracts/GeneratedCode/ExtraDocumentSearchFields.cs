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

namespace Glintths.Er.Common.DataContracts
{
	/// <summary>
	/// Data Contract Class - ExtraDocumentSearchFields
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "ExtraDocumentSearchFields")]
	public partial class ExtraDocumentSearchFields 
	{
		private string id;
		private string patient;
		private string patientType;
		private string episode;
		private string episodeType;
		private string docType;
		private string element;
		private Glintths.Er.Common.DataContracts.SimpleReqIds reqList;
		private string actMed;
		private string service;
		private string templateName;
		private string email;
		private string rubric;
		private Glintths.Er.Common.DataContracts.SimpleReqIds episodeChildList;
		private string requisition;
		private bool isToMerge;
		private bool selectedForPrint;
		private bool selectedForPreview;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = false, Order = 0)]
		public string Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Patient", IsRequired = false, Order = 1)]
		public string Patient
		{
		  get { return patient; }
		  set { patient = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatientType", IsRequired = false, Order = 2)]
		public string PatientType
		{
		  get { return patientType; }
		  set { patientType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Episode", IsRequired = false, Order = 3)]
		public string Episode
		{
		  get { return episode; }
		  set { episode = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "EpisodeType", IsRequired = false, Order = 4)]
		public string EpisodeType
		{
		  get { return episodeType; }
		  set { episodeType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocType", IsRequired = false, Order = 5)]
		public string DocType
		{
		  get { return docType; }
		  set { docType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Element", IsRequired = false, Order = 6)]
		public string Element
		{
		  get { return element; }
		  set { element = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ReqList", IsRequired = false, Order = 7)]
		public Glintths.Er.Common.DataContracts.SimpleReqIds ReqList
		{
		  get { return reqList; }
		  set { reqList = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ActMed", IsRequired = false, Order = 8)]
		public string ActMed
		{
		  get { return actMed; }
		  set { actMed = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Service", IsRequired = false, Order = 9)]
		public string Service
		{
		  get { return service; }
		  set { service = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TemplateName", IsRequired = false, Order = 10)]
		public string TemplateName
		{
		  get { return templateName; }
		  set { templateName = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Email", IsRequired = false, Order = 11)]
		public string Email
		{
		  get { return email; }
		  set { email = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Rubric", IsRequired = false, Order = 12)]
		public string Rubric
		{
		  get { return rubric; }
		  set { rubric = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "EpisodeChildList", IsRequired = false, Order = 13)]
		public Glintths.Er.Common.DataContracts.SimpleReqIds EpisodeChildList
		{
		  get { return episodeChildList; }
		  set { episodeChildList = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Requisition", IsRequired = false, Order = 14)]
		public string Requisition
		{
		  get { return requisition; }
		  set { requisition = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IsToMerge", IsRequired = false, Order = 15)]
		public bool IsToMerge
		{
		  get { return isToMerge; }
		  set { isToMerge = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SelectedForPrint", IsRequired = false, Order = 16)]
		public bool SelectedForPrint
		{
		  get { return selectedForPrint; }
		  set { selectedForPrint = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SelectedForPreview", IsRequired = false, Order = 17)]
		public bool SelectedForPreview
		{
		  get { return selectedForPreview; }
		  set { selectedForPreview = value; }
		}				
	}
}

