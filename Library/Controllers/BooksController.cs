using LibraryDB;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryDB.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private LibraryRepository<Book> _bookRepo;
        private LibraryRepository<Autor> _autorRepo;
        private LibraryRepository<LibraryStorageUnit> _unitRepo;

        public BooksController()
        {
            _bookRepo = new LibraryRepository<Book>(new LibraryDBContext());
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
            _unitRepo = new LibraryRepository<LibraryStorageUnit>(new LibraryDBContext());
        }

        public ActionResult List()
        {
            var books = _bookRepo.GetWithInclude(b => b.Unit.Autor);
            return View(books);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _bookRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
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
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]Book book)
        {
            book.Unit.UnitName = nameof(Book);
            if (ModelState.IsValid)
            {
                _bookRepo.Add(book);
                return RedirectToAction("List");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookRepo.FindById(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var autors = new SelectList(_autorRepo.Get(), "Id", "AutorName");
            var unit = new SelectList(_unitRepo.Get().Where(u => u.UnitName == null || u.Id == id), "Id", "Title");
            ViewBag.Autors = autors;
            ViewBag.Units = unit;
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.Edit(book);
                return RedirectToAction("List");
            }
            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = _bookRepo.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _bookRepo.FindById(id);
            if (book != null)
            {
                _bookRepo.Delete(book);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(Book)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _bookRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}