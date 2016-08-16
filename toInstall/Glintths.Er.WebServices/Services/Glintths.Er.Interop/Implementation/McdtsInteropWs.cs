using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Glintths.Er.Interop.MessageContracts;

namespace Glintths.Er.Interop.ServiceImplementation
{  
    public partial class McdtsInteropWs
    {
        //private static Dictionary<int, object> cache = new Dictionary<int, object>();

        #region Getters

        //public static Dictionary<int, object> GetCache()
        //{
        //    return cache;
        //}

        public override GetJobInfoResponse GetJobInfo(GetJobInfoRequest request)
        {
            try
            {
                GetJobInfoResponse response = new GetJobInfoResponse();

                List<VideoProcessorJobInfo> jobs = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetJobsInfo(request.CompanyDb, request.Status, request.Context, request.ClientFileEliminated, request.ComputerName, request.StartDate, request.EndDate);

                Glintths.Er.Interop.DataContracts.JobsInfo Jobs = new Glintths.Er.Interop.DataContracts.JobsInfo();

                foreach (VideoProcessorJobInfo j in jobs)
                {
                    Jobs.Add(TranslateBetweenVideoProcessorJobInfoAndJobInfo.TranslateVideoProcessorJobInfoToJobInfo(j));
                }

                response.McdtsInteropJobsInfoDataContract = new Glintths.Er.Interop.DataContracts.JobInfoList();
                response.McdtsInteropJobsInfoDataContract.JobsInfo = Jobs;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override GetAllContextsResponse GetAllContexts(GetAllContextsRequest request)
        {
            try
            {
                GetAllContextsResponse response = new GetAllContextsResponse();

                VideoProcessorContextList contexts = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetContexts(request.CompanyDb);

                Glintths.Er.Interop.DataContracts.Contexts Contexts = new Glintths.Er.Interop.DataContracts.Contexts();

                foreach (VideoProcessorContext j in contexts.Items)
                {
                    Contexts.Add(TranslateBetweenVideoProcessorContextAndContext.TranslateVideoProcessorContextToContext(j));
                }

                response.McdtsInteropContextsDataContract = new Glintths.Er.Interop.DataContracts.ContextList();
                response.McdtsInteropContextsDataContract.Contexts = Contexts;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override GetAllComputersResponse GetAllComputers(GetAllComputersRequest request)
        {
            try
            {
                GetAllComputersResponse response = new GetAllComputersResponse();

                VideoProcessorClientList clients = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetComputers(request.CompanyDb);

                Glintths.Er.Interop.DataContracts.Computers Computers = new Glintths.Er.Interop.DataContracts.Computers();

                foreach (VideoProcessorClient j in clients.Items)
                {
                    Computers.Add(TranslateBetweenComputerAndVideoProcessorClient.TranslateVideoProcessorClientToComputer(j));
                }

                response.McdtsInteropComputersDataContract = new Glintths.Er.Interop.DataContracts.ComputersList();
                response.McdtsInteropComputersDataContract.Computers = Computers;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override GetAllStatusResponse GetAllStatus(GetAllStatusRequest request)
        {
            try
            {
                GetAllStatusResponse response = new GetAllStatusResponse();

                VideoProcessorStatusList status = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetStatus(request.CompanyDb);

                Glintths.Er.Interop.DataContracts.StatusC Status = new Glintths.Er.Interop.DataContracts.StatusC();

                foreach (VideoProcessorStatus s in status.Items)
                {
                    Status.Add(TranslateBetweenVideoProcessorStatusAndStatus.TranslateVideoProcessorStatusToStatus(s));
                }

                response.McdtsInteropStatusDataContract = new Glintths.Er.Interop.DataContracts.StatusList();
                response.McdtsInteropStatusDataContract.StatusC = Status;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override GetWaitingJobsResponse GetWaitingJobs(GetWaitingJobsRequest request)
        {
            try
            {
                GetWaitingJobsResponse response = new GetWaitingJobsResponse();

                VideoProcessorJobStateList jobs = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetWaitingJobs(request.CompanyDb, request.Status);

                Glintths.Er.Interop.DataContracts.Jobs Jobs = new Glintths.Er.Interop.DataContracts.Jobs();

                foreach (VideoProcessorJobState j in jobs.Items)
                {
                    Jobs.Add(TranslateBetweenVideoProcessorJobStateAndJob.TranslateVideoProcessorJobStateToJob(j));
                }

                response.McdtsInteropJobsDataContract = new Glintths.Er.Interop.DataContracts.JobList();
                response.McdtsInteropJobsDataContract.Jobs = Jobs;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }        

        public override GetIdByNameResponse GetIdByName(GetIdByNameRequest request)
        {
            GetIdByNameResponse response = new GetIdByNameResponse();

            VideoProcessorClient client = null;
            client = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetIdByName(request.CompanyDb, request.ComputerName);

            response.McdtsInteropComputerDataContract = new Glintths.Er.Interop.DataContracts.Computer();

            if (client == null)
                response.McdtsInteropComputerDataContract.ComputerId = 0;
            else
                response.McdtsInteropComputerDataContract.ComputerId = client.ComputerId;

            return response;
        }

        public override GetContextByNameResponse GetContextByName(GetContextByNameRequest request)
        {
            GetContextByNameResponse response = new GetContextByNameResponse();

            VideoProcessorContext context = null;
            context = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.GetContextByName(request.CompanyDb, request.ContextName);

            response.McdtsInteropContextDataContract = new Glintths.Er.Interop.DataContracts.Context();

            if (context == null)
                response.McdtsInteropContextDataContract.ContextId = 0;
            else
                response.McdtsInteropContextDataContract.ContextId = context.ContextId;

            return response;
        }

        #endregion

        #region Registers

        public override ComputerRegisterResponse ComputerRegister(ComputerRegisterRequest request)
        {
            try
            {
                ComputerRegisterResponse response = new ComputerRegisterResponse();

                VideoProcessorClient computer = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.ComputerRegister(request.CompanyDb, request.ComputerName);

                response.ComputerId = (int)computer.ComputerId;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override ContestRegisterResponse ContextRegister(ContextRegisterRequest request)
        {
            try
            {
                ContestRegisterResponse response = new ContestRegisterResponse();

                VideoProcessorContext context = Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.AdicionarContexto(request.CompanyDb, request.ContextName);

                response.ContextId = (int)context.ContextId;

                return response;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public override void JobRegister(JobRegisterRequest request)
        {
            try
            {
                int machine = (int)GetIdByName(new GetIdByNameRequest() { CompanyDb = request.CompanyDb, ComputerName = request.ComputerName }).McdtsInteropComputerDataContract.ComputerId;
                int context = 0;
                context = (int)GetContextByName(new GetContextByNameRequest() { CompanyDb = request.CompanyDb, ContextName = request.Context }).McdtsInteropContextDataContract.ContextId;

                Cpchs.ER2Indexer.WCF.BusinessEntities.Dados d = 
                    Cpchs.ER2Indexer.WCF.BusinessEntities.Dados.Deserialize(request.XmlData);
                string coisas = d.Serialize();

                if (machine.Equals(0))
                    machine = (int)ComputerRegister(new ComputerRegisterRequest() { CompanyDb = request.CompanyDb, ComputerName = request.ComputerName }).ComputerId;

                if (context.Equals(0) && request.Context != string.Empty && request.Context != null)
                    context = (int)ContextRegister(new ContextRegisterRequest() { CompanyDb = request.CompanyDb, ContextName = request.Context }).ContextId;
                else context = 1;

                Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.JobRegister(request.CompanyDb, machine, 1, DateTime.Now, DateTime.Now, 0, context, "Parado", request.JobUrl, "N", request.XmlData, request.PatientData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Updates

        public override void UpdateJobStatus(UpdateJobStatusRequest request)
        {
            Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.UpdateJobStatus(request.CompanyDb, (int)request.JobId, request.JobStatus, DateTime.Now);
        }

        public override void UpdateJobPercentage(UpdateJobPercentageRequest request)
        {
            Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.UpdateJobPercentage(request.CompanyDb, (int)request.JobId, (int)request.Percentage);
        }

        public override void UpdateJobErrorLog(UpdateJobErrorLogRequest request)
        {
            String XmlizedString = SerializeLog(request.ErrorLog);
            Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.UpdateErrorLog(request.CompanyDb, XmlizedString, (int)request.JobId);
        }

        public override void UpdateJobClob(UpdateJobClobRequest request)
        {
            try
            {
                object temp = VideoProcessorBER.Instance.GetJobClob(request.CompanyDb, request.JobId);
                Cpchs.ER2Indexer.WCF.BusinessEntities.Dados data = 
                    Cpchs.ER2Indexer.WCF.BusinessEntities.Dados.Deserialize((string)temp);

                (data.Documento.Elemento[0].Item as Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideo).Duracao = request.Duration;
                (data.Documento.Elemento[0].Item as Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideo).VideoLink[0].Link = request.Link;
                (data.Documento.Elemento[0].Item as Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideo).UrlPreviewImg = request.LinkImage;

                Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideoVideoLink vl = 
                    new Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideoVideoLink();
                vl.Tamanho = request.Size;
                vl.Tipo = request.Type;
                vl.ResolucaoH = request.ResolutionW;
                vl.ResolucaoV = request.ResolutionH;
                request.Link = request.Link.ToLower();
                vl.Link = request.Link.Substring(0, request.Link.LastIndexOf("wmv"));
                vl.Link += "ism/manifest";
                (data.Documento.Elemento[0].Item as Cpchs.ER2Indexer.WCF.BusinessEntities.DadosDocumentoElementoVideo).VideoLink.Add(vl);

                String XmlizedString = data.Serialize();
                Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.UpdateJobClob(request.CompanyDb, (int)request.JobId, XmlizedString);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public override void UpdateJobFileStatus(UpdateJobClientFileRequest request)
        {
            Glintths.Er.Interop.BusinessLogic.VideoProcessor.McdtsInteropLogic.UpdateJobFileStatus(request.CompanyDb, request.JobId, request.DeletedStatus);
        }

        #endregion

        #region Serialization

        //internal static Object DeserializeObject(String pXmlizedString)
        //{
        //    XmlSerializer xs = new XmlSerializer(typeof(Dados));
        //    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));

        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.OmitXmlDeclaration = true;
        //    settings.Encoding = new UTF8Encoding(false);
        //    settings.Indent = true;

        //    XmlWriter xmlTextWriter = XmlWriter.Create(memoryStream, settings);

        //    return xs.Deserialize(memoryStream);
        //}

        //private static Byte[] StringToUTF8ByteArray(String pXmlString)
        //{
        //    UTF8Encoding encoding = new UTF8Encoding(false);
        //    Byte[] byteArray = encoding.GetBytes(pXmlString);
        //    return byteArray;
        //}

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding(false);
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        //private static String SerializeObject(Object pObject)
        //{
        //    try
        //    {
        //        XmlSerializer xs = new XmlSerializer(typeof(Dados));
        //        MemoryStream memoryStream = new MemoryStream();

        //        XmlWriterSettings settings = new XmlWriterSettings();
        //        settings.OmitXmlDeclaration = true;
        //        settings.Encoding = new UTF8Encoding(false);
        //        settings.Indent = true;

        //        XmlWriter xmlTextWriter = XmlWriter.Create(memoryStream, settings);

        //        xs.Serialize(xmlTextWriter, pObject);
        //        return UTF8ByteArrayToString(memoryStream.ToArray());
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        private static String SerializeLog(Object pObject)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Glintths.Er.Interop.DataContracts.JobErrorLog));
                MemoryStream memoryStream = new MemoryStream();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Encoding = new UTF8Encoding(false);
                settings.Indent = true;

                XmlWriter xmlTextWriter = XmlWriter.Create(memoryStream, settings);

                xs.Serialize(xmlTextWriter, pObject);
                return UTF8ByteArrayToString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion
    }
}