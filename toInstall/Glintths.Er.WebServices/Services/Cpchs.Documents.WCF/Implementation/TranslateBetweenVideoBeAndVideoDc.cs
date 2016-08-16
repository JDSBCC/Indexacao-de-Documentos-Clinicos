using System.Linq;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenVideoBeAndVideoDc
    {
        public static DataContracts.Video TranslateVideoBeToVideoDc(Eresults.Common.WCF.BusinessEntities.Video from)
        {
            DataContracts.Video to = new DataContracts.Video
                                         {
                                             VideoDesc = from.VideoDesc,
                                             VideoDuration = from.VideoDuration,
                                             VideoThumbPreviewUri = from.VideoThumbPreviewUri,
                                             VideoElementId = from.VideoElemId,
                                             VideoVersionCode = from.VideoVersionCode,
                                             VideoLinks =
                                                 TranslateVideoLinksBeToVideoLinksDc(
                                                     from.VideoLinks)
                                         };
            return to;
        }

        public static DataContracts.VideoList TranslateVideoListBeToVideoListDc(Eresults.Common.WCF.BusinessEntities.VideoList from)
        {
            DataContracts.VideoList to = new DataContracts.VideoList {Videos = new DataContracts.Videos()};
            foreach (Eresults.Common.WCF.BusinessEntities.Video item in from.Items)
            {
                to.Videos.Add(TranslateVideoBeToVideoDc(item));
            }
            return to;
        }

        public static DataContracts.VideoLink TranslateVideoLinkBeToVideoLinkDc(Eresults.Common.WCF.BusinessEntities.VideoLink from)
        {
            DataContracts.VideoLink to = new DataContracts.VideoLink
                                             {
                                                 VideoLinkElementId = from.VideoLinkElemId,
                                                 VideoLinkVersionCode = from.VideoLinkVersionCode,
                                                 VideoLinkId = from.VideoLinkId,
                                                 VideoLinkLink = from.VideoLinkUrl,
                                                 VideoLinkVResolution = from.VideoLinkVResolution,
                                                 VideoLinkHResolution = from.VideoLinkHResolution,
                                                 VideoLinkSize = from.VideoLinkSize,
                                                 VideoLinkType = from.VideoLinkType
                                             };
            return to;
        }

        public static DataContracts.VideoLinks TranslateVideoLinksBeToVideoLinksDc(Eresults.Common.WCF.BusinessEntities.VideoLinkList from)
        {
            DataContracts.VideoLinks to = new DataContracts.VideoLinks();
            to.AddRange(from.Items.Select(TranslateVideoLinkBeToVideoLinkDc));
            return to;
        }
    }
}