
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
	/// Service Contract Class - GetAllPatientDocumentsMultiResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetAllPatientDocumentsMultiResponse
	{
		private Cpchs.Documents.WCF.DataContracts.PatientDocumentList patientDocumentsTree;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "PatientDocumentsTree")]
		public Cpchs.Documents.WCF.DataContracts.PatientDocumentList PatientDocumentsTree
		{
			get { return patientDocumentsTree; }
			set { patientDocumentsTree = value; }
		}
	}
}
