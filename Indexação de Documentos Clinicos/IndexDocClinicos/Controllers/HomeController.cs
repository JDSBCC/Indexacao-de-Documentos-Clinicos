using IndexDocClinicos.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Xml;

namespace IndexDocClinicos.Controllers
{
    public class HomeController : Controller
    {
        private SearchView searchModel;

        public HomeController()
        {
            searchModel = new SearchView();
            ViewBag.searchModel = searchModel;
        }

        public ActionResult Index()
        {
            //ViewBag.TotalResults = 0;
            return View();
        }

        public /*PartialViewResult*/ActionResult Search(string id, int page)
        {
            
            searchModel.SearchTerm = id;
            searchModel.Page = page - 1;
            List<Dictionary<string, string>> res = new ContributionsController().GetContributions(searchModel.SearchTerm, searchModel.StartPage, searchModel.Rows);
            searchModel.Results = res;
            if (res.Count==0)
            {
                searchModel.Reset();
                return View("Index");
            }
            searchModel.TotalResults = Convert.ToInt32(res[0]["total_num"]);
            ViewBag.searchModel = searchModel;

            return View("Index");/*PartialView("ResultsPartial");*/
        }

        public ActionResult Document(string id)
        {

            byte[] file = Convert.FromBase64String(new DocumentsController().GetFile(id));

            return File(file, "application/pdf");
        }


        public ActionResult Metadata(string id)
        {//uid of contribution

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Joaogcorreia\Desktop\EHR + Solr + IndexDocClinicos\Indexacao-de-Documentos-Clinicos\cabolabs-ehrserver-master\versions\" + id + ".xml");
            XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("items");
            for (int i = 1; i < nodes.Count; i++ )
            {
                ViewData[nodes[i]["name"]["value"].InnerText] = nodes[i]["value"]["value"].InnerText;
            }

            return View("Metadata");
        }
    }
}
