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
    public class MagazineController : Controller
    {
        private readonly MagazineService _magazineDtoRepo;
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public MagazineController(AutorService autorDtoRepo, MagazineService magazineDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _magazineDtoRepo = magazineDtoRepo;
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var magazineDtos = _magazineDtoRepo.Get();
            if (id != null)
            {
                magazineDtos = _magazineDtoRepo.Get().Where(m => m.Id == id);
            }

            var magazineView = magazineDtos.Select(
                    m =>
                        new MagazineViewModel
                        {
                            Id = m.Id,
                            Title = m.Unit.Title,
                            Autor = Mapper.Map<AutorDTO, AutorViewModel>(m.Unit.Autor),
                            Style = m.Style.ToString(),
                            ReleaseDate = m.ReleaseDate,
                            UnitId = m.Unit.Id,
                            IssueNumber = m.IssueNumber
                        }).ToList();

            return Json(magazineView, JsonRequestBehavior.AllowGet);
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
            var genreList = Enum.GetNames(typeof(StylesOfPublicationsDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MagazineViewModel magazineView)
        {
            if (magazineView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForEdit = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == magazineView.UnitId);

            if (unitForEdit != null)
            {
                unitForEdit.Title = magazineView.Title;
                unitForEdit.AutorId = magazineView.Autor.Id;
                unitForEdit.Autor = _autorDtoRepo.Get().FirstOrDefault(a => a.Id == magazineView.Autor.Id);
                _unitDtoRepo.Edit(unitForEdit);
            }

            var magazineForEdit = _magazineDtoRepo.Get().FirstOrDefault(u => u.Id == magazineView.Id);

            if (magazineForEdit != null)
            {
                magazineForEdit.Unit = unitForEdit;
                magazineForEdit.Style = (StylesOfPublicationsDTO)Enum.Parse(typeof(StylesOfPublicationsDTO), magazineView.Style);
                magazineForEdit.ReleaseDate = magazineView.ReleaseDate;
                magazineForEdit.IssueNumber = magazineView.IssueNumber;

                _magazineDtoRepo.Edit(magazineForEdit);
            }
            return Json(magazineView);
        }

        [HttpPut]
        public ActionResult Add(MagazineViewModel magazineView)
        {
            if (magazineView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForAdd = new LibraryStorageUnitDTO
            {
                AutorId = magazineView.Autor.Id,
                Title = magazineView.Title
            };

            var magazineForAdd = new MagazineDTO
            {
                Style = (StylesOfPublicationsDTO)Enum.Parse(typeof(StylesOfPublicationsDTO), magazineView.Style),
                ReleaseDate = magazineView.ReleaseDate,
                Unit = unitForAdd,
                UnitId = unitForAdd.Id,
                IssueNumber = magazineView.IssueNumber
            };

            _magazineDtoRepo.Create(magazineForAdd);
            return Json(magazineView);
        }

        public ActionResult Delete(MagazineViewModel magazineView)
        {
            var magazineDto = _magazineDtoRepo.Get().FirstOrDefault(m => m.Id == magazineView.Id);
            var unitDto = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == magazineView.UnitId);

            if (magazineDto == null || unitDto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _magazineDtoRepo.Delete(magazineDto);
            _unitDtoRepo.Delete(unitDto);
            return Json(magazineView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Magazines." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Magazines." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}