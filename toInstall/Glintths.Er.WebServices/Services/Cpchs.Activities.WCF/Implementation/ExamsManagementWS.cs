using System.Configuration;
using System.Linq;
using Cpchs.Activities.WCF.BusinessLogic;
using Cpchs.Activities.WCF.DataContracts;
using Cpchs.Activities.WCF.MessageContracts;
using Cpchs.Eresults.Common.WCF.BusinessEntities;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public partial class ExamsManagementWS
    {
        public override GetExamsByDocumentIdResponse GetExamsByDocumentId(GetExamsByDocumentIdRequest request)
        {
            AnaResList result = ExamLogic.GetAnaResByDocId(request.companyDb, request.docId, request.username, request.session);
            string attachBaseUrl = GetAttachBaseUrl(request.companyDb);
            Exams exams = new Exams();
            exams.AddRange(result.Items.Select(exam => TranslateBetweenAnaResAndExam.TranslateAnaResToExam(exam, attachBaseUrl)));

            GetExamsByDocumentIdResponse response = new GetExamsByDocumentIdResponse
                                                    {Exams = new ExamList {Exams = exams}};
            return response;
        }

        public override GetPatientExamsMultiResponse GetPatientExamsMulti(GetPatientExamsMultiRequest request)
        {
            AnaResList result = ExamLogic.GetAnaResByMultiCrit(
                request.CompanyDb,
                request.PatEntIds,
                request.EpisodeTypeId,
                request.EpisodeId,
                request.Doc,
                request.ExtId,
                request.EpiDateBegin,
                request.EpiDateEnd,
                request.DocType,
                request.AppId,
                request.LocalId,
                request.InstId,
                request.DocDateBegin,
                request.DocDateEnd,
                request.ValDateBegin,
                request.ValDateEnd,
                request.GlobalFilters,
                request.DocsSessionFilters,
                request.ServsSessionFilters,
                request.UserName,
                string.Empty);
            string attachBaseUrl = GetAttachBaseUrl(request.CompanyDb);
            Exams exams = new Exams();
            exams.AddRange(result.Items.Select(exam => TranslateBetweenAnaResAndExam.TranslateAnaResToExam(exam, attachBaseUrl)));

            GetPatientExamsMultiResponse response = new GetPatientExamsMultiResponse
                                                    {PatientExams = new ExamList {Exams = exams}};
            return response;
        }

        private static string GetAttachBaseUrl(string companyDb)
        {
            try
            {
                ERConfiguration config = EntityManagementBER.Instance.GetConfigurationByScopeKey(companyDb, "FILEVIEWERURI", "INITCONFIG");
                return config.ErConfigValue;
            }
            catch
            {
                return ConfigurationSettings.AppSettings["FileBaseUrl"];
            }
        }
    }
}