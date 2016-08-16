using System.Collections.Generic;
using System.Globalization;
using Cpchs.Activities.WCF.DataContracts;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAttachResAndAttach
    {
        public static Attach TranslateAttachResToAttach(Eresults.Common.WCF.BusinessEntities.AttachRes from, string attachBaseUrl)
        {
            Attach to = new Attach
            {
                attachDescr = from.Descr,
                attachReqId = from.ReqId,
                attachReqDate = from.ReqDate,
                attachProduct = from.Product,
                attachResDate = from.ResDate,
                attachValDate = from.ValDate,
                attachOrder = from.ResOrder,
                attachResNotes = new Notes(),
                attachReqNotes = new Notes()
            };
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.Notes.Items)
            {
                to.attachResNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
            }
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.ReqNotes.Items)
            {
                to.attachReqNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
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
            to.attachExamInfo = dicInfo;
            if (from.Name != null)
            {
                to.attachExtension = from.Name.Contains(".") ? from.Name.Substring(from.Name.LastIndexOf('.'), from.Name.Length - from.Name.LastIndexOf('.')) : ".qqc";
            }
            else
            {
                to.attachExtension = from.Name;
            }
            to.attachEncryption = true;
            to.attachBaseUrl = attachBaseUrl;
            to.attachQueryUrl = "attachResId=" + from.Id.ToString(CultureInfo.InvariantCulture);
            return to;
        }
    }
}