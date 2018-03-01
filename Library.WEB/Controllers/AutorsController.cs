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
                        new AutorViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Surname = a.Surname,
                            FoundingDate = a.FoundingDate != 0 ? (int?)a.FoundingDate : null
                        }).ToList();

            return Json(autorsView, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetails(int id)
        {
            var unitDtos = _unitDtoRepo.Get().Where(u => u.AutorId == id);
            var unitview = unitDtos.Select(
                    u =>
                        new LibraryStorageUnitViewModel
                        {
                            Id = u.Id,
                            Title = u.Title
                        }).ToList();

            return Json(unitview, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(AutorViewModel autorView)
        {
            if (autorView != null && ModelState.IsValid)
            {
                _autorDtoRepo.Edit(Mapper.Map<AutorViewModel, AutorDTO>(autorView));
                return Json(autorView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public ActionResult Add(AutorViewModel autorView)
        {
            if (ModelState.IsValid)
            {
                _autorDtoRepo.Create(Mapper.Map<AutorViewModel, AutorDTO>(autorView));

                return Json(autorView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Delete(AutorViewModel autorView)
        {
            var autorDto = _autorDtoRepo.GetWithInclude(autorView.Id);

            if (autorDto != null)
            {
                _autorDtoRepo.Delete(autorDto);
                return Json(autorView);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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