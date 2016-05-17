using IndexDocClinicos.Classes;
using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using Oracle.ManagedDataAccess.Client;
using SolrNet;
using SolrNet.Exceptions;
using SolrNet.Impl;
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
            Data data = new Data();
            EhrData ehr_data = new EhrData();
            if(connectionsWork(ehr_data)){
                /*var odds = Enumerable.Range(13706193, 13707193).Where(i => i % 500 != 0);
                Parallel.ForEach(odds, i =>
                {
                    Debug.WriteLine("[" + i + "]");
                    ReadIndexAllData(data, ehr_data, i, 500);
                });*/
                ReadIndexAllData(data, ehr_data, 13706193, 13707193);
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

        private bool connectionsWork(EhrData ehr_data)
        {
            //testing eresults connection
            if (!IsServerConnected()) {
                Debug.WriteLine("Não é possível conectar ao Eresults. Verifique a ligação antes de tentar novamente.");
                return false;
            }

            //testing ehrserver connection
            try {
                Debug.WriteLine("Loging in...");
                ehr_data.login();//login to get token
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

        private void ReadIndexAllData(Data data, EhrData ehr_data, int first, int last)
        {
            Debug.WriteLine("Querying eresults and saving their results...");
            data.queryingEresults(first, last);//querying eresulst to get data

            ehr_data.setPatients(data.getPatients());
            data.clearPatients();

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