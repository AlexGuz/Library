using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.Controllers
{
    public class LibraryStorageUnitController : Controller
    {
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public LibraryStorageUnitController(AutorService autorService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }
        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var libraryStorageUnitDto = _libraryStorageUnitService.Get();
            var libraryStorageUnitForView = Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(libraryStorageUnitDto);

            return Json(libraryStorageUnitForView, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "LibraryStorageUnits." + fileType;

                _autorService.SaveToFile(connectionString);
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