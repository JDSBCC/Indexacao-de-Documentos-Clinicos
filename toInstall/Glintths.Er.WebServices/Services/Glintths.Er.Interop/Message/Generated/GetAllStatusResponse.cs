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

namespace Glintths.Er.Interop.MessageContracts
{
	/// <summary>
	/// Service Contract Class - GetAllStatusResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class GetAllStatusResponse
	{
		private Glintths.Er.Interop.DataContracts.StatusList mcdtsInteropStatusDataContract;
	 		
		[WCF::MessageBodyMember(Name = "McdtsInteropStatusDataContract")] 
		public Glintths.Er.Interop.DataContracts.StatusList McdtsInteropStatusDataContract
		{
			get { return mcdtsInteropStatusDataContract; }
			set { mcdtsInteropStatusDataContract = value; }
		}
	}
}
