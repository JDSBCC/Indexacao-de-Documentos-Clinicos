using IndexDocClinicos.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
