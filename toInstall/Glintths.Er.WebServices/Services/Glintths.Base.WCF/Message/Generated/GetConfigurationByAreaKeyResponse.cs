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

namespace Glintths.Base.WCF.MessageContracts
{
	/// <summary>
	/// Service Contract Class - GetConfigurationByAreaKeyResponse
	/// </summary>
	[WCF::MessageContract(IsWrapped = true)] 
	public partial class GetConfigurationByAreaKeyResponse
	{
		private Glintths.Base.WCF.DataContracts.Codifications configurationList;
	 		
		[WCF::MessageBodyMember(Name = "ConfigurationList")] 
		public Glintths.Base.WCF.DataContracts.Codifications ConfigurationList
		{
			get { return configurationList; }
			set { configurationList = value; }
		}
	}
}

