using Cpchs.Documents.WCF.MessageContracts;
using Cpchs.Documents.WCF.BusinessLogic;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public partial class LinksManagementWS
    {
        public override GetDocumentLinksResponse GetDocumentLinks(GetDocumentLinksRequest request)
        {
            LinkList linkList = LinkLogic.GetDocumentLinks(request.CompanyDb,request.DocId);

            DataContracts.LinkList links = new DataContracts.LinkList
                                               {Links = new Cpchs.Documents.WCF.DataContracts.Links()};
            foreach(Link obj in linkList.Items)
            {
                DataContracts.Link link = TranslateBetweenLinkBeAndLinkDc.TranslateLinkToLink(obj);
                links.Links.Add(link);
            }
            GetDocumentLinksResponse response = new GetDocumentLinksResponse {DocumentLinks = links};
            return response;
        }

        public override GetLinkByElementIdResponse GetLinkByElementId(GetLinkByElementIdRequest request)
        {
            GetLinkByElementIdResponse response = new GetLinkByElementIdResponse {Link = new DataContracts.Link()};
            Link link = LinkLogic.GetLinkByElementId(request.CompanyDb, request.ElementId);
            if (link != null)
            {
                DataContracts.Link linkInfo = TranslateBetweenLinkBeAndLinkDc.TranslateLinkToLink(link);
                response.Link = linkInfo;
            }
            return response;
        }

    }
}
