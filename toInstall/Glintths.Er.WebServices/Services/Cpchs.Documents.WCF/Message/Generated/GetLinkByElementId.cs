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
	/// Service Contract Class - GetLinkByElementId
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetLinkByElementId
	{
		private Cpchs.Documents.WCF.DataContracts.Link link;
	 		
		[WCF::MessageBodyMember(Name = "Link")] 
		public Cpchs.Documents.WCF.DataContracts.Link Link
		{
			get { return link; }
			set { link = value; }
		}
	}
}
