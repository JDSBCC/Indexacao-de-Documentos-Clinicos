using IndexDocClinicos.Classes;
using IndexDocClinicos.Models;
using SolrNet;
using SolrNet.Impl;
using System.Web.Http;

namespace IndexDocClinicos
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var connection = new SolrConnection("http://localhost:8983/solr/ehr");
            Startup.Init<Contribution>(connection);

            Data data = new Data();
        }
    }
}
