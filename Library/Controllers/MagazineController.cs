using LibraryDB;
using LibraryDB.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Library.Controllers
{
    public class MagazineController : Controller
    {
        private LibraryRepository<Magazine> _magazineRepo;
        private LibraryRepository<Autor> _autorRepo;
        private LibraryRepository<LibraryStorageUnit> _unitRepo;

        public MagazineController()
        {
            _magazineRepo = new LibraryRepository<Magazine>(new LibraryDBContext());
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
        }

        public ActionResult List()
        {
            var magazine = _magazineRepo.GetWithInclude(b => b.Unit.Autor);
            return View(magazine);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var magazine = _magazineRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            if (magazine == null)
            {
                return HttpNotFound();
            }

            return View(magazine);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            var unit = new SelectList(_unitRepo.Get().Where(u => u.UnitName == null), "Id", "Title");
            ViewBag.Autors = autors;
            ViewBag.Units = unit;
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]Magazine magazine)
        {
            magazine.Unit.UnitName = nameof(Book);
            if (ModelState.IsValid)
            {
                _magazineRepo.Add(magazine);
                return RedirectToAction("List");
            }
            return View(magazine);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var magazine = _magazineRepo.FindById(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            var unit = new SelectList(_unitRepo.Get().Where(u => u.UnitName == null || u.Id == id), "Id", "Title");
            ViewBag.Autors = autors;
            ViewBag.Units = unit;
            return View(magazine);
        }

        [HttpPost]
        public ActionResult Edit(Magazine magazine)
        {
            if (ModelState.IsValid)
            {
                _magazineRepo.Edit(magazine);
                return RedirectToAction("List");
            }
            return View(magazine);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var magazine = _magazineRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);

            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var magazine = _magazineRepo.FindById(id);
            if (magazine != null)
            {
                _magazineRepo.Delete(magazine);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(Magazine)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _magazineRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}