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
	/// Service Contract Class - GetContextByNameRequest
	/// </summary>
	[WCF::MessageContract(WrapperName = "GetContextByNameRequest", WrapperNamespace = "urn:Glintths.Er.Interop")] 
	public partial class GetContextByNameRequest
	{
		private string companyDb;
	 	private string contextName;
	 		
		[WCF::MessageBodyMember(Name = "CompanyDb")] 
		public string CompanyDb
		{
			get { return companyDb; }
			set { companyDb = value; }
		}
			
		[WCF::MessageBodyMember(Name = "ContextName")] 
		public string ContextName
		{
			get { return contextName; }
			set { contextName = value; }
		}
	}
}

