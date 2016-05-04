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
    }
}
