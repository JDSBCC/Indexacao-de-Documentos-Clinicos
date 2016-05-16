using IndexDocClinicos.Classes;
using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Impl;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
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
            ReadIndexAllData();
            stopwatch.Stop();//REMOVE
            Debug.WriteLine("[TIME] = " + stopwatch.ElapsedMilliseconds);//REMOVE

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ReadIndexAllData()
        {
            Data data = new Data();
            EhrData ehr_data = new EhrData();

            //testing connection with eresults
            if (!IsServerConnected()) {
                Debug.WriteLine("Não é possível conectar ao Eresults. Verifique a ligação antes de tentar novamente.");
                return;
            }

            //testing connection with ehrserver
            try
            {
                Debug.WriteLine("Loging in...");
                ehr_data.login();//login to get token
            }catch(WebException){
                Debug.WriteLine("Não é possível conectar ao EHRserver. Verifique a ligação antes de tentar novamente.");
                return;
            }

            Debug.WriteLine("Connecting with solr...");
            var connection = new SolrConnection(ConfigurationManager.AppSettings["SolrCore"]);//connect to solrcore
            Startup.Init<Contribution>(connection);
            ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();

            Debug.WriteLine("Querying eresults and saving their results...");
            data.queryingEresults();//querying eresulst to get data

            ehr_data.setPatients(data.getPatients());

            Debug.WriteLine("Committing patients to ehr...");
            ehr_data.commitPersonsPatients();//commit persons to ehr

            Debug.WriteLine("Initializing information to fill xml...");
            ehr_data.fillData();//create a string with file information (xml with data)

            Debug.WriteLine("Filling xml...");
            ehr_data.commitDocument();//commit xml in ehr

            Debug.WriteLine("Indexing data in solr...");
            data.addToSolr();//indexing ehr data in solr
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