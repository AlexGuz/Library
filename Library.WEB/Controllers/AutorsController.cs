using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.Models;

namespace Library.WEB.Controllers
{
    public class AutorsController : Controller
    {

        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public AutorsController(AutorService autorDtoRepo, LibraryStorageUnitService unitDtoRepo)
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
            var autorDtos = _autorDtoRepo.Get();
            var autorsView = autorDtos.Select(
                    a =>
                        new
                        {
                            a.Id,
                            a.Name,
                            a.Surname
                        }).ToList();

            return Json(autorsView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetails()
        {
            var unitDtos = _unitDtoRepo.Get();//.Where(u => u.Id == id);
            var unitview = unitDtos.Select(
                    u =>
                        new
                        {
                            u.Id,
                            u.Title,
                            AutorId = u.AutorId
                        }).ToList();

            return Json(unitview, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var autorDto = _autorDtoRepo.GetWithInclude(id);
            var autorView = Mapper.Map<AutorDTO, AutorViewModel>(autorDto);

            if (autorView == null)
            {
                return HttpNotFound();
            }

            return View(autorView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Name,Surname")]AutorViewModel autorView)
        {
            if (ModelState.IsValid)
            {
                _autorDtoRepo.Create(Mapper.Map<AutorViewModel, AutorDTO>(autorView));
                return RedirectToAction("List");
            }
            return View(autorView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var autorDto = _autorDtoRepo.FindById(id);
            if (autorDto == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AutorDTO, AutorViewModel>(autorDto));
        }

        [HttpPost]
        public ActionResult Edit(AutorViewModel autorView)
        {
            if (ModelState.IsValid)
            {
                _autorDtoRepo.Edit(Mapper.Map<AutorViewModel, AutorDTO>(autorView));
                return RedirectToAction("List");
            }
            return View(autorView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var autorDto = _autorDtoRepo.GetWithInclude(id);

            if (autorDto == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AutorDTO, AutorViewModel>(autorDto));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var autorDto = _autorDtoRepo.GetWithInclude(id);
            if (autorDto != null)
            {
                _autorDtoRepo.Delete(autorDto);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Autors." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Autors.",
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}