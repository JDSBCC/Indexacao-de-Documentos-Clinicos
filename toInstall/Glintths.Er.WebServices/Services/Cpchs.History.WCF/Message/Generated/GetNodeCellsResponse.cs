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

namespace Cpchs.History.WCF.MessageContracts
{
	/// <summary>
	/// Service Contract Class - GetNodeCellsResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetNodeCellsResponse
	{
		private Cpchs.History.WCF.DataContracts.NodeDistrsList nodeCells;
	 		
		[WCF::MessageBodyMember(Name = "NodeCells")] 
		public Cpchs.History.WCF.DataContracts.NodeDistrsList NodeCells
		{
			get { return nodeCells; }
			set { nodeCells = value; }
		}
	}
}
