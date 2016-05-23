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
using System.Linq;
using System.Net;
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
        protected void Application_Start()
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //REMOVE
            //----------------------------------------
            List<Task> tasks = new List<Task>();
            int chunckSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);
            if(connectionsWork()){
                for (int i = 13706193; i < 13707193; i += chunckSize)
                {
                    int index = i;
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        ReadIndexAllData(index, index + chunckSize-1);
                    }));
                    //ReadIndexAllData(i, i + 999);
                }
                Task.WaitAll(tasks.ToArray<Task>());
                //ReadIndexAllData(13706193, 13716193);
            }
            //----------------------------------------
            stopwatch.Stop();//REMOVE
            Debug.WriteLine("[TIME] = " + stopwatch.ElapsedMilliseconds);//REMOVE

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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

        private void ReadIndexAllData(int first, int last)
        {
            Data data = new Data();
            EhrData ehr_data = new EhrData();

            Debug.WriteLine("[" + first + "] - Querying eresults and saving their results...");
            data.queryingEresults(first, last);//querying eresulst to get data

            Debug.WriteLine("[" + first + "] - Adding patients to ehr_Data...");
            ehr_data.setPatients(data.getPatients());

            Debug.WriteLine("[" + first + "] - Committing patients to ehr...");
            ehr_data.commitPersonsPatients();//commit persons to ehr

            Debug.WriteLine("[" + first + "] - Initializing information to fill xml...");
            ehr_data.fillData();//create a string with file information (xml with data)

            Debug.WriteLine("[" + first + "] - Filling xml...");
            ehr_data.commitDocument();//commit xml in ehr

            data.setNumContQuery(ehr_data.getPatientUids());

            Debug.WriteLine("[" + first + "] - Indexing data in solr...");
            data.commitDataSolr();//indexing ehr data in solr

            ehr_data.freeMemory();
            data.freeMemory();
        }


        public bool IsServerConnected()
        {
            using (var conn = new OracleConnection(ConfigurationManager.AppSettings["Eresults_v2_db"]))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (OracleException)
                {
                    return false;
                }
            }
        }
    }
}