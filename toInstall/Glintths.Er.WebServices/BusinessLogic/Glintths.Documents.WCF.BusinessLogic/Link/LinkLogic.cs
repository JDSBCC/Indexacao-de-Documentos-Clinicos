using System.Collections.Generic;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public class LinkLogic
    {
        public static LinkList GetDocumentLinks(string companyDb, long docId)
        {
            LinkList linkList = LinkManagementBER.Instance.GetDocumentLinks(companyDb, docId);
            LinkParamList linkParamList = LinkManagementBER.Instance.GetDocumentLinksParams(companyDb, docId);
            LinkTypeArgList linkTypeArgList = LinkManagementBER.Instance.GetAllLinkTypeArguments(companyDb);
            foreach (Link link in linkList.Items)
            {
                LinkTypeArgList currentLinkTypeArgs = SelectCurrentLinkTypeArgList(linkTypeArgList, link.LinkTypeId);
                link.ExternalParams = new List<string>();
                string argsString = string.Empty;
                foreach (LinkTypeArg linkTypeArg in currentLinkTypeArgs.Items)
                {
                    if (linkTypeArg.LinkTypeArgExternal == "S")
                    {
                        link.ExternalParams.Add(linkTypeArg.LinkTypeArgArg);
                    }
                    else if (linkTypeArg.LinkTypeArgStatic == "S")
                    {
                        argsString = ConcatParam(argsString, link.LinkTypeBE.LinkTypeSeparator);
                        argsString += linkTypeArg.LinkTypeArgArg + "=" + linkTypeArg.LinkTypeArgMask;
                    }
                    else
                    {
                        string argInstantiated = ArgIsInstantiated(linkTypeArg, link, linkParamList);
                        if (!string.IsNullOrEmpty(argInstantiated))
                        {
                            argsString = ConcatParam(argsString, link.LinkTypeBE.LinkTypeSeparator);
                            argsString += argInstantiated;
                        }
                    }
                }
                link.InstatiatedArgs = argsString;
            }
            return linkList;
        }

        private static string ConcatParam(string queryStr, string separator)
        {
            if (!string.IsNullOrEmpty(queryStr))
            {
                return queryStr + separator;
            }
            return queryStr;
        }

        private static string ArgIsInstantiated(LinkTypeArg linkTypeArg, Link link, LinkParamList linkParamList)
        {
            string argInstantiated = "";
            foreach (LinkParam param in linkParamList.Items.Where(param => link.LinkElemId == param.LinkParamElemId && link.LinkVersionCode == param.LinkParamVersionCode && linkTypeArg.LinkTypeArgMask.ToUpper().Replace("#", "") == param.LinkParamArg.ToUpper()))
            {
                argInstantiated = 
                    linkTypeArg.LinkTypeArgArg + 
                    (string.IsNullOrEmpty(linkTypeArg.LinkTypeArgArg) ? string.Empty : "=") + 
                    linkTypeArg.LinkTypeArgMask.ToUpper().Replace("#" + param.LinkParamArg.ToUpper() + "#", param.LinkParamValue);
                if (!argInstantiated.Contains('#'))
                {
                    break;
                }
                return "";
            }
            return argInstantiated;
        }

        private static LinkTypeArgList SelectCurrentLinkTypeArgList(LinkTypeArgList linkTypeArgList, long linkTypeId)
        {
            LinkTypeArgList currentLinkTypeArgs = new LinkTypeArgList();
            foreach (LinkTypeArg arg in linkTypeArgList.Items.Where(arg => arg.LinkTypeArgLinkTypeId == linkTypeId))
            {
                currentLinkTypeArgs.Items.Add(arg);
            }
            return currentLinkTypeArgs;
        }

        public static Link GetLinkByElementId(string companyDb, long elementId)
        {
            Link link = LinkManagementBER.Instance.GetLinkByElementId(companyDb, elementId);
            LinkParamList linkParamList = LinkManagementBER.Instance.GetLinkByElementIdParams(companyDb, elementId);
            LinkTypeArgList linkTypeArgList = LinkManagementBER.Instance.GetAllLinkTypeArguments(companyDb);
            LinkTypeArgList currentLinkTypeArgs = SelectCurrentLinkTypeArgList(linkTypeArgList, link.LinkTypeId);
            link.ExternalParams = new List<string>();
            string argsString = string.Empty;
            foreach (LinkTypeArg linkTypeArg in currentLinkTypeArgs.Items)
            {
                if (linkTypeArg.LinkTypeArgExternal == "S")
                {
                    link.ExternalParams.Add(linkTypeArg.LinkTypeArgArg);
                }
                else if (linkTypeArg.LinkTypeArgStatic == "S")
                {
                    argsString = ConcatParam(argsString, link.LinkTypeBE.LinkTypeSeparator);
                    argsString += linkTypeArg.LinkTypeArgArg + "=" + linkTypeArg.LinkTypeArgMask;
                }
                else
                {
                    string argInstantiated = ArgIsInstantiated(linkTypeArg, link, linkParamList);
                    if (!string.IsNullOrEmpty(argInstantiated))
                    {
                        argsString = ConcatParam(argsString, link.LinkTypeBE.LinkTypeSeparator);
                        argsString += argInstantiated;
                    }
                }
            }
            link.InstatiatedArgs = argsString;
            return link;
        }
    }
}