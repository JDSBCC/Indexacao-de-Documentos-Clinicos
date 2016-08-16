namespace Cpchs.Documents.WCF.ServiceImplementation
{
    class TranslateBetweenVisitBeAndPatientEpisodeExamDC
    {
        internal static Entities.WCF.DataContracts.PatientEpisode TranslateVisitToPatientEpisode(Eresults.Common.WCF.BusinessEntities.Visit from)
        {
            Entities.WCF.DataContracts.PatientEpisode to = new Entities.WCF.DataContracts.PatientEpisode
                                                               {
                                                                   VisitId = from.VisitId,
                                                                   EndDate = from.VisitDtEnd,
                                                                   Episode = from.VisitEpisode,
                                                                   EpisodeTypeId = from.VisitEpiTypeId,
                                                                   EpisodeTypeAcronym =
                                                                       from.VisitEpisodeType != null
                                                                           ? from.VisitEpisodeType.EpiTypeAcronym
                                                                           : null,
                                                                   EpisodeTypeCode =
                                                                       from.VisitEpisodeType != null
                                                                           ? from.VisitEpisodeType.EpiTypeCode
                                                                           : null,
                                                                   EpisodeTypeDescription =
                                                                       from.VisitEpisodeType != null
                                                                           ? from.VisitEpisodeType.EpiTypeDescription
                                                                           : null,
                                                                   InstId = from.VisitInstId,
                                                                   LocalId = from.VisitLocalId,
                                                                   StartDate = from.VisitDtEnd,
                                                                   ParentVisitId = from.VisitParentId
                                                               };
            return to;
        }

        internal static Eresults.Common.WCF.BusinessEntities.Visit TranslateVisitToPatientEpisode( Cpchs.Entities.WCF.DataContracts.PatientEpisode from )
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.Visit to = new Eresults.Common.WCF.BusinessEntities.Visit
                                                                      {
                                                                          VisitId = from.VisitId,
                                                                          VisitDtEnd = from.EndDate,
                                                                          VisitEpisode = from.Episode,
                                                                          VisitEpiTypeId = from.EpisodeTypeId,
                                                                          VisitEpisodeType =
                                                                              new Eresults.Common.WCF.
                                                                              BusinessEntities.EpisodeType
                                                                                  {
                                                                                      EpiTypeId = from.EpisodeTypeId,
                                                                                      EpiTypeAcronym =
                                                                                          from.EpisodeTypeAcronym,
                                                                                      EpiTypeCode =
                                                                                          from.EpisodeTypeCode,
                                                                                      EpiTypeDescription =
                                                                                          from.EpisodeTypeDescription
                                                                                  },
                                                                          VisitInstId = from.InstId,
                                                                          VisitLocalId = from.LocalId
                                                                      };
            to.VisitDtEnd = from.StartDate;
            if(from.ParentVisitId.HasValue)
                to.VisitParentId = from.ParentVisitId.Value;
            return to;
        }
    }
}