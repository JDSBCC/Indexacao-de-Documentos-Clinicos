using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace IndexDocClinicos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Indexação de Documentos Clínicos";
            return View();
        }


        public ActionResult GetDoc(string id)
        {
            ViewBag.Title = "Indexação de Documentos Clínicos";

            byte[] file = Convert.FromBase64String(new DocumentsController().GetFile(id));

            return File(file, "application/pdf");
        }
    }
}
