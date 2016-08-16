using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.Data;

namespace Glintths.Er.Interop.BusinessLogic.VideoProcessor
{
    public class McdtsInteropLogic
    {
        public static VideoProcessorJobStateList GetWaitingJobs(string CompanyDb, long status)
        {
            return VideoProcessorBER.Instance.GetWaitingJobs(CompanyDb, status);
        }

        public static void UpdateJobStatus(string CompanyDb, int jobID, long status, DateTime time)
        {
            VideoProcessorBER.Instance.UpdateJobStatus(CompanyDb, new VideoProcessorJobState() { CompanyDB = CompanyDb, JobId = jobID, JobStatus = status, JobCompletedTimestamp=time });
        }

        public static void UpdateJobFileStatus(string CompanyDb, long jobId, string DeletedStatus)
        {
            VideoProcessorBER.Instance.UpdateJobClientFile(CompanyDb, new VideoProcessorJobState() { CompanyDB = CompanyDb, JobClientFileDeleted = DeletedStatus, JobId = jobId });
        }

        public static void UpdateJobPercentage(string CompanyDb, int jobID, int percentage)
        {
            VideoProcessorBER.Instance.UpdateJobPercentage(CompanyDb, new VideoProcessorJobState() { CompanyDB = CompanyDb, JobId = jobID, JobCompletedPercentage = percentage });
        }

        public static bool JobRegister(string CompanyDb, long computerName, long jobStatus, DateTime registered, DateTime completed, int percentage, long context, string fileStatus, string url, string fileDeleted, object xml, string patientData)
        {
            VideoProcessorJobState temp = VideoProcessorBER.Instance.JobRegister(CompanyDb, new VideoProcessorJobState() { JobClient = computerName, JobStatus = jobStatus, JobRegisterTimestamp = registered, JobCompletedTimestamp = completed, JobCompletedPercentage = percentage, JobContext = context, JobURL = url, JobClientFileDeleted = fileDeleted, JobXMLData=xml, JobData=patientData });
            if(temp.JobId > 0)
                return true;
            else
                return false;
        }

        public static void UpdateJobClob(string CompanyDb, int jobID, object XmlData)
        {
            VideoProcessorBER.Instance.UpdateJobClob(CompanyDb, new VideoProcessorJobState() { JobId = jobID, JobXMLData = XmlData });
        }

        public static VideoProcessorClient ComputerRegister(string CompanyDb, string ComputerName)
        {
            return VideoProcessorBER.Instance.RegistarMaquina(CompanyDb, new VideoProcessorClient() { ComputerName = ComputerName });
        }

        public static VideoProcessorContext AdicionarContexto(string CompanyDb, string ContextName)
        {
            return VideoProcessorBER.Instance.AdicionarContexto(CompanyDb, new VideoProcessorContext() { CompanyDB = CompanyDb, ContextName = ContextName });
        }

        public static VideoProcessorClient GetIdByName(string CompanyDb, string ComputerName)
        {
            return VideoProcessorBER.Instance.GetIdByName(CompanyDb, ComputerName);
        }

        public static VideoProcessorContext GetContextByName(string CompanyDb, string ContextName)
        {
            return VideoProcessorBER.Instance.GetContextIdByName(CompanyDb, ContextName);
        }

        public static List<VideoProcessorJobInfo> GetJobsInfo(string CompanyDb, long status, long context, string clientFileDeleted, string computerName, DateTime startDate, DateTime endDate)
        {
            return VideoProcessorBER.Instance.GetJobsInfoUpdated(CompanyDb, computerName, status, startDate, endDate, context, clientFileDeleted);
        }

        public static VideoProcessorContextList GetContexts(string CompanyDb)
        {
            return VideoProcessorBER.Instance.GetAllContext(CompanyDb);
        }

        public static VideoProcessorStatusList GetStatus(string CompanyDb)
        {
            return VideoProcessorBER.Instance.GetAllStatus(CompanyDb);
        }

        public static VideoProcessorClientList GetComputers(string CompanyDb)
        {
            return VideoProcessorBER.Instance.GetAllNames(CompanyDb);
        }

        public static void UpdateErrorLog(string CompanyDb, string Log, int Jobid)
        {
            VideoProcessorBER.Instance.UpdateErrorLog(CompanyDb, new VideoProcessorJobState() { JobId = Jobid, JobErrorLog = Log });
        }
    }
}
