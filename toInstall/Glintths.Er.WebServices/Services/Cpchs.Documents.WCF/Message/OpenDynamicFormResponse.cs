
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
    /// Service Contract Class - GetDocumentsForEPRResponse
	/// </summary>
    [WCF::MessageContract(WrapperName = "OpenDynamicFormResponse", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)]
    public partial class OpenDynamicFormResponse
	{
        private string url;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Url")]
        public string Url
		{
			get { return url; }
			set { url = value; }
		}
	}
}

