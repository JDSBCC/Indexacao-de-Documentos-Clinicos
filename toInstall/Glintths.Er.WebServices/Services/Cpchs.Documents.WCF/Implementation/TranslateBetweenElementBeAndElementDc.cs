using System;
using System.Globalization;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenElementBeAndElementDc
    {
        public static Eresults.Common.WCF.BusinessEntities.Element TranslateElementToElement(DataContracts.ElementInfoForReport from)
        {
            Eresults.Common.WCF.BusinessEntities.Element to = new Eresults.Common.WCF.BusinessEntities.Element
                                                                  {
                                                                      ElementId = from.ElementId,
                                                                      ElementDescription = from.Description,
                                                                      ElementReport = from.ForReport ? "S" : "N",
                                                                      ElementReportPresOrder =
                                                                          from.PresentationOrder.HasValue
                                                                              ? from.PresentationOrder.Value
                                                                              : 0,
                                                                  };
            return to;
        }

        public static Eresults.Common.WCF.BusinessEntities.Element TranslateElementToElement(DataContracts.Element from)
        {
            Eresults.Common.WCF.BusinessEntities.Element to = new Eresults.Common.WCF.BusinessEntities.Element
                                                                  {
                                                                      ElementId = from.Id,
                                                                      ElementDescription = from.Description,
                                                                      ElementDate = from.ValidationDate
                                                                  };
            return to;
        }

        public static DataContracts.Element TranslateElementToElement(Eresults.Common.WCF.BusinessEntities.Element from)
        {
                DataContracts.Element to = new DataContracts.Element
                                               {
                                                   Id = from.ElementId,
                                                   Description = from.ElementDescription,
                                                   ValidationDate = from.ElementDate.HasValue ? from.ElementDate.Value : DateTime.MinValue,
                                                   Version = from.ElementVersion.ToString(CultureInfo.InvariantCulture),
                                                   ReportPresOrder = from.ElementReportPresOrder,
                                                   ForReport = from.ElementReport == "S",
                                                   User = from.ElementUser,
                                                   Title=from.ElementTitle,
                                                   UpdateDate = from.ElementUpdateDate
                                               };
                switch (from.ElemType)
                {
                    case "exam":
                        to.ElementType = DataContracts.ElementType.AnalyticalResult;
                        break;
                    case "file":
                        to.ElementType = DataContracts.ElementType.File;
                        break;
                    case "link":
                        to.ElementType = DataContracts.ElementType.Link;
                        break;
                    case "video":
                        to.ElementType = DataContracts.ElementType.Video;
                        break;
                    case "mix":
                        to.ElementType = DataContracts.ElementType.Mix;
                        break;
                }
                
                return to;
            }
    }
}