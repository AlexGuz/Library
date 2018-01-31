using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.Models;

namespace Library.WEB.Controllers
{
    public class LibraryStorageUnitController : Controller
    {
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public LibraryStorageUnitController(AutorService autorDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }
        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData()
        {
            var unitsDto = _unitDtoRepo.Get();

            var unitView =
                unitsDto.Select(
                    u =>
                        new 
                        {
                            u.Title, u.UnitName
                        }).ToList();

            return Json(unitView, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unitDto = _unitDtoRepo.FindById(id);
            if (unitDto == null)
            {
                return HttpNotFound();
            }

            var autors = new SelectList(_autorDtoRepo.Get(), "Id", "AutorName");
            ViewBag.Autors = autors;
            return View(Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnitViewModel>(unitDto));
        }

        [HttpPost]
        public ActionResult Edit(LibraryStorageUnitViewModel unitViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitDtoRepo.Edit(Mapper.Map<LibraryStorageUnitViewModel, LibraryStorageUnitDTO>(unitViewModel));
                return RedirectToAction("List");
            }
            return View(unitViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Title")]LibraryStorageUnitViewModel unitViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitDtoRepo.Create(Mapper.Map<LibraryStorageUnitViewModel, LibraryStorageUnitDTO>(unitViewModel));
                return RedirectToAction("List");
            }
            return View(unitViewModel);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "LibraryStorageUnits." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "LibraryStorageUnits." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}