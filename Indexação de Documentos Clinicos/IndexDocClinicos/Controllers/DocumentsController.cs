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
    public class DocumentsController : ApiController
    {

        //private const int rows = 2;

        [HttpGet]
        public IEnumerable<Document> GetAllDocuments()
        {
            return Query("*:*");
        }

        [HttpGet]
        public IHttpActionResult GetDocument(string id)
        {
            IEnumerable<Document> cont = Query(id);
            if (cont == null)
            {
                return NotFound();
            }
            return Ok(cont);
        }

        [NonAction]
        public IEnumerable<Document> Query(string text)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
            return solr.Query(new SolrQuery(text));
        }
    }
}
