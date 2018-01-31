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
            var magazineDto = _magazineDtoRepo.Get();

            return View(Mapper.Map<IEnumerable<MagazineDTO>, List<MagazineViewModel>>(magazineDto));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var magazineDto = _magazineDtoRepo.GetWithInclude(id);
            
            if (magazineDto == null)
            {
                return HttpNotFound();
            }
            var magazineView = Mapper.Map<MagazineDTO, MagazineViewModel>(magazineDto);
            return View(magazineView);
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
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]MagazineViewModel magazineView)
        {
            if (ModelState.IsValid)
            {
                _magazineDtoRepo.Create(Mapper.Map<MagazineViewModel, MagazineDTO>(magazineView));
                return RedirectToAction("List");
            }
            return View(magazineView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var magazineDto = _magazineDtoRepo.FindById(id);
            if (magazineDto == null)
            {
                return HttpNotFound();
            }
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto), "Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;

            return View(Mapper.Map<MagazineDTO, MagazineViewModel>(magazineDto));
        }

        [HttpPost]
        public ActionResult Edit(MagazineViewModel magazineView)
        {
            if (ModelState.IsValid)
            {
                _magazineDtoRepo.Edit(Mapper.Map<MagazineViewModel, MagazineDTO>(magazineView));
                return RedirectToAction("List");
            }
            return View(magazineView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var magazineDto = _magazineDtoRepo.GetWithInclude(id);

            if (magazineDto == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<MagazineDTO, MagazineViewModel>(magazineDto));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var magazineDto = _magazineDtoRepo.FindById(id);
            if (magazineDto != null)
            {
                _magazineDtoRepo.Delete(magazineDto);
            }
            return RedirectToAction("List");
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