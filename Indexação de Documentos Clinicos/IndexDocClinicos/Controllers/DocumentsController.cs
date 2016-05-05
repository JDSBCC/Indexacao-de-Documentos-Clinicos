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

        public string  GetFile(string id)
        {
            string[] elems = id.Split('_');

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            var results = solr.Query(new SolrQueryByField("elemento_id", elems[0]) && new SolrQueryByField("cod_versao", elems[1]), new QueryOptions
            {
                Fields = new[] { "file_stream" }
            });

            Debug.WriteLine(results[0].File_Stream);
            return results[0].File_Stream;
        }
    }
}