using Library.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        private LibraryContext _db = new LibraryContext();

        public ActionResult List()
        {
            var books = _db.Books.Include(b => b.Autor);
            return View(books);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _db.Books.Include(b => b.Autor).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var autors = new SelectList(_db.Autors, "Id", "AutorName");
            ViewBag.Autors = autors;
            return View();
        }
       
        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Title,ReleaseDate")]Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                _db.SaveChanges();
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
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var autors = new SelectList(_db.Autors, "Id", "AutorName");
            ViewBag.Autors = autors;
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(book).State = EntityState.Modified;
                _db.SaveChanges();
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
            var book = _db.Books.Include(b => b.Autor).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _db.Books.Find(id);
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}