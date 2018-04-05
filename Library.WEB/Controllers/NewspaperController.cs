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
    public class NewspaperController : Controller
    {
        private readonly NewspaperService _newspaperService;
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public NewspaperController(AutorService autorService, NewspaperService newspaperService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _newspaperService = newspaperService;
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var newspapersDto = _newspaperService.Get();
            if (id != null)
            {
                newspapersDto = _newspaperService.Get().Where(b => b.Id == id);
            }

            var newspapersForView = Mapper.Map<IEnumerable<NewspaperDTO>, List<NewspaperViewModel>>(newspapersDto);

            return Json(newspapersForView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsDto = _autorService.Get();
            var autorsNameList = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(NewspaperTypeDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(NewspaperViewModel newspaperFromView)
        {
            if (newspaperFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<NewspaperViewModel, LibraryStorageUnitDTO>(newspaperFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var newspaperForEdit = Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperFromView);
                _newspaperService.Edit(newspaperForEdit);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(newspaperFromView);
        }

        [HttpPut]
        public ActionResult Add(NewspaperViewModel newspaperFromView)
        {
            if (newspaperFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var libraryStorageUnitForAdd = Mapper.Map<NewspaperViewModel, LibraryStorageUnitDTO>(newspaperFromView);

            var newspaperForAdd = Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperFromView);
            newspaperForAdd.Unit = libraryStorageUnitForAdd;
            _newspaperService.Create(newspaperForAdd);

            return Json(newspaperFromView);
        }

        public ActionResult Delete(NewspaperViewModel newspaperFromView)
        {
            var newspaperForDelete = _newspaperService.Get().FirstOrDefault(n => n.Id == newspaperFromView.Id);
            var unitForDelete = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == newspaperFromView.UnitId);

            if (newspaperForDelete == null || unitForDelete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _newspaperService.Delete(newspaperForDelete);
            _libraryStorageUnitService.Delete(unitForDelete);
            return Json(newspaperFromView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Newspapers." + fileType;

                _autorService.SaveToFile(connectionString);
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