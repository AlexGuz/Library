using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryDB;
using LibraryDB.Models;

namespace Library.Controllers
{
    public class LibraryStorageUnitController : Controller
    {
        private LibraryRepository<LibraryStorageUnit> _unitRepo;
        private LibraryRepository<Autor> _autorRepo;

        public LibraryStorageUnitController()
        {
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
        }

        public ActionResult List(string unitType)
        {
            var units = _unitRepo.GetWithInclude(b => b.Autor);

            if (!string.IsNullOrEmpty(unitType) && !unitType.Equals("All"))
            {
                units = _unitRepo.GetWithInclude(b => b.Autor).Where(u => u.UnitName == unitType);
            }

            var allType = _unitRepo.Get().GroupBy(u => u.UnitName).Select(u => u.First()).ToList();
            
            allType.Insert(0, new LibraryStorageUnit { UnitName = "All", Id = 0 });

            ViewBag.UnitType = new SelectList(allType.Select(t=>t.UnitName));

            return View(units);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var unit = _unitRepo.FindById(id);
            if (unit == null)
            {
                return HttpNotFound();
            }

            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            ViewBag.Autors = autors;
            return View(unit);
        }

        [HttpPost]
        public ActionResult Edit(LibraryStorageUnit unit)
        {
            unit.UnitName = null;
            if (ModelState.IsValid)
            {
                _unitRepo.Edit(unit);
                return RedirectToAction("List");
            }
            return View(unit);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Title")]LibraryStorageUnit unit)
        {
            if (ModelState.IsValid)
            {
                _unitRepo.Add(unit);
                return RedirectToAction("List");
            }
            return View(unit);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(LibraryStorageUnit)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _unitRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}