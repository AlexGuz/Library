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
    public class NewspaperController : Controller
    {
        private readonly NewspaperService _newspaperDtoRepo;
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public NewspaperController(AutorService autorDtoRepo, NewspaperService newspaperDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _newspaperDtoRepo = newspaperDtoRepo;
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var newspaperDtos = _newspaperDtoRepo.Get();
            if (id != null)
            {
                newspaperDtos = _newspaperDtoRepo.Get().Where(n => n.Id == id);
            }

            var newspaperView = newspaperDtos.Select(
                    n =>
                        new NewspaperViewModel
                        {
                            Id = n.Id,
                            Title = n.Unit.Title,
                            Autor = Mapper.Map<AutorDTO, AutorViewModel>(n.Unit.Autor),
                            Type = n.Type.ToString(),
                            ReleaseDate = n.ReleaseDate,
                            UnitId = n.Unit.Id,
                            IssueNumber = n.IssueNumber
                        }).ToList();

            return Json(newspaperView, JsonRequestBehavior.AllowGet);
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
            var genreList = Enum.GetNames(typeof(NewspaperTypeDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(NewspaperViewModel newspaperView)
        {
            if (newspaperView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForEdit = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == newspaperView.UnitId);

            if (unitForEdit != null)
            {
                unitForEdit.Title = newspaperView.Title;
                unitForEdit.AutorId = newspaperView.Autor.Id;
                unitForEdit.Autor = _autorDtoRepo.Get().FirstOrDefault(a => a.Id == newspaperView.Autor.Id);
                _unitDtoRepo.Edit(unitForEdit);
            }

            var newspaperForEdit = _newspaperDtoRepo.Get().FirstOrDefault(u => u.Id == newspaperView.Id);

            if (newspaperForEdit != null)
            {
                newspaperForEdit.Unit = unitForEdit;
                newspaperForEdit.Type = (NewspaperTypeDTO)Enum.Parse(typeof(NewspaperTypeDTO), newspaperView.Type);
                newspaperForEdit.ReleaseDate = newspaperView.ReleaseDate;
                newspaperForEdit.IssueNumber = newspaperView.IssueNumber;

                _newspaperDtoRepo.Edit(newspaperForEdit);
            }
            return Json(newspaperView);
        }

        [HttpPut]
        public ActionResult Add(NewspaperViewModel newspaperView)
        {
            if (newspaperView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForAdd = new LibraryStorageUnitDTO
            {
                AutorId = newspaperView.Autor.Id,
                Title = newspaperView.Title
            };

            var newspaperForAdd = new NewspaperDTO
            {
                Type = (NewspaperTypeDTO)Enum.Parse(typeof(NewspaperTypeDTO), newspaperView.Type),
                ReleaseDate = newspaperView.ReleaseDate,
                IssueNumber = newspaperView.IssueNumber,
                Unit = unitForAdd,
                UnitId = unitForAdd.Id
            };

            _newspaperDtoRepo.Create(newspaperForAdd);
            return Json(newspaperView);
        }

        public ActionResult Delete(NewspaperViewModel newspaperView)
        {
            var newspaperDto = _newspaperDtoRepo.Get().FirstOrDefault(n => n.Id == newspaperView.Id);
            var unitDto = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == newspaperView.UnitId);

            if (newspaperDto == null || unitDto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _newspaperDtoRepo.Delete(newspaperDto);
            _unitDtoRepo.Delete(unitDto);
            return Json(newspaperView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Newspapers." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Newspapers." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}