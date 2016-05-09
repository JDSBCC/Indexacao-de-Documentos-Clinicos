using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using System.Xml;

namespace IndexDocClinicos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
