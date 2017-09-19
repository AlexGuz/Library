using Library.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class AutorsController : Controller
    {
        private LibraryContext _db = new LibraryContext();

        public ActionResult List()
        {
            var autors = _db.Autors;
            
            return View(autors);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var autor = _db.Autors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);

            if (autor == null)
            {
                return HttpNotFound();
            }

            return View(autor);
        }

        [HttpGet]
        public ActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,Name,Surname")]Autor autor)
        {
            if (ModelState.IsValid)
            {
                _db.Autors.Add(autor);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(autor);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var autor = _db.Autors.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }            
            return View(autor);
        }

        [HttpPost]
        public ActionResult Edit(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(autor).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(autor);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var autor = _db.Autors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);

            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var autor = _db.Autors.Find(id);
            if (autor != null)
            {
                _db.Autors.Remove(autor);
                _db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}