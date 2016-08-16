using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public class TranslateBetweenExternalEpisodesListBEAndPatientEpisodesDC
    {
        internal static PatientEpisodes TranslateExternalEpisodeListToPatientEpisodes(ExternalEpisodeList patList)
        {
            PatientEpisodes patEpis = new PatientEpisodes();
            patEpis.EpisodesList = new PatientEpisodeCollection();
            foreach (var item in patList.Items)
            {
                PatientEpisode patEpi = new PatientEpisode();
                patEpi.Episode = item.Episodeid;
                patEpi.EpisodeTypeDescription = item.Episodetype;
                patEpi.StartDate = item.Episodestartdt;
                patEpi.EndDate = item.Episodeenddt;
                patEpis.EpisodesList.Add(patEpi);
            }

            return patEpis;
        }
    }
}
