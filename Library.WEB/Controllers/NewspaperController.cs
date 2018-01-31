using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
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
            var newspaperDto = _newspaperDtoRepo.Get();

            return View(Mapper.Map<IEnumerable<NewspaperDTO>, List<NewspaperViewModel>>(newspaperDto));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newspaperDto = _newspaperDtoRepo.GetWithInclude(id);

            if (newspaperDto == null)
            {
                return HttpNotFound();
            }
            var newspaperView = Mapper.Map<NewspaperDTO, NewspaperViewModel>(newspaperDto);
            return View(newspaperView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto), "Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]NewspaperViewModel newspaperView)
        {
            if (ModelState.IsValid)
            {
                _newspaperDtoRepo.Create(Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperView));
                return RedirectToAction("List");
            }
            return View(newspaperView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newspaperDto = _newspaperDtoRepo.FindById(id);
            if (newspaperDto == null)
            {
                return HttpNotFound();
            }
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto), "Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;

            return View(Mapper.Map<NewspaperDTO, NewspaperViewModel>(newspaperDto));
        }

        [HttpPost]
        public ActionResult Edit(NewspaperViewModel newspaperView)
        {
            if (ModelState.IsValid)
            {
                _newspaperDtoRepo.Edit(Mapper.Map<NewspaperViewModel, NewspaperDTO>(newspaperView));
                return RedirectToAction("List");
            }
            return View(newspaperView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newspaperDto = _newspaperDtoRepo.GetWithInclude(id);

            if (newspaperDto == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<NewspaperDTO, NewspaperViewModel>(newspaperDto));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var newspaperDto = _newspaperDtoRepo.FindById(id);
            if (newspaperDto != null)
            {
                _newspaperDtoRepo.Delete(newspaperDto);
            }
            return RedirectToAction("List");
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