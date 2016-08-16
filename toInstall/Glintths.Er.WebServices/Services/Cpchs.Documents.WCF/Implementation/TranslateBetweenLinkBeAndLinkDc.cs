namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenLinkBeAndLinkDc
    {
        public static Eresults.Common.WCF.BusinessEntities.Link TranslateLinkToLink(DataContracts.Link from)
        {
            Eresults.Common.WCF.BusinessEntities.Link to = new Eresults.Common.WCF.BusinessEntities.Link
                                                               {LinkTypeBE = {LinkTypeLink = from.LinkBaseUrl}};
            return to;
        }

        public static DataContracts.Link TranslateLinkToLink(Eresults.Common.WCF.BusinessEntities.Link from)
        {
            DataContracts.Link to = new DataContracts.Link();
            if (from != null)
            {
                to.LinkQueryUrl = from.InstatiatedArgs;
                to.LinkBaseUrl = from.LinkTypeBE.LinkTypeLink;
                to.LinkEncryption = from.LinkTypeBE.LinkTypeEncrypt == "S";
                to.LinkExternalParams = from.ExternalParams;
                to.LinkQueryUrl = from.InstatiatedArgs;
                to.LinkSeparator = from.LinkTypeBE.LinkTypeSeparator;
                to.LinkElementId = from.LinkElemId;
                to.LinkVersionCode = from.LinkVersionCode;
                to.OpenExternally = from.LinkTypeBE.LinkTypeOpenExternally == "S";
            }
            return to;
        }
    }
}