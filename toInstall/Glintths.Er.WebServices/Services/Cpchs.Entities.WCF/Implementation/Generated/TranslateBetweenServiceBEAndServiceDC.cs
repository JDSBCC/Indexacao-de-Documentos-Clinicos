﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenServiceBEAndServiceDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.Service TranslateServiceToService(Cpchs.Entities.WCF.DataContracts.Service from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.Service to = new Cpchs.Eresults.Common.WCF.BusinessEntities.Service();
            to.ServiceId = from.Id;
            to.ServiceCode = from.Code;
            to.ServiceDescription = from.Description;
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.Service TranslateServiceToService(Cpchs.Eresults.Common.WCF.BusinessEntities.Service from)
        {
            Cpchs.Entities.WCF.DataContracts.Service to = new Cpchs.Entities.WCF.DataContracts.Service();
            to.Id = from.ServiceId;
            to.Code = string.IsNullOrEmpty(from.ServiceCode) ? from.ServiceAcronym : from.ServiceCode;
            to.Description = from.ServiceDescription;
            return to;
        }
    }
}
