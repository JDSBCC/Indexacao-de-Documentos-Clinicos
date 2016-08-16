using System.Collections.Generic;
using Cpchs.Activities.WCF.DataContracts;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAlphanumResAndAlphanum
    {
        public static Alphanum TranslateAlphanumResToAlphanum(Eresults.Common.WCF.BusinessEntities.AlphanumRes from)
        {
            Alphanum to = new Alphanum
            {
                alphanumResult = from.Res,
                alphanumUnit = from.Unid,
                alphanumInfRefVal = from.RvInf,
                alphanumSupRefVal = from.RvSup,
                alphanumTextRefVal = from.RvText,
                alphanumReqId = from.ReqId,
                alphanumReqDate = from.ReqDate,
                alphanumProduct = from.Product,
                alphanumResDate = from.ResDate,
                alphanumValDate = from.ValDate,
                alphanumOrder = from.ResOrder,
                alphanumResNotes = new Notes(),
                alphanumReqNotes = new Notes()
            };
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.Notes.Items)
            {
                to.alphanumResNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
            }
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.ReqNotes.Items)
            {
                to.alphanumReqNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
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
            to.alphanumExamInfo = dicInfo;
            return to;
        }
    }
}