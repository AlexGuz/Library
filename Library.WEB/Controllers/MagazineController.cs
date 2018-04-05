using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.Controllers
{
    public class MagazineController : Controller
    {
        private readonly MagazineService _magazineService;
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public MagazineController(AutorService autorService, MagazineService magazineService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _magazineService = magazineService;
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var magazinesDto = _magazineService.Get();
            if (id != null)
            {
                magazinesDto = _magazineService.Get().Where(b => b.Id == id);
            }

            var magazinesForView = Mapper.Map<IEnumerable<MagazineDTO>, List<MagazineViewModel>>(magazinesDto);

            return Json(magazinesForView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsDto = _autorService.Get();
            var autorsNameList = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(StylesOfPublicationsDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MagazineViewModel magazineFromView)
        {
            if (magazineFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<MagazineViewModel, LibraryStorageUnitDTO>(magazineFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var magazineForEdit = Mapper.Map<MagazineViewModel, MagazineDTO>(magazineFromView);
                _magazineService.Edit(magazineForEdit);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(magazineFromView);
        }

        [HttpPut]
        public ActionResult Add(MagazineViewModel magazineFromView)
        {
            if (magazineFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var libraryStorageUnitForAdd = Mapper.Map<MagazineViewModel, LibraryStorageUnitDTO>(magazineFromView);

            var magazineForAdd = Mapper.Map<MagazineViewModel, MagazineDTO>(magazineFromView);
            magazineForAdd.Unit = libraryStorageUnitForAdd;
            _magazineService.Create(magazineForAdd);

            return Json(magazineFromView);
        }

        public ActionResult Delete(MagazineViewModel magazineView)
        {
            var magazineForDelete = _magazineService.Get().FirstOrDefault(m => m.Id == magazineView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == magazineView.UnitId);

            if (magazineForDelete == null || unitForDelete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _magazineService.Delete(magazineForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);
            return Json(magazineView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Magazines." + fileType;

                _autorService.SaveToFile(connectionString);
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