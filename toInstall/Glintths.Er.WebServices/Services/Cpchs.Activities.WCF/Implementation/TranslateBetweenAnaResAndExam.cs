using Cpchs.Activities.WCF.DataContracts;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAnaResAndExam
    {
        public static Exam TranslateAnaResToExam(Eresults.Common.WCF.BusinessEntities.AnaRes from, string attachBaseUrl)
        {
            Exam to = new Exam();
            if (from == null)
            {
                return to;
            }
            to.examDescription = from.Exam.Descr;
            to.examOrder = from.ExamOrder;
            to.examProduct = from.Prod.Descr;
            to.examAcronym = from.Exam.Code;
            to.examNotes = new Notes();
            foreach (Eresults.Common.WCF.BusinessEntities.Note note in from.ResNotes.Items)
            {
                to.examNotes.Add(TranslateBetweenNoteBEAndNoteDC.TranslateNoteToNote(note));
            }
            if (from.Childs != null && from.Childs.Count > 0)
            {
                to.examChildExams = new Exams();
                foreach (Eresults.Common.WCF.BusinessEntities.AnaRes anaRes in from.Childs.Items)
                {
                    to.examChildExams.Add(TranslateAnaResToExam(anaRes, attachBaseUrl));
                }
            }
            if (from.Alphanums != null && from.Alphanums.Count > 0)
            {
                to.examAlphanumResults = new AlphanumResults();
                foreach (Eresults.Common.WCF.BusinessEntities.AlphanumRes alphanum in from.Alphanums.Items)
                {
                    to.examAlphanumResults.Add(TranslateBetweenAlphanumResAndAlphanum.TranslateAlphanumResToAlphanum(alphanum));
                }
            }
            if (from.Micros != null && from.Micros.Count > 0)
            {
                to.examMicroResults = new MicroResults();
                foreach (Eresults.Common.WCF.BusinessEntities.MicroRes micro in from.Micros.Items)
                {
                    to.examMicroResults.Add(TranslateBetweenMicroResAndMicro.TranslateMicroResToMicro(micro));
                }
            }
            if (from.Attachs != null && from.Attachs.Count > 0)
            {
                to.examAttachResults = new AttachResults();
                foreach (Eresults.Common.WCF.BusinessEntities.AttachRes attach in from.Attachs.Items)
                {
                    to.examAttachResults.Add(TranslateBetweenAttachResAndAttach.TranslateAttachResToAttach(attach, attachBaseUrl));
                }
            }
            return to;
        }
    }
}