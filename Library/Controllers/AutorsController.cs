using LibraryDB;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryDB.Models;

namespace Library.Controllers
{
    public class AutorsController : Controller
    {
        private LibraryRepository<Autor> _autorRepo;

        public AutorsController()
        {
            _autorRepo = new LibraryRepository<Autor>(new LibraryDBContext());
        }

        public ActionResult List()
        {            
            var autors = _autorRepo.Get();
            return View(autors);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var autor = _autorRepo.GetWithInclude(a => a.Units).FirstOrDefault(a => a.Id == id);

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
                _autorRepo.Add(autor);
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
            var autor = _autorRepo.FindById(id);
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
                _autorRepo.Edit(autor);
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
            var autor = _autorRepo.GetWithInclude(a => a.Units).FirstOrDefault(a => a.Id == id);

            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var autor = _autorRepo.FindById(id);
            if (autor != null)
            {
                _autorRepo.Delete(autor);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            string fileName = $"{nameof(Autor)}s.";
            string filePath = string.Empty;

            if (Request.PhysicalApplicationPath != null)
            {
                filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + fileName + fileType;
                _autorRepo.Get().ToList().ToXMLFile(connectionString);
            }

            return RedirectToAction("SaveToFile", "Home", new { name = fileName, path = filePath });
        }
    }
}