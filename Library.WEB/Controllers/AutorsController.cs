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
        
        //public JsonResult GetData()
        //{
        //    var autorsDto = _autorService.Get();
        //    var autorsForView = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);
            
        //    return Json(autorsForView, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetDetails(int id)
        //{
        //    var libraryStorageUnitDto = _libraryStorageUnitService.Get().Where(u => u.AutorId == id);
        //    var libraryStorageUnitForView = Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(libraryStorageUnitDto);

        //    return Json(libraryStorageUnitForView, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult Edit(AutorViewModel autorFromView)
        //{
        //    if (autorFromView != null && ModelState.IsValid)
        //    {
        //        _autorService.Edit(Mapper.Map<AutorViewModel, AutorDTO>(autorFromView));
        //        return Json(autorFromView);
        //    }
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //}

        //[HttpPut]
        //public ActionResult Add(AutorViewModel autorFromView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _autorService.Create(Mapper.Map<AutorViewModel, AutorDTO>(autorFromView));

        //        return Json(autorFromView);
        //    }
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //}

        //public ActionResult Delete(AutorViewModel autorFromView)
        //{
        //    var autorForDelete = _autorService.GetWithInclude(autorFromView.Id);

        //    if (autorForDelete != null)
        //    {
        //        _autorService.Delete(autorForDelete);
        //        return Json(autorFromView);
        //    }
        //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //}

        public ActionResult SaveToFile(string fileType, string path)
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