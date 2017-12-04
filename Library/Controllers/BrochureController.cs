using LibraryDB;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryDB.Models;

namespace Library.Controllers
{
    public class BrochureController : Controller
    {
        private LibraryRepository<Brochure> _brochureRepo;
        private LibraryRepository<Autor> _autorRepo;
        private LibraryRepository<LibraryStorageUnit> _unitRepo;
        
        public BrochureController()
        {
            _brochureRepo = new LibraryRepository<Brochure>(new LibraryDBContext());
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
        }

        public ActionResult List()
        {
            var brochure = _brochureRepo.GetWithInclude(b => b.Unit.Autor);
            return View(brochure);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var brochure = _brochureRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            if (brochure == null)
            {
                return HttpNotFound();
            }

            return View(brochure);
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
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]Brochure brochure)
        {
            brochure.Unit.UnitName = nameof(Book);
            if (ModelState.IsValid)
            {
                _brochureRepo.Add(brochure);
                return RedirectToAction("List");
            }
            return View(brochure);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brochure = _brochureRepo.FindById(id);
            if (brochure == null)
            {
                return HttpNotFound();
            }
            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            var unit = new SelectList(_unitRepo.Get().Where(u => u.UnitName == null || u.Id == id), "Id", "Title");
            ViewBag.Autors = autors;
            ViewBag.Units = unit;
            return View(brochure);
        }

        [HttpPost]
        public ActionResult Edit(Brochure brochure)
        {
            if (ModelState.IsValid)
            {
                _brochureRepo.Edit(brochure);
                return RedirectToAction("List");
            }
            return View(brochure);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var brochure = _brochureRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);

            if (brochure == null)
            {
                return HttpNotFound();
            }
            return View(brochure);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var brochure = _brochureRepo.FindById(id);
            if (brochure != null)
            {
                _brochureRepo.Delete(brochure);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(Brochure)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _brochureRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}