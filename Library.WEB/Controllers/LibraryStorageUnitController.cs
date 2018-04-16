using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class LibraryStorageUnitController : Controller
    {
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public LibraryStorageUnitController(LibraryStorageUnitService libraryStorageUnitService)
        {
            _libraryStorageUnitService = libraryStorageUnitService;
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
                string connectionString = filePath + "LibraryStorageUnits." + fileType;

                _libraryStorageUnitService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "LibraryStorageUnits." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}