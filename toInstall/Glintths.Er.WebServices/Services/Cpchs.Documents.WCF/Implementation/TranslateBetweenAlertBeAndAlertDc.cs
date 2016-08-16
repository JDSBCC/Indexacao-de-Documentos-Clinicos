using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Documents.WCF.ServiceImplementation
{
    public static class TranslateBetweenAlertBeAndAlertDc
    {
        public static Alert TranslateAlertToAlert(DataContracts.Alert from)
        {
            Alert to = new Alert
                           {
                               AlertId = from.AlertId.HasValue ? from.AlertId.Value : 0,
                               AlertDate = from.Date,
                               AlertStatusId = from.StatusId.HasValue ? from.StatusId.Value : 0,
                               AlertSubsId = from.SubscriptionId.HasValue ? from.SubscriptionId.Value : 0,
                               alertHasLogs = from.HasLogs ? "S" : "N",
                               alertIsCorrectable = from.IsCorrectable ? "S" : "N",
                               alertIsIgnorable = from.IsIgnorable ? "S" : "N",
                               AlertSubscription =
                                   new AlertSubscription
                                       {
                                           AlertSubsActive = from.IsActive ? "S" : "N",
                                           AlertSubsAlertTypeId =
                                               from.AlertTypeId.HasValue ? from.AlertTypeId.Value : 0,
                                           AlertSubsAppId = from.AppId.HasValue ? from.AppId.Value : 0,
                                           AlertSubsDocTypeId = from.DocTypeId.HasValue ? from.DocTypeId.Value : 0,
                                           AlertSubsId = from.SubscriptionId.HasValue ? from.SubscriptionId.Value : 0,
                                           AlertSubsInstId = from.InstId.HasValue ? from.InstId.Value : 0,
                                           AlertSubsNotify = from.Notify ? "S" : "N",
                                           AlertSubsPalceId = from.PlaceId.HasValue ? from.PlaceId.Value : 0,
                                           AlertSubsParam = from.Parameter,
                                           AlertSubsUserId = Convert.ToInt64(from.UserId),
                                           AlertTypeBE =
                                               new AlertType
                                                   {
                                                       AlertTypeId =
                                                           from.AlertTypeId.HasValue ? from.AlertTypeId.Value : 0
                                                   }
                                       }
                           };
            to.AlertSubscription.Username = from.UserName;
            to.AlertSubscription.CrudCode = from.CrudCode;
            return to;
        }

        public static DataContracts.Alert TranslateAlertToAlert(Alert from)
        {
            DataContracts.Alert to = new DataContracts.Alert();
            if (from != null)
            {
                to.AppDesc = from.alertAppDesc;
                to.AlertId = from.AlertId;
                to.AlertTypeDesc = from.alertAlertTypeDesc;
                if (from.AlertSubscription != null)
                {
                    to.SubscriptionId = from.AlertSubscription.AlertSubsId;
                    to.AlertTypeId = from.AlertSubscription.AlertSubsAlertTypeId;
                    to.DocTypeId = from.AlertSubscription.AlertSubsDocTypeId;
                    to.AppId = from.AlertSubscription.AlertSubsAppId;
                    to.InstId = from.AlertSubscription.AlertSubsInstId;
                    to.IsActive = from.AlertSubscription.AlertSubsActive == "S";
                    to.Notify = from.AlertSubscription.AlertSubsNotify == "S";
                    to.Parameter = from.AlertSubscription.AlertSubsParam;
                    to.PlaceId = from.AlertSubscription.AlertSubsPalceId;
                    to.UserId = from.AlertSubscription.AlertSubsUserId;
                }
                else
                    to.SubscriptionId = from.AlertSubsId;
                to.HasLogs = from.alertHasLogs == "S";
                to.IsCorrectable = from.alertIsCorrectable == "S";
                to.IsIgnorable = @from.alertIsIgnorable == "S";

                if(from.AlertDate.HasValue)
                to.Date = from.AlertDate.Value ;
                to.DocTypeDesc = from.alertDocTypeDesc;
                to.InstDesc = from.alertInstDesc;
                to.PlaceDesc = from.alertPlaceDesc;
                to.StatusDesc = from.alertStatusDesc;
                to.StatusId = from.AlertStatusId;
            }
            return to;
        }
    }
}