using System;
using System.Collections.Generic;
using Cpchs.History.WCF.DataContracts;

namespace Cpchs.History.WCF.BusinessLogic
{
    public class HistoryLogic
    {
        public static void ParseEresultsAttributes(AttributesDict dic, out string globalFilters,
                                                   out string docsSessionFilters, out string servsSessionFilters,
                                                   out string userName, out string userAnaRes)
        {
            if (dic != null)
            {
                try
                {
                    globalFilters = dic["GlobalFilters"];
                    if (globalFilters != "N")
                    {
                        globalFilters = "S";
                    }
                }
                catch (KeyNotFoundException)
                {
                    globalFilters = "S";
                }

                try
                {
                    docsSessionFilters = dic["DocsSessionFilters"];
                    if (docsSessionFilters != "S")
                    {
                        docsSessionFilters = "N";
                    }
                }
                catch (KeyNotFoundException)
                {
                    docsSessionFilters = "N";
                }

                try
                {
                    servsSessionFilters = dic["ServsSessionFilters"];
                    if (servsSessionFilters != "S")
                    {
                        servsSessionFilters = "N";
                    }
                }
                catch (KeyNotFoundException)
                {
                    servsSessionFilters = "N";
                }

                try
                {
                    userName = dic["UserName"];
                }
                catch (KeyNotFoundException)
                {
                    userName = "";
                }

                try
                {
                    userAnaRes = dic["UserAnaRes"];
                    userAnaRes = userAnaRes.ToUpper() != "FALSE" ? "S" : "N";
                }
                catch (KeyNotFoundException)
                {
                    userAnaRes = "S";
                }
            }
            else
            {
                globalFilters = "S";
                docsSessionFilters = "N";
                servsSessionFilters = "N";
                userName = "";
                userAnaRes = "S";
            }
        }

        public static string GetTreeLevels(string companyDb, string scope, string searchId, AttributesDict dic)
        {
            string xml;
            switch (scope.ToUpper())
            {
                case "ERESULTS":
                    string globalFilters;
                    string docsSessionFilters;
                    string servsSessionFilters;
                    string userName;
                    string userAnaRes;

                    ParseEresultsAttributes(dic, out globalFilters, out docsSessionFilters, out servsSessionFilters,
                                            out userName, out userAnaRes);
                    xml =
                        Eresults.Common.WCF.BusinessEntities.HistoryManagementBER.Instance.GetTreeLevelsForEresults(
                            companyDb, searchId, globalFilters, docsSessionFilters, servsSessionFilters, userName,
                            userAnaRes);
                    break;
                default:
                    xml = "Unexpected scope";
                    break;
            }
            return xml;
        }

        public static string GetNodeCells(string companyDb, string scope, string mode, string searchId,
                                          DateTime? dateBegin, DateTime? dateEnd, AttributesDict dic)
        {
            string xml;
            switch (scope.ToUpper())
            {
                case "ERESULTS":
                    string globalFilters;
                    string docsSessionFilters;
                    string servsSessionFilters;
                    string userName;
                    string userAnaRes;
                    ParseEresultsAttributes(dic, out globalFilters, out docsSessionFilters, out servsSessionFilters,
                                            out userName, out userAnaRes);
                    xml =
                        Eresults.Common.WCF.BusinessEntities.HistoryManagementBER.Instance.GetNodeCellsForEresults(
                            companyDb, mode, searchId, dateBegin, dateEnd, globalFilters, docsSessionFilters,
                            servsSessionFilters, userName, userAnaRes);
                    break;
                default:
                    xml = "Unexpected scope";
                    break;
            }
            return xml;
        }
    }
}