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

namespace Cpchs.Documents.WCF.ServiceImplementation
{	
	/// <summary>
	/// Service Class - FilesManagementWS
	/// </summary>
	[WCF::ServiceBehavior(Name = "FilesManagementWS", 
		Namespace = "urn:Cpchs.Documents", 
		InstanceContextMode = WCF::InstanceContextMode.PerSession, 
		ConcurrencyMode = WCF::ConcurrencyMode.Single )]
	public abstract class FilesManagementWSBase : Cpchs.Documents.WCF.ServiceContracts.IFilesManagementSC
	{
		#region FilesManagementSC Members

		public virtual Cpchs.Documents.WCF.MessageContracts.GetDocumentFilesResponse GetDocumentFiles(Cpchs.Documents.WCF.MessageContracts.GetDocumentFilesRequest request)
		{
			return null;
		}

		public virtual Cpchs.Documents.WCF.MessageContracts.GetFileByElementIdResponse GetFileByElementId(Cpchs.Documents.WCF.MessageContracts.GetFileByElementIdRequest request)
		{
			return null;
		}

		#endregion		
		
	}
	
	public partial class FilesManagementWS : FilesManagementWSBase
	{
	}
	
}

