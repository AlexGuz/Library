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
            var brochureDto = _brochureDtoRepo.Get();

            return View(Mapper.Map<IEnumerable<BrochureDTO>, List<BrochureViewModel>>(brochureDto));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var brochureDto = _brochureDtoRepo.GetWithInclude(id);
            
            if (brochureDto == null)
            {
                return HttpNotFound();
            }
            var brochureView = Mapper.Map<BrochureDTO, BrochureViewModel>(brochureDto);

            return View(brochureView);
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
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]BrochureViewModel brochureView)
        {
            if (ModelState.IsValid)
            {
                _brochureDtoRepo.Create(Mapper.Map<BrochureViewModel, BrochureDTO>(brochureView));
                return RedirectToAction("List");
            }
            return View(brochureView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brochureDto = _brochureDtoRepo.FindById(id);
            if (brochureDto == null)
            {
                return HttpNotFound();
            }
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto), "Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;
          
            return View(Mapper.Map<BrochureDTO, BrochureViewModel>(brochureDto));
        }

        [HttpPost]
        public ActionResult Edit(BrochureViewModel brochureView)
        {
            if (ModelState.IsValid)
            {
                _brochureDtoRepo.Edit(Mapper.Map<BrochureViewModel, BrochureDTO>(brochureView));
                return RedirectToAction("List");
            }
            return View(brochureView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brochureDto = _brochureDtoRepo.GetWithInclude(id);

            if (brochureDto == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<BrochureDTO, BrochureViewModel>(brochureDto));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var brochureDto = _brochureDtoRepo.FindById(id);
            if (brochureDto != null)
            {
                _brochureDtoRepo.Delete(brochureDto);
            }
            return RedirectToAction("List");
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