using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class MagazineController : Controller
    {
        private readonly MagazineService _magazineService;

        public MagazineController(MagazineService magazineService)
        {
            _magazineService = magazineService;
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Magazines." + fileType;

                _magazineService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Magazines." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}