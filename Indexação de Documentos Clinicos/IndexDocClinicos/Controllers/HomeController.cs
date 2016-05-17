using IndexDocClinicos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Xml;

namespace IndexDocClinicos.Controllers
{
    public class HomeController : Controller
    {
        private static SearchView searchModel = new SearchView();

        public HomeController()
        {
            ViewBag.searchModel = searchModel;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult Search(string id, int page)
        {
            searchModel.SearchTerm = id;
            searchModel.Page = page - 1;
            List<Dictionary<string, string>> res = new ContributionsController().GetContributions(searchModel.SearchTerm, searchModel.Start, searchModel.Rows);
            searchModel.Results = res;

            if (res.Count==0)
            {
                searchModel.Reset();
                return PartialView("ResultsPartial");
            }
            searchModel.TotalResults = Convert.ToInt32(res[0]["total_num"]);
            ViewBag.searchModel = searchModel;

            return PartialView("ResultsPartial");
        }

        public PartialViewResult Pagination()
        {
            ViewBag.searchModel = searchModel;
            return PartialView("PaginationPartial");
        }


        public ActionResult Metadata(string id)
        {//uid of contribution

            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["VersionsFolder"] + "\\" + id + ".xml");
            XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("items");
            for (int i = 1; i < nodes.Count; i++ )
            {
                ViewData[nodes[i]["name"]["value"].InnerText] = nodes[i]["value"]["value"].InnerText;
            }

            return View("Metadata");
        }
    }
}
