
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
	/// Service Contract Class - GetAlertsSubscriptionsResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetAlertsSubscriptionsResponse
	{
		private Cpchs.Documents.WCF.DataContracts.AlertList alertSubscriptions;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "AlertSubscriptions")]
		public Cpchs.Documents.WCF.DataContracts.AlertList AlertSubscriptions
		{
			get { return alertSubscriptions; }
			set { alertSubscriptions = value; }
		}
	}
}
