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

namespace Cpchs.History.WCF.ServiceContracts
{
	/// <summary>
	/// Service Contract Class - HistoryManagementSC
	/// </summary>
	[WCF::ServiceContract(Namespace = "urn:Cpchs.History", Name = "HistoryManagementSC", SessionMode = WCF::SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None )]
	public partial interface IHistoryManagementSC 
	{
		[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.History.HistoryManagementSC.GetTreeLevels", ReplyAction = "urn:Cpchs.History.HistoryManagementSC.GetTreeLevels", ProtectionLevel = ProtectionLevel.None)]
		Cpchs.History.WCF.MessageContracts.GetTreeLevelsResponse GetTreeLevels(Cpchs.History.WCF.MessageContracts.GetTreeLevelsRequest request);

[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "urn:Cpchs.History.HistoryManagementSC.GetNodeCells", ReplyAction = "urn:Cpchs.History.HistoryManagementSC.GetNodeCells", ProtectionLevel = ProtectionLevel.None)]
		Cpchs.History.WCF.MessageContracts.GetNodeCellsResponse GetNodeCells(Cpchs.History.WCF.MessageContracts.GetNodeCellsRequest request);

	}
}

