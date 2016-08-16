using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAlertTypeListAndAlertTypeCollection
    {
        public static AlertTypeCollection TranslateAlertTypesToAlertTypes(AlertTypeList from)
        {
            AlertTypeCollection to = new AlertTypeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.AlertType alertType in from.Items)
            {
                to.Add(TranslateBetweenAlertTypeBEAndAlertTypeDC.TranslateAlertTypeToAlertType(alertType));
            }
            return to;
        }
    }
}