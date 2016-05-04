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

        public List<string> GetAllContributions()
        {
            return Query("*:*");
        }

        public List<string> GetContribution(string id)
        {
            return Query(id);
        }

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
                searchResults.Append("</small><a href='' onclick='getFile();'>Documento</a>&nbsp;-&nbsp;<a href='' onclick=''>Informação Demográfica</a></div></div>");
                res.Add(searchResults.ToString());
                rIndex++;
            }

            return res;
        }

        /*[HttpGet]
        public string GetFile(string id)
        {
            /*var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            var results = solr.Query(new SolrQuery(id), new QueryOptions
            {
                Fields = new[] { "elemento_id", "cod_versao" }
            });

            Debug.WriteLine(results[0].Elemento_id);
            Debug.WriteLine("HERE");

            /*
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Contribution>>();
            using (MemoryStream stream = new MemoryStream((byte[]) Data.contributions.)
            {
                ExtractParameters extract = new ExtractParameters(stream, dataReaderOracle["elemento_id"] + "", dataReaderOracle["nome_original"] + "")
                {
                    ExtractOnly = true,
                    ExtractFormat = ExtractFormat.Text
                };
                var response = solr.Extract(extract);
            return "";
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