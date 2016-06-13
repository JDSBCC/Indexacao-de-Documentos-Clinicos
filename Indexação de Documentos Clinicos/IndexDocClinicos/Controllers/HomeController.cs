using IndexDocClinicos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Xml;

namespace IndexDocClinicos.Controllers
{
    public class HomeController : Controller
    {
        private static SearchView searchModel = new SearchView();

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public PartialViewResult Search(string id, int page)
        {
            string[] values = id.Split('_');
            searchModel.SearchTerm = values[0];
            searchModel.Page = page - 1;
            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            if (values.Length==1) {
                res = new ContributionsController().GetContributions(searchModel.SearchTerm, searchModel.Start, searchModel.Rows);
            } else {
                res = new ContributionsController().GetContributionsByRangeDate(id, searchModel.Start, searchModel.Rows);
            }
            searchModel.Results = res;

            if (res.Count==0)
            {
                searchModel.Reset();
                return PartialView("ResultsPartial");
            }
            searchModel.TotalResults = Convert.ToInt32(res[0]["total_num"]);
            ViewBag.SearchTerm = id;

            return PartialView("ResultsPartial", searchModel);
        }

        public PartialViewResult Pagination()
        {
            return PartialView("PaginationPartial", searchModel);
        }


        public ActionResult Metadata(string id)
        {//uid of contribution

            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["VersionsFolder"] + "\\" + id + ".xml");
            XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("items");
            for (int i = 1; i < nodes.Count; i++ )
            {
                try {
                    ViewData[nodes[i]["name"]["value"].InnerText] = nodes[i]["value"]["value"].InnerText;
                } catch (NullReferenceException) {
                    ViewData[nodes[i]["name"]["value"].InnerText] = nodes[i]["value"]["magnitude"].InnerText;
                }
            }

            return View("Metadata");
        }
    }
}
