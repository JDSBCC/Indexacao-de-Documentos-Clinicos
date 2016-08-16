using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.IO;
using System.Net;
using System.IO.Compression;
using CPCHS.Common.BusinessEntities;
using GLINTTHS.GenericWindowsService.AbstractPeriodicTask;
using System.Xml;
using System.Xml.Serialization;
using CPCHS.GenericWindowsService.Configuration;
using System.Threading;

namespace Glintths.Er.Task
{

    public class DownloadFilesTask : AbstractPeriodicTask
    {
        public bool ExternalDocumentsUseHttps { get; set; }
        public string ExternalDocumentsUri { get; set; }
        public string CompanyDb { get; set; }

        public string ExternalFilesUri { get; set; }
        public bool ExternalFilesUseHttps { get; set; }

        public string LocalEResultsUri { get; set; }
        public bool LocalEResultsUseHttps { get; set; }

        private DownloadFilesTaskConf conf;

        public bool Debug { get; set; }


        static SemaphoreSlim _sem;

        ERConfiguration configDirName, configZipMethod, configMaxNumberJobs, configURLName, configDownloadDate;

        public bool _firstTime = true;

        protected override void Setup()
        {
            try
            {
                base.Setup();
                XmlSerializer serializer = new XmlSerializer(typeof(DownloadFilesTaskConf));
                this.conf = (DownloadFilesTaskConf)serializer.Deserialize(new XmlNodeReader(this.taskConfiguration));


                string file = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"TasksConf.xml");
                FileStream fstream = new FileStream(file, FileMode.Open, FileAccess.Read);
                XmlSerializer s = new XmlSerializer(typeof(TasksConf));
                TasksConf taskConf = (TasksConf)s.Deserialize(fstream);
                fstream.Close();

                this.CompanyDb = taskConf.TaskConfCollection[0].Companies.CompanyConfCollection[0].company;

                this.ExternalDocumentsUri = conf.Configuration.ExternalDocumentsUri;
                this.ExternalFilesUri = conf.Configuration.ExternalFilesUri;
                this.LocalEResultsUri = conf.Configuration.LocalEResultsUri;

                this.ExternalDocumentsUseHttps = (this.ExternalDocumentsUri.ToLower().StartsWith("https"));
                this.ExternalFilesUseHttps = (this.ExternalFilesUri.ToLower().StartsWith("https"));

                this.LocalEResultsUseHttps = (this.LocalEResultsUri.ToLower().StartsWith("https"));

                this.Debug = conf.Configuration.Debug.Equals("S") ? true : false;

            }
            catch (Exception ex)
            {
                writeLog("Erro no Setup: " + ex.Message);

                System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Erro no Setup: " + ex.Message);
            }
        }

        protected override void RunInner()
        {
            try
            {

                if (true == this._firstTime)
                {
                    this._firstTime = false;
                    return;
                }

                //      writeLog("RunInner: Start...");
                if (this.Debug)
                    System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "RunInner: Start...");

                LocalEResultsService.LocalERJob[] jobsListAProcessar = GetJobsByState("PROCESSING");

                GetConfigurations();

                _sem = new SemaphoreSlim(Convert.ToInt32(configMaxNumberJobs.ErConfigValue));
                //       _sem = new SemaphoreSlim(1);

                //   ThreadPool.SetMaxThreads(Convert.ToInt32(configMaxNumberJobs.ErConfigValue), Convert.ToInt32(configMaxNumberJobs.ErConfigValue));


                //Console.WriteLine("jobsListAProcessar.Length: " + jobsListAProcessar.Length);
                if (this.Debug)
                {
                    writeLog("jobsListAProcessar.Length: " + jobsListAProcessar.Length);
                    System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "jobsListAProcessar.Length: " + jobsListAProcessar.Length);

                }
                //Se a quantidade de jobs que estão a ser processados for maior que o numero maximo, tem de esperar
                if (jobsListAProcessar.Length < Convert.ToInt32(configMaxNumberJobs.ErConfigValue))
                {
                    int diff = Convert.ToInt32(configMaxNumberJobs.ErConfigValue) - jobsListAProcessar.Length;

                    //  Console.WriteLine("dif: " + diff);
                    //   writeLog("dif: " + diff);
                    _sem = new SemaphoreSlim(diff);

                    LocalEResultsService.LocalERJob[] jobsList = GetJobsByState("NEW");
                    //   LocalEResultsService.LocalERJob[] jobsErrorList = GetJobsByState("ERROR");

                    // var jobsTotalList = new LocalEResultsService.LocalERJob[jobsList.Length + jobsErrorList.Length];
                    // jobsList.CopyTo(jobsTotalList, 0);
                    // jobsErrorList.CopyTo(jobsTotalList, jobsList.Length);

                    if (this.Debug)
                    {
                        Console.WriteLine("jobsList.Length: " + jobsList.Length);
                        writeLog("jobsList.Length: " + jobsList.Length);
                        System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "jobsList.Length: " + jobsList.Length);
                    }

