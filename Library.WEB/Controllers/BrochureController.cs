using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class BrochureController : Controller
    {
        private readonly BrochureService _brochureService;

        public BrochureController(BrochureService brochureService)
        {
            _brochureService = brochureService;
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
                string connectionString = filePath + "Brochures." + fileType;

                _brochureService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Brochures." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}