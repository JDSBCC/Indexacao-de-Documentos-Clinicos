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
    public static class TranslateBetweenGenderBEAndGenderDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.Gender TranslateGenderToGender(Cpchs.Entities.WCF.DataContracts.Gender from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.Gender to = new Cpchs.Eresults.Common.WCF.BusinessEntities.Gender();
            to.GenId = from.Id;
            to.GenCode = from.Code;
            to.GenAcronym = from.Acronym;
            to.GenDescription = from.Description;
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.Gender TranslateGenderToGender(Cpchs.Eresults.Common.WCF.BusinessEntities.Gender from)
        {
            Cpchs.Entities.WCF.DataContracts.Gender to = new Cpchs.Entities.WCF.DataContracts.Gender();
            to.Id = from.GenId;
            to.Code = from.GenCode;
            to.Acronym = from.GenAcronym;
            to.Description = from.GenDescription;
            return to;
        }
    }
}

