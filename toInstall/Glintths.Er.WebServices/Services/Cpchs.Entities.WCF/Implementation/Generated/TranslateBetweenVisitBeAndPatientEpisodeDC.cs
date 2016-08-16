using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    class TranslateBetweenVisitBeAndPatientEpisodeDC
    {
        internal static Cpchs.Entities.WCF.DataContracts.PatientEpisode TranslateVisitToPatientEpisode( Cpchs.Eresults.Common.WCF.BusinessEntities.Visit from )
        {
            Cpchs.Entities.WCF.DataContracts.PatientEpisode to = new Cpchs.Entities.WCF.DataContracts.PatientEpisode();

            to.VisitId = from.VisitId;
            to.EndDate = from.VisitDtEnd;
            to.Episode = from.VisitEpisode;

            to.EpisodeTypeId = from.VisitEpiTypeId;
            to.EpisodeTypeAcronym = from.VisitEpisodeType != null ? from.VisitEpisodeType.EpiTypeAcronym : null;
            to.EpisodeTypeCode = from.VisitEpisodeType != null ? from.VisitEpisodeType.EpiTypeCode : null;
            to.EpisodeTypeDescription = from.VisitEpisodeType != null ? from.VisitEpisodeType.EpiTypeDescription : null;

            to.InstId = from.VisitInstId;
            to.LocalId = from.VisitLocalId;

            to.StartDate = from.VisitDtIni;

            to.ParentVisitId = from.VisitParentId;

            to.EntId = from.VisitEntId;

            to.Episode = from.Episode;
            to.EpisodeTypeCode = from.EpisodeType;
            to.EpisodeTypeDescription = from.EpisodeType;
            to.ServiceReq = from.ServiceReq;
            to.ServiceReqDesc = from.ServiceReqDesc;

            to.Patient = from.Patient;
            to.PatientType = from.PatientType;

            return to;
        }

        internal static Cpchs.Eresults.Common.WCF.BusinessEntities.Visit TranslateVisitToPatientEpisode(Cpchs.Entities.WCF.DataContracts.PatientEpisode from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.Visit to = new Cpchs.Eresults.Common.WCF.BusinessEntities.Visit();

            to.VisitId = from.VisitId;
            to.VisitDtEnd = from.EndDate;
            to.VisitEpisode = from.Episode;

            to.VisitEpiTypeId = from.EpisodeTypeId;

            to.VisitEpisodeType = new Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType();
            to.VisitEpisodeType.EpiTypeId = from.EpisodeTypeId;
            to.VisitEpisodeType.EpiTypeAcronym = from.EpisodeTypeAcronym;
            to.VisitEpisodeType.EpiTypeCode = from.EpisodeTypeCode;
            to.VisitEpisodeType.EpiTypeDescription = from.EpisodeTypeDescription;

            to.VisitInstId = from.InstId;
            to.VisitLocalId = from.LocalId;

            to.VisitDtEnd = from.StartDate;

            if (from.EntId.HasValue)
                to.VisitEntId = from.EntId.Value;

            if (from.ParentVisitId.HasValue)
                to.VisitParentId = from.ParentVisitId.Value;

            return to;
        }
    }
}
