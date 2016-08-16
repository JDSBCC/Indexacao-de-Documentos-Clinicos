using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenNoteBEAndNoteDC
    {
        public static Cpchs.Activities.WCF.DataContracts.Note TranslateNoteToNote(Cpchs.Eresults.Common.WCF.BusinessEntities.Note from)
        {
            Cpchs.Activities.WCF.DataContracts.Note to = new Cpchs.Activities.WCF.DataContracts.Note();
            to.NoteBody = from.Body;
            to.NoteReqRef = from.ReqRef;
            return to;
        }
    }
}
