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
	/// Data Contract Class - Patient
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "Patient")]
	public partial class Patient 
	{
		private string entityIds;
		private string name;
		private string socialNum;
		private string address;
		private string city;
		private string postalCode;
		private string phone1;
		private string phone2;
		private string fax;
		private string processNum;
		private string snsNum;
		private string benefNum;
		private string idCardNum;
		private System.Nullable<System.DateTime> birthDate;
		private string healthCentre;
		private Gender gender;
		private MaritalStatus maritalStatus;
		private LocalPatientCollection localPatients;
		private string sampleState;
		private int presentationOrder;
		private PatientEpisodeCollection patientEpisodes;
		private string presentationNSC;
		private string presentationPatient;
		private string presentationNProc;
		
		[WcfSerialization::DataMember(Name = "EntityIds", IsRequired = false, Order = 0)]
		public string EntityIds
		{
		  get { return entityIds; }
		  set { entityIds = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Name", IsRequired = false, Order = 1)]
		public string Name
		{
		  get { return name; }
		  set { name = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SocialNum", IsRequired = false, Order = 2)]
		public string SocialNum
		{
		  get { return socialNum; }
		  set { socialNum = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Address", IsRequired = false, Order = 3)]
		public string Address
		{
		  get { return address; }
		  set { address = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "City", IsRequired = false, Order = 4)]
		public string City
		{
		  get { return city; }
		  set { city = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PostalCode", IsRequired = false, Order = 5)]
		public string PostalCode
		{
		  get { return postalCode; }
		  set { postalCode = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Phone1", IsRequired = false, Order = 6)]
		public string Phone1
		{
		  get { return phone1; }
		  set { phone1 = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Phone2", IsRequired = false, Order = 7)]
		public string Phone2
		{
		  get { return phone2; }
		  set { phone2 = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Fax", IsRequired = false, Order = 8)]
		public string Fax
		{
		  get { return fax; }
		  set { fax = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ProcessNum", IsRequired = false, Order = 9)]
		public string ProcessNum
		{
		  get { return processNum; }
		  set { processNum = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SnsNum", IsRequired = false, Order = 10)]
		public string SnsNum
		{
		  get { return snsNum; }
		  set { snsNum = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "BenefNum", IsRequired = false, Order = 11)]
		public string BenefNum
		{
		  get { return benefNum; }
		  set { benefNum = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IdCardNum", IsRequired = false, Order = 12)]
		public string IdCardNum
		{
		  get { return idCardNum; }
		  set { idCardNum = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "BirthDate", IsRequired = false, Order = 13)]
		public System.Nullable<System.DateTime> BirthDate
		{
		  get { return birthDate; }
		  set { birthDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "HealthCentre", IsRequired = false, Order = 14)]
		public string HealthCentre
		{
		  get { return healthCentre; }
		  set { healthCentre = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Gender", IsRequired = false, Order = 15)]
		public Gender Gender
		{
		  get { return gender; }
		  set { gender = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MaritalStatus", IsRequired = false, Order = 16)]
		public MaritalStatus MaritalStatus
		{
		  get { return maritalStatus; }
		  set { maritalStatus = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "LocalPatients", IsRequired = false, Order = 17)]
		public LocalPatientCollection LocalPatients
		{
		  get { return localPatients; }
		  set { localPatients = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "SampleState", IsRequired = false, Order = 18)]
		public string SampleState
		{
		  get { return sampleState; }
		  set { sampleState = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PresentationOrder", IsRequired = false, Order = 19)]
		public int PresentationOrder
		{
		  get { return presentationOrder; }
		  set { presentationOrder = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatientEpisodes", IsRequired = false, Order = 20)]
		public PatientEpisodeCollection PatientEpisodes
		{
		  get { return patientEpisodes; }
		  set { patientEpisodes = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PresentationNSC", IsRequired = false, Order = 21)]
		public string PresentationNSC
		{
		  get { return presentationNSC; }
		  set { presentationNSC = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PresentationPatient", IsRequired = false, Order = 22)]
		public string PresentationPatient
		{
		  get { return presentationPatient; }
		  set { presentationPatient = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PresentationNProc", IsRequired = false, Order = 23)]
		public string PresentationNProc
		{
		  get { return presentationNProc; }
		  set { presentationNProc = value; }
		}				
	}
}
