using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
using Library.BLL.Services;
using Library.WEB.Models;

namespace Library.WEB.Controllers
{
    public class BrochureController : Controller
    {
        private readonly BrochureService _brochureDtoRepo;
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public BrochureController(AutorService autorDtoRepo, BrochureService brochureDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _brochureDtoRepo = brochureDtoRepo;
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var brochureDtos = _brochureDtoRepo.Get();
            if (id != null)
            {
                brochureDtos = _brochureDtoRepo.Get().Where(b => b.Id == id);
            }

            var brochureView = brochureDtos.Select(
                    b =>
                        new BrochureViewModel
                        {
                            Id = b.Id,
                            Title = b.Unit.Title,
                            Autor = Mapper.Map<AutorDTO, AutorViewModel>(b.Unit.Autor),
                            Type = b.Type.ToString(),
                            ReleaseDate = b.ReleaseDate,
                            UnitId = b.Unit.Id,
                        }).ToList();

            return Json(brochureView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsNameList = _autorDtoRepo.Get().Select(a => new AutorViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname
            });

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BrochureTypeDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(BrochureViewModel brochureView)
        {
            if (brochureView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForEdit = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == brochureView.UnitId);

            if (unitForEdit != null)
            {
                unitForEdit.Title = brochureView.Title;
                unitForEdit.AutorId = brochureView.Autor.Id;
                unitForEdit.Autor = _autorDtoRepo.Get().FirstOrDefault(a => a.Id == brochureView.Autor.Id);
                _unitDtoRepo.Edit(unitForEdit);
            }

            var brochureForEdit = _brochureDtoRepo.Get().FirstOrDefault(u => u.Id == brochureView.Id);

            if (brochureForEdit != null)
            {
                brochureForEdit.Unit = unitForEdit;
                brochureForEdit.Type = (BrochureTypeDTO)Enum.Parse(typeof(BrochureTypeDTO), brochureView.Type);
                brochureForEdit.ReleaseDate = brochureView.ReleaseDate;

                _brochureDtoRepo.Edit(brochureForEdit);
            }
            return Json(brochureView);
        }

        [HttpPut]
        public ActionResult Add(BrochureViewModel brochureView)
        {
            if (brochureView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForAdd = new LibraryStorageUnitDTO
            {
                AutorId = brochureView.Autor.Id,
                Title = brochureView.Title
            };

            var brochureForAdd = new BrochureDTO
            {
                Type = (BrochureTypeDTO)Enum.Parse(typeof(BrochureTypeDTO), brochureView.Type),
                ReleaseDate = brochureView.ReleaseDate,
                Unit = unitForAdd,
                UnitId = unitForAdd.Id
            };

            _brochureDtoRepo.Create(brochureForAdd);
            return Json(brochureView);
        }

        public ActionResult Delete(BrochureViewModel brochureView)
        {
            var brochureDto = _brochureDtoRepo.Get().FirstOrDefault(b => b.Id == brochureView.Id);
            var unitDto = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == brochureView.UnitId);

            if (brochureDto == null || unitDto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _brochureDtoRepo.Delete(brochureDto);
            _unitDtoRepo.Delete(unitDto);
            return Json(brochureView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Brochures." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Brochures." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}