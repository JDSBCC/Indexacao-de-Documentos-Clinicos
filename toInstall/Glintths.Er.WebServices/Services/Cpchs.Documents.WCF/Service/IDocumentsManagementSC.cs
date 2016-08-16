//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Net.Security;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.ServiceContracts
{
	/// <summary>
	/// Service Contract Class - DocumentsManagementSC
	/// </summary>
	public partial interface IDocumentsManagementSC 
	{
		[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.ConvertPdfToImages", ProtectionLevel = ProtectionLevel.None)]
		Cpchs.Documents.WCF.MessageContracts.ConvertPdfToImagesResponse ConvertPdfToImages(Cpchs.Documents.WCF.MessageContracts.ConvertPdfToImagesRequest request);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.OpenDynamicForm", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.OpenDynamicFormResponse OpenDynamicForm(Cpchs.Documents.WCF.MessageContracts.OpenDynamicFormRequest request);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.GetGeneralDocuments", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.GetGeneralDocumentsResponse GetGeneralDocuments(Cpchs.Documents.WCF.MessageContracts.GetGeneralDocumentsRequest r);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.GetCancelledDocuments", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.GetGeneralDocumentsResponse GetCancelledDocuments(Cpchs.Documents.WCF.MessageContracts.GetGeneralDocumentsRequest r);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.GetDocumentsToCancel", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.GetDocumentsByMultiCriteriaV2Response GetDocumentsToCancel(Cpchs.Documents.WCF.MessageContracts.GetDocumentsByMultiCriteriaV2Request request);
	

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.CancelDocumentPermanentlyV2", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.CancelDocumentPermanentlyResponseV2 CancelDocumentPermanentlyV2(Cpchs.Documents.WCF.MessageContracts.CancelDocumentPermanentlyRequestV2 request);

        [WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.Documents.DocumentsManagementSC.CancelDocumentLastVersionV2", ProtectionLevel = ProtectionLevel.None)]
        Cpchs.Documents.WCF.MessageContracts.CancelDocumentLastVersionResponseV2 CancelDocumentLastVersionV2(Cpchs.Documents.WCF.MessageContracts.CancelDocumentLastVersionRequestV2 request);
		
    }
}