                    for (int i = 0; i < jobsList.Length; i++)
                    {
                        LocalEResultsService.LocalERJob job = jobsList[i];
                        new Thread(() => ThreadPoolCallback(job)).Start();

                    }
                }

            }
            catch (Exception ex)
            {
                writeLog(ex.Message);
                System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Erro no RunInner:" + ex.Message);
            }
        }

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(Object threadContext)
        {
            LocalEResultsService.LocalERJob job = threadContext as LocalEResultsService.LocalERJob;

            _sem.Wait();

            UpdateJobsState(job.JobId, "PROCESSING");

            if (this.Debug)
            {
                Console.WriteLine(job.JobId + " is in!");
                // writeLog(job.JobId + " is in!");
                System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", job.JobId + " is in!");
            }
            ExtractFiles(job);

            //   Thread.Sleep(2000);


            // Console.WriteLine(job.JobId + " is out!");
            //    writeLog(job.JobId + " is out!");

            _sem.Release();


        }


        private LocalEResultsService.LocalEResultsServiceClient LocalEResultsServiceClient()
        {
            LocalEResultsService.LocalEResultsServiceClient proxy = null;

            Binding binding = null;
            if (this.LocalEResultsUseHttps == true)
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBindingHttps();
            else
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBinding();
            proxy = new LocalEResultsService.LocalEResultsServiceClient(binding, new EndpointAddress(this.LocalEResultsUri));

            return proxy;
        }

        public LocalEResultsService.LocalERJob[] GetJobsByState(string state)
        {
            LocalEResultsService.LocalEResultsServiceClient proxy = LocalEResultsServiceClient();


            LocalEResultsService.SessionContext sessionContext = new LocalEResultsService.SessionContext();
            sessionContext.CompanyName = companyDB;

            LocalEResultsService.LocalERJob[] jobs = proxy.GetJobsByState(sessionContext, state);

            return jobs;
        }

        public void UpdateJobsState(long jobid, string state)
        {
            LocalEResultsService.LocalEResultsServiceClient proxy = LocalEResultsServiceClient();

            LocalEResultsService.SessionContext sessionContext = new LocalEResultsService.SessionContext();
            sessionContext.CompanyName = companyDB;

            proxy.SetJobState(sessionContext, jobid, state, sessionContext.UserName);
        }

        public void SetJobUrl(long jobid, string url)
        {
            LocalEResultsService.LocalEResultsServiceClient proxy = LocalEResultsServiceClient();

            LocalEResultsService.SessionContext sessionContext = new LocalEResultsService.SessionContext();
            sessionContext.CompanyName = companyDB;

            proxy.SetJobUrl(sessionContext, jobid, url, sessionContext.UserName);
        }

        public void SetMsgError(long jobid, string msgError)
        {
            LocalEResultsService.LocalEResultsServiceClient proxy = LocalEResultsServiceClient();

            LocalEResultsService.SessionContext sessionContext = new LocalEResultsService.SessionContext();
            sessionContext.CompanyName = companyDB;

            proxy.SetJobMsgError(sessionContext, jobid, msgError);
        }

        private DocumentsManagementWs.DocumentsManagementSCClient GetDocumentsManagementClient()
        {
            DocumentsManagementWs.DocumentsManagementSCClient proxy = null;

            Binding binding = null;
            if (this.ExternalDocumentsUseHttps == true)
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBindingHttps();
            else
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBinding();
            proxy = new DocumentsManagementWs.DocumentsManagementSCClient(binding, new EndpointAddress(this.ExternalDocumentsUri));

            return proxy;
        }

        public DocumentsManagementWs.DocumentsList GetDocumentsList(string appId, string docType, string entityId, DateTime? dt_ini, DateTime? dt_fim)
        {
            DocumentsManagementWs.DocumentsManagementSCClient proxy = GetDocumentsManagementClient();

            DocumentsManagementWs.DocumentsList docs = new DocumentsManagementWs.DocumentsList();

            DocumentsManagementWs.PaginationResponse paginationResponse = null;

            try
            {
                switch (configDownloadDate.ErConfigValue)
                {
                    case "DATA_EXECUCAO":
                        docs = proxy.GetGeneralDocuments(appId, null, CompanyDb, null, null, docType, null, null, entityId, null, null, null, null, null,
                                                 dt_fim, dt_ini, null, null, null, null, null, null, null, null, null, null, null, null,
                                                 null, null, null, null, null, out paginationResponse);
                        break;
                    case "DATA_VALIDACAO":
                        docs = proxy.GetGeneralDocuments(appId, null, CompanyDb, null, null, docType, null, null, entityId, null, null, null, null, null,
                                                null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                                null, null, dt_fim, dt_ini, null, out paginationResponse);
                        break;
                    case "DATA_EMISSAO":
                        docs = proxy.GetGeneralDocuments(appId, null, CompanyDb, null, null, docType, dt_fim, dt_ini, entityId, null, null, null, null, null,
                                                null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                                                null, null, null, null, null, out paginationResponse);
                        break;
                    default:
                        break;
                }
                proxy.Close();
            }
            catch (Exception ex)
            {
                proxy.Abort();
                throw ex;
            }
            finally
            {
                proxy.Abort();
            }

            return docs;
        }


        private FilesManagementWS.FilesManagementSCClient GetFilesManagementClient()
        {
            FilesManagementWS.FilesManagementSCClient proxy = null;

            Binding binding = null;
            if (this.ExternalFilesUseHttps == true)
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBindingHttps();
            else
                binding = Cpchs.Security.Providers.Utilities.ProxyInitializer.GetDefaultBinding();
            proxy = new FilesManagementWS.FilesManagementSCClient(binding, new EndpointAddress(this.ExternalFilesUri));

            return proxy;
        }

        public FilesManagementWS.FileInfoList GetDocumentFiles(long docId)
        {

            FilesManagementWS.FilesManagementSCClient proxy = GetFilesManagementClient();
            FilesManagementWS.FileInfoList filesList = new FilesManagementWS.FileInfoList();
            try
            {
                filesList = proxy.GetDocumentFiles(CompanyDb, docId);

                proxy.Close();
            }
            catch (Exception ex)
            {
                proxy.Abort();
                throw ex;
            }
            finally
            {
                proxy.Abort();
            }

            return filesList;
        }


        private void GetConfigurations()
        {
            #region Configuracoes

            configDirName = EntityManagementBER.Instance.GetConfigurationByScopeKey(CompanyDb, "FILES_FOLDER_PATH", "DOWNLOAD_FILE");
            configZipMethod = EntityManagementBER.Instance.GetConfigurationByScopeKey(CompanyDb, "DOWNLOAD_METHOD_ZIP", "DOWNLOAD_FILE");
            configMaxNumberJobs = EntityManagementBER.Instance.GetConfigurationByScopeKey(CompanyDb, "MAX_JOBS", "DOWNLOAD_FILE");
            configURLName = EntityManagementBER.Instance.GetConfigurationByScopeKey(CompanyDb, "FILES_HTTP_PATH", "DOWNLOAD_FILE");
            configDownloadDate = EntityManagementBER.Instance.GetConfigurationByScopeKey(CompanyDb, "DOWNLOAD_DATE", "DOWNLOAD_FILE");


            #endregion

            //Verificar se a pasta base já foi criada (se ainda nao existir, criar)
            if (!Directory.Exists(configDirName.ErConfigValue) && configDirName != null)
            {
                Directory.CreateDirectory(configDirName.ErConfigValue);
                Console.WriteLine("Pasta base criada!");
                writeLog("Pasta base criada");

            }

        }
        //private void ExtractFiles(int jobId, string appId, string docType, string patientId, string patientType, string username)
        private void ExtractFiles(LocalEResultsService.LocalERJob job)
        {
            //Criar pasta para o job
            string jobdirGuid = Guid.NewGuid().ToString();
            string jobDirName = Path.Combine(configDirName.ErConfigValue, jobdirGuid);

            try
            {

                Directory.CreateDirectory(jobDirName);
                //   Console.WriteLine("Pasta para o job " + job.JobId + " criada: " + jobDirName);

                if (this.Debug)
                {
                    writeLog("Job " + job.JobId + " está a ser processado...");
                    writeLog("Pasta para o job " + job.JobId + " criada: " + jobDirName);
                    System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Job " + job.JobId + " está a ser processado...");
                    System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Pasta para o job " + job.JobId + " criada: " + jobDirName);
                }
                DocumentsManagementWs.DocumentsList docs = new DocumentsManagementWs.DocumentsList();

                //Ir buscar a lista de documentos associados ao job
                foreach (LocalEResultsService.LocalERJobFilter jobFilter in job.JobFilter)
                {
                    string[] filter = jobFilter.Filter.Split('|');

                    //     docs = GetDocumentsList(filter[0], filter[1], job.PatientId, job.PatientType);

                    docs = GetDocumentsList(filter[0], filter[1], job.Entity.EntidadeId.ToString(), job.Dt_ini, job.Dt_fim);


                    // Console.WriteLine("JOB " + job.JobId + ": " + filter[0] + " ; " + filter[1] + " ; " + job.PatientId);

                    if (this.Debug)
                    {
                        writeLog("JOB " + job.JobId + ": " + filter[0] + " ; " + filter[1] + " ; " + job.PatientId);
                        System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "JOB " + job.JobId + ": " + filter[0] + " ; " + filter[1] + " ; " + job.PatientId);

                    }

                    //Para cada documento da lista, chamar o serviço Cpchs.Documents.Web.DataPresenter/FileViewer.aspx, que faz o download do documento
                    foreach (DocumentsManagementWs.Document doc in docs.Documents)
                    {
                        FilesManagementWS.FileInfoList documentFiles = new FilesManagementWS.FileInfoList();
                        documentFiles = GetDocumentFiles(doc.UniqueId);

                        if (this.Debug) writeLog("Existem " + documentFiles.FilesInfo.Count + " ficheiros");

                        //Guardar o documento na pasta do job
                        if (documentFiles != null && documentFiles.FilesInfo != null && documentFiles.FilesInfo.Count != 0)
                        {
                            foreach (var v in documentFiles.FilesInfo)
                            {
                                StringBuilder urlBldr = new StringBuilder();
                                urlBldr.Append(v.FileBaseUrl);
                                if (!v.FileBaseUrl.Contains("?"))
                                    urlBldr.Append("?");
                                urlBldr.Append(v.FileQueryUrl);

                                string fileUrl = urlBldr.ToString();
                                string fileName = jobDirName + "\\" + v.FileElemId + v.FileExtension;

                                if (!System.IO.File.Exists(fileName))
                                {
                                    WebClient wc = new WebClient();
                                    wc.DownloadFile(fileUrl, fileName);
                                    //   wc.DownloadFileTaskAsync(fileUrl, fileName);
                                }
                            }
                        }
                    }

                }

                string urlfolder = string.Empty;
                //retornar o link da pasta (pode ser zipada ou não)
                if (configZipMethod.ErConfigValue == "N")
                {
                    urlfolder = jobDirName + "\\";
                    jobdirGuid += "//";
                }
                else
                {

                    urlfolder = jobDirName + ".zip";
                    jobdirGuid += ".zip";

                    ZipFile.CreateFromDirectory(jobDirName, urlfolder);
                    //Apagar pasta original
                    Directory.Delete(jobDirName, true);
                }

                if (!String.IsNullOrEmpty(configURLName.ErConfigValue))
                    job.Url = configURLName.ErConfigValue + jobdirGuid;
                else
                    job.Url = urlfolder; // guardar link da pasta dos ficheiros
                SetJobUrl(job.JobId, job.Url);

                // PARA TESTE APENAS
                //    Thread.Sleep(2000); 

                //Chamar serviço que faz update ao estado do job
                UpdateJobsState(job.JobId, "PROCESSED");

                if (this.Debug)
                {
                    writeLog("Job " + job.JobId + " foi processado.");
                    System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Job " + job.JobId + " foi processado, pasta: " + urlfolder);
                    //Console.WriteLine("Link final para o job " + job.JobId + " ->" + urlfolder);
                    writeLog("Link final para o job " + job.JobId + " ->" + urlfolder);
                }

            }
            catch (Exception ex)
            {
                //No caso de algum job for mal processado
                UpdateJobsState(job.JobId, "ERROR");

                string msg = ex.Message.Length >= 4000 ? ex.Message.Substring(0, 3500) : ex.Message;

                SetMsgError(job.JobId, msg);

                //Update er_job set msg_erro = ex.message
                //  Console.WriteLine("Ocorreu um erro ao processar o job " + job.JobId + " : " + ex.Message);
                writeLog("Ocorreu um erro ao processar o job " + job.JobId + " : " + ex.Message);



                System.Diagnostics.EventLog.WriteEntry("DownloadFilesTask", "Ocorreu um erro ao processar o job " + job.JobId + " : " + ex.Message);

                if (Directory.Exists(jobDirName))
                    Directory.Delete(jobDirName, true);
            }
        }


        public static void writeLog(string msg)
        {
            try
            {
                System.IO.FileStream file = new System.IO.FileStream(Environment.CurrentDirectory + @"\log.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(file);

                sw.WriteLine("[" + System.DateTime.Now + "]" + msg);

                sw.Close();
                file.Close();
            }
            catch { }
        }

    }
}
