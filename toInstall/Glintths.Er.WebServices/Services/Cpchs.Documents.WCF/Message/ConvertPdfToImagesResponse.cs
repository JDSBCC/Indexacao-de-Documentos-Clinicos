using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF = global::System.ServiceModel;


namespace Cpchs.Documents.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "ConvertPdfToImagesResponse", WrapperNamespace = "urn:Cpchs.Documents", IsWrapped = true)] 
    public partial class ConvertPdfToImagesResponse
    {
        private List<String> images;
        private int totalPages;
        private int currentPage;

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "Images")]
        public List<String> Images
        {
            get { return images; }
            set { images = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "TotalPages")]
        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        [WCF::MessageBodyMember(Namespace = "urn:Cpchs.Documents", Name = "CurrentPage")]
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }
    }
}
