
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

namespace Glintths.Er.Workspaces.MessageContracts
{
	/// <summary>
	/// Service Contract Class - ActivateWorkspaceRequest
	/// </summary>
	[WCF::MessageContract(WrapperName = "ActivateWorkspaceRequest", WrapperNamespace = "http://Glintths.Er.Services.Model/2011/Glintths")] 
	public partial class ActivateWorkspaceRequest
	{
		private string companyDB;
	 	private long workspaceId;
	 		
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "CompanyDB")]
		public string CompanyDB
		{
			get { return companyDB; }
			set { companyDB = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "WorkspaceId")]
		public long WorkspaceId
		{
			get { return workspaceId; }
			set { workspaceId = value; }
		}
	}
}

