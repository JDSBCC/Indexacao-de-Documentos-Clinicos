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

        public List<Dictionary<string, string>> GetContributions(int startPage, int rows)
        {
            return Query(SolrQuery.All, startPage, rows);
        }

        public List<Dictionary<string, string>> GetContributions(string id, int startPage, int rows)
        {
            return Query(/*new SolrQueryByField("content", id) || new SolrQueryByField("value", id)*/ new SolrQuery(id), startPage, rows);
        }

        public List<Dictionary<string, string>> Query(ISolrQuery query, int startPage, int rows)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            var results = solr.Query(query, new QueryOptions
            {
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { "content", "value" },
                },
                Rows = rows,
                Start = startPage
            });

            return FormatData(results);
        }

        public List<Dictionary<string, string>> FormatData(SolrQueryResults<Contribution> results)
        {
            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            CultureInfo ci = new CultureInfo("pt-PT");
            int rIndex = 0;
            foreach (var searchResult in results.Highlights)
            {
                StringBuilder searchResults = new StringBuilder();

                foreach (List<string> val in searchResult.Value.Values)
                {
                    searchResults.Append(string.Format("{0}<br>", string.Join("... ", val.ToArray())));
                }
                res.Add(new Dictionary<string, string>());
                res[res.Count - 1].Add("document_id", results[rIndex].Elemento_id + "_" + results[rIndex].Cod_Versao);
                res[res.Count - 1].Add("uid", results[rIndex].Uid + "");
                res[res.Count - 1].Add("first_name", results[rIndex].First_name);
                res[res.Count - 1].Add("last_name", results[rIndex].Last_name);
                res[res.Count - 1].Add("dob", results[rIndex].Dob.ToString("d MMMM yyyy", ci));
                res[res.Count - 1].Add("text", searchResults.ToString());
                res[res.Count - 1].Add("total_num", results.NumFound + "");

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