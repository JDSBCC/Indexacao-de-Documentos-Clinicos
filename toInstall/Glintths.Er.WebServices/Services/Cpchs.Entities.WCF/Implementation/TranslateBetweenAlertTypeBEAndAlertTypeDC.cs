using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAlertTypeBEAndAlertTypeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.AlertType TranslateAlertTypeToAlertType(Cpchs.Eresults.Common.WCF.BusinessEntities.AlertType from)
        {
            Cpchs.Entities.WCF.DataContracts.AlertType to = new Cpchs.Entities.WCF.DataContracts.AlertType();
            to.Id = from.AlertTypeId;
            to.Code = from.AlertTypeCode;
            to.Acronym = from.AlertTypeAcronym;
            to.Description = from.AlertTypeDesc;
            return to;
        }
    }
}