
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
	/// Service Contract Class - GetDocumentVideosResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetDocumentVideosResponse
	{
		private Cpchs.Documents.WCF.DataContracts.VideoList documentVideos;
	 		
		[WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "DocumentVideos")]
		public Cpchs.Documents.WCF.DataContracts.VideoList DocumentVideos
		{
			get { return documentVideos; }
			set { documentVideos = value; }
		}
	}
}

