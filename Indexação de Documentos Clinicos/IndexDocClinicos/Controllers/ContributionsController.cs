using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web.Http;

namespace IndexDocClinicos.Controllers
{
    public class ContributionsController : ApiController
    {

        //private const int rows = 2;

        [HttpGet]
        public List<string> GetAllContributions()
        {
            return Query("*:*");
        }

        [HttpGet]
        public IHttpActionResult GetContribution(string id)
        {
            List<string> cont = Query(id);
            if (cont == null)
            {
                return NotFound();
            }
            return Ok(cont);
        }

        [NonAction]
        public List<string> Query(string text)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            var results = solr.Query(new SolrQuery(text), new QueryOptions
            {
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { "content", "value" },
                }
            });


            CultureInfo ci = new CultureInfo("pt-PT");
            int rIndex = 0;
            List<string> res = new List<string>();
            foreach (var searchResult in results.Highlights)
            {
                StringBuilder searchResults = new StringBuilder();

                searchResults.Append("<div class='panel panel-default'><div class='panel-body'><b>" + results[rIndex].First_name + " " + results[rIndex].Last_name + 
                                        " - " + results[rIndex].Dob.ToString("d MMM yyyy", ci) + "</b><br/><small> ");

                foreach (List<string> val in searchResult.Value.Values)
                {
                    searchResults.Append(string.Format("{0}<br>", string.Join("... ", val.ToArray())));
                }
                searchResults.Append("</small><a href='#' onclick=''>Document</a>&nbsp;&nbsp;<a href='#' onclick=''>Metadata</a></div></div>");
                res.Add(searchResults.ToString());
                rIndex++;
            }

            return res;
        }

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
