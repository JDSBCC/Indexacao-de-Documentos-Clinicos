using System.Collections.Generic;
using Cpchs.Activities.WCF.DataContracts;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenMicroResAndMicro
    {
        public static Micro TranslateMicroResToMicro(Eresults.Common.WCF.BusinessEntities.MicroRes from)
        {
            Micro to = new Micro
            {
                microId = from.Micro.Code,
                microName = from.Micro.Descr,
                microQuantif = from.Quantif,
                microReqId = from.ReqId,
                microReqDate = from.ReqDate,
                microProduct = from.Product,
                microResDate = from.ResDate,
                microValDate = from.ValDate,
                microOrder = from.MicroOrder,
                microResNotes = new Notes(),
                microReqNotes = new Notes()
            };
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.Notes.Items)
            {
                to.microResNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
            }
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.ReqNotes.Items)
            {
                to.microReqNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
            }
            if (from.Antibs != null && from.Antibs.Count > 0)
            {
                to.microAntibs = new AntibResults();
                foreach (Eresults.Common.WCF.BusinessEntities.AntibRes antib in from.Antibs.Items)
                {
                    to.microAntibs.Add(TranslateBetweenAntibResAndAntib.TranslateAntibResToAntib(antib));
                }
            }
            Dictionary<string, string> dicInfo = new Dictionary<string, string>
                                                     {
                                                         {"EpiType", from.EpiType},
                                                         {"EpiId", from.EpiId},
                                                         {"EpiBeginDate", from.EpiBeginDate == null ? "" : from.EpiBeginDate.ToString()},
                                                         {"EpiEndDate", from.EpiEndDate == null ? "" : from.EpiEndDate.ToString()},
                                                         {"SerReq", @from.SerReq},
                                                         {"SerExe", @from.SerExec},
                                                         {"EspReq", @from.EspReq},
                                                         {"EspExe", @from.EspExec},
                                                         {"ExtId", @from.ExtId},
                                                         {"DocDate", @from.DocDate == null ? "" : from.DocDate.ToString()},
                                                         {"DocId", @from.DocId.ToString()},
                                                         {"DocRef", @from.ReqId},
                                                         {"ArtId", @from.ElemId.ToString()},
                                                         {"ArtVersion", @from.VerCod.ToString()},
                                                         {"AppId", @from.AppId.ToString()},
                                                         {"DocTypeId", @from.DocTypeId.ToString()}
                                                     };
            to.microExamInfo = dicInfo;
            return to;
        }
    }
}