using IndexDocClinicos.Classes;
using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Exceptions;
using SolrNet.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IndexDocClinicos
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private DateTime lastUpdate;

        protected void Application_Start()
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //REMOVE
            init();
            stopwatch.Stop();//REMOVE
            Debug.WriteLine("[TIME] = " + stopwatch.ElapsedMilliseconds);//REMOVE


            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void init()
        {
            lastUpdate = DateTime.Now;//update date

            List<Task> tasks = new List<Task>();

            int chunckSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);

            //init connections to databases
            Connection.initMySQL();
            Connection.initOracle();

            if (connectionsWork())
            {
                int[] id = getMinMaxDocumentId();
                for (int i = id[0]; i < id[1]; i += chunckSize)
                {
                    int index = i;
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        ReadIndexAllData("d.documento_id>=" + index + " AND d.documento_id<=" + (index + chunckSize - 1));
                    }));
                    //ReadIndexAllData("d.documento_id>=" + i + " AND d.documento_id<=" + (i + chunckSize - 1));
                }
                Task.WaitAll(tasks.ToArray());
                //ReadIndexAllData("d.documento_id>="+id[0]+" AND d.documento_id<="+id[1]);
                runUpdateThread();
            }
        }

        private bool connectionsWork()
        {
            //testing eresults connection
            if (!IsServerConnected()) {
                Debug.WriteLine("Não é possível conectar ao Eresults. Verifique a ligação antes de tentar novamente.");
                return false;
            }

            //testing ehrserver connection
            try {
                Debug.WriteLine("Loging in...");
                EhrData ehr_Data = new EhrData();
                ehr_Data.login();//login to get token
                ehr_Data.setOrganization();//just save the only organization that exists
            } catch (WebException) {
                Debug.WriteLine("Não é possível conectar ao EHRserver. Verifique a ligação antes de tentar novamente.");
                return false;
            }

            //testing solr connection
            Debug.WriteLine("Connecting with solr...");
            var connection = new SolrConnection(ConfigurationManager.AppSettings["SolrCore"]);//connect to solrcore
            Startup.Init<Contribution>(connection);
            try {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
                solr.Ping();
                solr.Delete(SolrQuery.All);//REMOVE
            } catch (SolrConnectionException) {
                Debug.WriteLine("Não é possível conectar ao SolrAdmin. Verifique a ligação antes de tentar novamente.");
                return false;
            }
            return true;
        }

        private void ReadIndexAllData(string condition)
        {

            Data data = new Data();
            EhrData ehr_data = new EhrData();

            Debug.WriteLine("Querying eresults and saving their results...");
            data.queryingEresults(condition);//querying eresulst to get data

            List<Patient> patients = data.getPatients();
            if (patients.Count > 0) {
                Debug.WriteLine("Adding patients to ehr_Data...");
                ehr_data.setPatients(patients);

                Debug.WriteLine("Committing patients to ehr...");
                ehr_data.commitPersonsPatients();//commit persons to ehr

                Debug.WriteLine("Initializing information to fill xml...");
                ehr_data.fillData();//create a string with file information (xml with data)

                Debug.WriteLine("Filling xml...");
                ehr_data.commitDocument();//commit xml in ehr

                data.setNumContQuery(ehr_data.getPatientUids());

                Debug.WriteLine("Indexing data in solr...");
                data.commitDataSolr();//indexing ehr data in solr
            }

            ehr_data.freeMemory();
            data.freeMemory();
        }


        public bool IsServerConnected()
        {
            bool connected = false;
            try {
                Connection.openOracle();
                connected = true;
            } catch (OracleException) {
            } finally {
                Connection.closeOracle();
            }
            return connected;
        }

        private void runUpdateThread() {
            Debug.WriteLine("Initializing update thread");
            Task.Factory.StartNew(() => {

                while(true){
                    //1200000-20min
                    Thread.Sleep(5000);//tempo entre updates

                    List<List<int>> ids = new List<List<int>>();
                    getUpdatedDocuments(ids);

                    if (ids.Count > 0) {
                        string conditions = "(e.elemento_id = " + ids[0][1] + " AND e.documento_id = " + ids[0][0] + ")";
                        foreach (var id in ids)
                        {
                            conditions += " OR (e.elemento_id = " + id[1] + " AND e.documento_id = " + id[0] + ")";
                        }

                        ReadIndexAllData(conditions);
                        lastUpdate = DateTime.Now;
                    }
                }
            });
        }

        private void getUpdatedDocuments(List<List<int>> ids)
        {
            ids.Clear();
            OracleConnection connOracle = null;
            try
            {
                connOracle = new OracleConnection();
                connOracle.ConnectionString = ConfigurationManager.AppSettings["Eresults_v2_db"];
                connOracle.Open();
                OracleCommand cmd = new OracleCommand("select documento_id, elemento_id from " +//UPDATE colocar a data real
                                        "(select documento_id, elemento_id, NVL(DT_ACT, DT_CRI) as final_date from er_elemento) " +
                                        "where final_date>to_date('" + lastUpdate.ToString("yyyyMMddHHmmss") + "', 'YYYYMMDDHHMISS')", connOracle);//to_date('20121009010000', 'YYYYMMDDHHMISS')

                OracleDataReader dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    ids.Add(new List<int>());
                    ids[ids.Count - 1].Add(Convert.ToInt32(dataReaderOracle["documento_id"]));
                    ids[ids.Count - 1].Add(Convert.ToInt32(dataReaderOracle["elemento_id"]));
                }
                dataReaderOracle.Close();
            }
            catch (OracleException) {
                if (connOracle != null) {
                    connOracle.Close();
                }
                getUpdatedDocuments(ids);
            }
            catch (TimeoutException) {
                if (connOracle != null) {
                    connOracle.Close();
                }
                getUpdatedDocuments(ids);
            }
            finally {
                if (connOracle != null) {
                    connOracle.Close();
                }
            }
        }

        private int [] getMinMaxDocumentId()
        {
            int []id = new int[2];
            try
            {
                Connection.openOracle();
                OracleCommand cmd = new OracleCommand("SELECT MIN(documento_id) as min_id, MAX(documento_id) as max_id from er_documento", Connection.getOracleCon());

                OracleDataReader dataReaderOracle = cmd.ExecuteReader();
                while (dataReaderOracle.Read())
                {
                    id[0] = Convert.ToInt32(dataReaderOracle["min_id"]);
                    id[1] = Convert.ToInt32(dataReaderOracle["max_id"]);
                }
                dataReaderOracle.Close();
            } catch (OracleException) { }
            finally {
                Connection.closeOracle();
            }
            return id;
        }
    }
}