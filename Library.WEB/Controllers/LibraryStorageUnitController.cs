using System.Linq;
using System.Net;
using System.Web.Mvc;
using Library.BLL.Services;

namespace Library.WEB.Controllers
{
    public class LibraryStorageUnitController : Controller
    {
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public LibraryStorageUnitController(AutorService autorDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }
        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var unitsDto = _unitDtoRepo.Get();

            var unitView =
                unitsDto.Select(
                    u =>
                        new
                        {
                            u.Title,
                            u.UnitName,
                            u.Autor.AutorName
                        }).ToList();

            return Json(unitView, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "LibraryStorageUnits." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "LibraryStorageUnits." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}