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
	/// Data Contract Class - ExtraDocument
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "ExtraDocument")]
	public partial class ExtraDocument 
	{
		private string id;
		private string requisition;
		private string typeDescription;
		private string element;
		private string description;
		private string typeCode;
		private string episodeType;
		private string service;
		private string medicalActType;
		private string rubric;
		private string desc;
		private string templateName;
		private string numCopies;
		private string descrAbrev;
		private string episode;
		private string patient;
		private string patientType;
		private string email;
		private bool selectedForPrint;
		private bool selectedForPreview;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = false, Order = 0)]
		public string Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Requisition", IsRequired = false, Order = 1)]
		public string Requisition
		{
		  get { return requisition; }
		  set { requisition = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TypeDescription", IsRequired = false, Order = 2)]
		public string TypeDescription
		{
		  get { return typeDescription; }
		  set { typeDescription = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Element", IsRequired = false, Order = 3)]
		public string Element
		{
		  get { return element; }
		  set { element = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 4)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TypeCode", IsRequired = false, Order = 5)]
		public string TypeCode
		{
		  get { return typeCode; }
		  set { typeCode = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "EpisodeType", IsRequired = false, Order = 6)]
		public string EpisodeType
		{
		  get { return episodeType; }
		  set { episodeType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Service", IsRequired = false, Order = 7)]
		public string Service
		{
		  get { return service; }
		  set { service = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MedicalActType", IsRequired = false, Order = 8)]
		public string MedicalActType
		{
		  get { return medicalActType; }
		  set { medicalActType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Rubric", IsRequired = false, Order = 9)]
		public string Rubric
		{
		  get { return rubric; }
		  set { rubric = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Desc", IsRequired = false, Order = 10)]
		public string Desc
		{
		  get { return desc; }
		  set { desc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "TemplateName", IsRequired = false, Order = 11)]
		public string TemplateName
		{
		  get { return templateName; }
		  set { templateName = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "NumCopies", IsRequired = false, Order = 12)]
		public string NumCopies
		{
		  get { return numCopies; }
		  set { numCopies = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DescrAbrev", IsRequired = false, Order = 13)]
		public string DescrAbrev
		{
		  get { return descrAbrev; }
		  set { descrAbrev = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Episode", IsRequired = false, Order = 14)]
		public string Episode
		{
		  get { return episode; }
		  set { episode = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Patient", IsRequired = false, Order = 15)]
		public string Patient
		{
		  get { return patient; }
		  set { patient = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatientType", IsRequired = false, Order = 16)]
		public string PatientType
		{
		  get { return patientType; }
		  set { patientType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Email", IsRequired = false, Order = 17)]
		public string Email
		{
		  get { return email; }
		  set { email = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SelectedForPrint", IsRequired = false, Order = 18)]
		public bool SelectedForPrint
		{
		  get { return selectedForPrint; }
		  set { selectedForPrint = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SelectedForPreview", IsRequired = false, Order = 19)]
		public bool SelectedForPreview
		{
		  get { return selectedForPreview; }
		  set { selectedForPreview = value; }
		}				
	}
}
