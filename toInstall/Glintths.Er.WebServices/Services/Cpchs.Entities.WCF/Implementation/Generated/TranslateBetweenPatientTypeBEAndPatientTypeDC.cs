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
    public static class TranslateBetweenPatientTypeBEAndPatientTypeDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.PatientType TranslatePatientTypeToPatientType(Cpchs.Entities.WCF.DataContracts.PatientType from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.PatientType to = new Cpchs.Eresults.Common.WCF.BusinessEntities.PatientType();
            to.PattypId = from.Id;
            to.PattypCode = from.Code;
            to.PattypAcronym = from.Acronym;
            to.PattypDescription = from.Description;
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.PatientType TranslatePatientTypeToPatientType(Cpchs.Eresults.Common.WCF.BusinessEntities.PatientType from)
        {
            Cpchs.Entities.WCF.DataContracts.PatientType to = new Cpchs.Entities.WCF.DataContracts.PatientType();
            to.Id = from.PattypId;
            to.Code = from.PattypCode;
            to.Acronym = from.PattypAcronym;
            to.Description = from.PattypDescription;
            return to;
        }
    }
}
