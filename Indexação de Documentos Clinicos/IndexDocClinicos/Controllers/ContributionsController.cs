using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System.Collections.Generic;
using System.Web.Http;

namespace IndexDocClinicos.Controllers
{
    public class ContributionsController : ApiController
    {

        //private const int rows = 2;

        [HttpGet]
        public IEnumerable<Contribution> GetAllContributions()
        {
            return Query("*:*");
        }

        [HttpGet]
        public IHttpActionResult GetContribution(string id)
        {
            IEnumerable<Contribution> cont = Query(id);
            if (cont == null)
            {
                return NotFound();
            }
            return Ok(cont);
        }

        [NonAction]
        public IEnumerable<Contribution> Query(string text)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            return solr.Query(new SolrQuery(text));
        }

        /*static ISolrQuery Join(string from, string to, string fromIndex, ISolrQuery query) {
            return new LocalParams { { "type", "join" }, { "from", from }, { "to", to }, { "fromIndex", fromIndex } } + query;
        }

        public void something()
        {
            ISolrReadOnlyOperations<TestDocument> solr = ...;
            var results = solr.Query(new SolrQueryByField("MyField0", "ValueA"), new QueryOptions {
                FilterQueries = new[] {
                    Join(from: "OtherID1", to: "MainID", fromIndex: "Index2", query: new SolrQueryByField("MyField1", "ValueB")),
                    Join(from: "OtherID2", to: "MainID", fromIndex: "Index3", query: new SolrQueryByField("MyField2", "ValueC")),
                }
            });
        }*/

        /*[NonAction]
        public IEnumerable<Contribution> QueryPag(string text, int start)//(page-1) * rows + 1
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            return solr.Query(text, new QueryOptions{
                Start = start,
                Rows = rows
            });
        }*/
    }
}
