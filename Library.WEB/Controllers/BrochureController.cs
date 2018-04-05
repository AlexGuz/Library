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
    public class BrochureController : Controller
    {
        private readonly BrochureService _brochureService;
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public BrochureController(AutorService autorService, BrochureService brochureService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _brochureService = brochureService;
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var brochuresDto = _brochureService.Get();
            if (id != null)
            {
                brochuresDto = _brochureService.Get().Where(b => b.Id == id);
            }

            var brochuresForView = Mapper.Map<IEnumerable<BrochureDTO>, List<BrochureViewModel>>(brochuresDto);

            return Json(brochuresForView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsDto = _autorService.Get();
            var autorsNameList = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BrochureTypeDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(BrochureViewModel brochureFromView)
        {
            if (brochureFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<BrochureViewModel, LibraryStorageUnitDTO>(brochureFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var brochureForEdit = Mapper.Map<BrochureViewModel, BrochureDTO>(brochureFromView);
                _brochureService.Edit(brochureForEdit);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(brochureFromView);
        }

        [HttpPut]
        public ActionResult Add(BrochureViewModel brochureFromView)
        {
            if (brochureFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var libraryStorageUnitForAdd = Mapper.Map<BrochureViewModel, LibraryStorageUnitDTO>(brochureFromView);

            var brochureForAdd = Mapper.Map<BrochureViewModel, BrochureDTO>(brochureFromView);
            brochureForAdd.Unit = libraryStorageUnitForAdd;
            _brochureService.Create(brochureForAdd);

            return Json(brochureFromView);
        }

        public ActionResult Delete(BrochureViewModel brochureFromView)
        {
            var brochureForDelete = _brochureService.Get().FirstOrDefault(b => b.Id == brochureFromView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == brochureFromView.UnitId);

            if (brochureForDelete == null || unitForDelete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _brochureService.Delete(brochureForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);

            return Json(brochureFromView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Brochures." + fileType;

                _autorService.SaveToFile(connectionString);
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