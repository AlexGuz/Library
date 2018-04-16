using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class NewspaperController : Controller
    {
        private readonly NewspaperService _newspaperService;

        public NewspaperController(NewspaperService newspaperService)
        {
            _newspaperService = newspaperService;
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
                string connectionString = filePath + "Newspapers." + fileType;

                _newspaperService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Newspapers." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}