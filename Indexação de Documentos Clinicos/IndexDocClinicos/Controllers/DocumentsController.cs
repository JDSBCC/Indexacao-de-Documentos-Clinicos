using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Diagnostics;
using System.Web.Http;

namespace IndexDocClinicos.Controllers
{
    public class DocumentsController : ApiController
    {

        public string GetFile(string id)
        {
            string[] elems = id.Split(' ');
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            var results = solr.Query(new SolrQueryByField("element_id", elems[0]));//UPDATE da erro

            Debug.WriteLine(results[0].Elemento_id);
            Debug.WriteLine(results[0].Cod_Versao);
            Debug.WriteLine(results[0].File_Stream);

            return "";
        }
    }
}