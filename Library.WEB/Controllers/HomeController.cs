using System.Web.Mvc;

namespace Library.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveToFile(string path, string name)
        {
            ViewBag.FileName = name;
            ViewBag.FilePath = path;
            return View();
        }
    }
}