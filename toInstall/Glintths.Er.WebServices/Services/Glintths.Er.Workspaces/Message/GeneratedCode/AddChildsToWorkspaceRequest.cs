
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
	/// Service Contract Class - AddChildsToWorkspaceRequest
	/// </summary>
	[WCF::MessageContract(WrapperName = "AddChildsToWorkspaceRequest", WrapperNamespace = "http://Glintths.Er.Services.Model/2011/Glintths")] 
	public partial class AddChildsToWorkspaceRequest
	{
		private string companyDB;
	 	private long workspaceId;
	 	private Glintths.Er.Common.DataContracts.Workspaces childWorkspaces;
	 		
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
			
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "ChildWorkspaces")]
		public Glintths.Er.Common.DataContracts.Workspaces ChildWorkspaces
		{
			get { return childWorkspaces; }
			set { childWorkspaces = value; }
		}
	}
}
