using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF = global::System.ServiceModel;

namespace Cpchs.Documents.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "ConvertPdfToImagesRequest", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
    public partial class ConvertPdfToImagesRequest
    {
        private string requestUrl;
        private int page;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "RequestUrl")]
        public string RequestUrl
        {
            get { return requestUrl; }
            set { requestUrl = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Page")]
        public int Page
        {
            get { return page; }
            set { page = value; }
        }
    }
}
