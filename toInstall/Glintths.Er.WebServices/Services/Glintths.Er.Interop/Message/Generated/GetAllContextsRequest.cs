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
	/// Service Contract Class - GetAllContextsRequest
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class GetAllContextsRequest
	{
		private string companyDb;
	 		
		[WCF::MessageBodyMember(Name = "CompanyDb")] 
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
	}
}

