using System.Dynamic;
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
            dynamic info = new ExpandoObject();
            info.Path = path;
            info.Name = name;

            return View(info);
        }
    }
}