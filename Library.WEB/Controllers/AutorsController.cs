using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class AutorsController : Controller
    {
        private readonly AutorService _autorService;

        public AutorsController(AutorService autorService)
        {
            _autorService = autorService;
        }

        public ActionResult List()
        {
            return View();
        }
        
        public ActionResult SaveToFile(string fileType)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Autors." + fileType;

                _autorService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Autors.",
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}