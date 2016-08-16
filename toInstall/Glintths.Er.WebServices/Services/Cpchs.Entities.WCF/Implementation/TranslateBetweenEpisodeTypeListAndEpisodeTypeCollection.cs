using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenEpisodeTypeListAndEpisodeTypeCollection
    {
        public static EpisodeTypeCollection TranslateEpisodeTypesToEpisodeTypes(Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeTypeList from)
        {
            EpisodeTypeCollection to = new EpisodeTypeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.EpisodeType epi in from.Items)
            {
                to.Add(TranslateBetweenEpisodeTypeBEAndEpisodeTypeDC.TranslateEpisodeTypeToEpisodeType(epi));
            }
            return to;
        }
    }
}