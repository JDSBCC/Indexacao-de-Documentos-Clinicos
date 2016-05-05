using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Http;

namespace IndexDocClinicos.Controllers
{
    public class ContributionsController : ApiController
    {

        //private const int rows = 2;

        public List<Dictionary<string, string>> GetAllContributions()
        {
            return Query("*:*");
        }

        public List<Dictionary<string, string>> GetContribution(string id)
        {
            return Query(id);
        }

        public List<Dictionary<string, string>> Query(string text)
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
            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            foreach (var searchResult in results.Highlights)
            {
                StringBuilder searchResults = new StringBuilder();

                foreach (List<string> val in searchResult.Value.Values)
                {
                    searchResults.Append(string.Format("{0}<br>", string.Join("... ", val.ToArray())));
                }
                res.Add(new Dictionary<string, string>());
                res[res.Count - 1].Add("elemento_id", results[rIndex].Elemento_id+"");
                res[res.Count - 1].Add("cod_versao", results[rIndex].Cod_Versao+"");
                res[res.Count - 1].Add("first_name", results[rIndex].First_name);
                res[res.Count - 1].Add("last_name", results[rIndex].Last_name);
                res[res.Count - 1].Add("dob", results[rIndex].Dob.ToString("d MMM yyyy", ci));
                res[res.Count - 1].Add("text", searchResults.ToString());

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