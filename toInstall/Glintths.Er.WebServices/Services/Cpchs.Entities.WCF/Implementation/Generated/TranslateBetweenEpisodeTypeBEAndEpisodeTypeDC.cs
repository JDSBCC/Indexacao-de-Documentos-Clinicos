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
    public static class TranslateBetweenEpisodeTypeBEAndEpisodeTypeDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType TranslateEpisodeTypeToEpisodeType(Cpchs.Entities.WCF.DataContracts.EpisodeType from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType to = new Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType();
            to.EpiTypeId = from.Id;
            to.EpiTypeCode = from.Code;
            to.EpiTypeAcronym = from.Acronym;
            to.EpiTypeDescription = from.Description;
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.EpisodeType TranslateEpisodeTypeToEpisodeType(Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType from)
        {
            Cpchs.Entities.WCF.DataContracts.EpisodeType to = new Cpchs.Entities.WCF.DataContracts.EpisodeType();
            to.Id = from.EpiTypeId;
            to.Code = from.EpiTypeCode;
            to.Acronym = from.EpiTypeAcronym;
            to.Description = from.EpiTypeDescription;
            return to;
        }
    }
}