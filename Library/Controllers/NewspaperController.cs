using LibraryDB;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryDB.Models;

namespace Library.Controllers
{
    public class NewspaperController : Controller
    {
        private LibraryRepository<Newspaper> _newspaperRepo;
        private LibraryRepository<Autor> _autorRepo;
        private LibraryRepository<LibraryStorageUnit> _unitRepo;

        public NewspaperController()
        {
            _newspaperRepo = new LibraryRepository<Newspaper>(new LibraryDBContext());
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
        }

        public ActionResult List()
        {
            var newspaper = _newspaperRepo.GetWithInclude(b => b.Unit.Autor);
            return View(newspaper);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newspaper = _newspaperRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }

            return View(newspaper);
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
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]Newspaper newspaper)
        {
            newspaper.Unit.UnitName = nameof(Book);
            if (ModelState.IsValid)
            {
                _newspaperRepo.Add(newspaper);
                return RedirectToAction("List");
            }
            return View(newspaper);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newspaper = _newspaperRepo.FindById(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            var unit = new SelectList(_unitRepo.Get().Where(u => u.UnitName == null || u.Id == id), "Id", "Title");
            ViewBag.Autors = autors;
            ViewBag.Units = unit;
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult Edit(Newspaper newspaper)
        {
            if (ModelState.IsValid)
            {
                _newspaperRepo.Edit(newspaper);
                return RedirectToAction("List");
            }
            return View(newspaper);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newspaper = _newspaperRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);

            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var newspaper = _newspaperRepo.FindById(id);
            if (newspaper != null)
            {
                _newspaperRepo.Delete(newspaper);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(Newspaper)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _newspaperRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}