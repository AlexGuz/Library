using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.WEB.Models;

namespace Library.WEB.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookDtoRepo;
        private readonly AutorService _autorDtoRepo;
        private readonly LibraryStorageUnitService _unitDtoRepo;

        public BooksController(AutorService autorDtoRepo, BookService bookDtoRepo, LibraryStorageUnitService unitDtoRepo)
        {
            _bookDtoRepo = bookDtoRepo;
            _autorDtoRepo = autorDtoRepo;
            _unitDtoRepo = unitDtoRepo;
        }
        
        public ActionResult List()
        {
            var booksDto = _bookDtoRepo.Get();
            
            return View(Mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(booksDto));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            
            var bookDto = _bookDtoRepo.GetWithInclude(id);
            var bookView = Mapper.Map<BookDTO, BookViewModel>(bookDto);
            if (bookView == null)
            {
                return HttpNotFound();
            }

            return View(bookView);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto),"Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;
            return View();
        }

        [HttpPost]
        public ActionResult Add([Bind(Include = "Id,ReleaseDate")]BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                _bookDtoRepo.Create(Mapper.Map<BookViewModel, BookDTO>(bookView));
                return RedirectToAction("List");
            }
            return View(bookView);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookDto = _bookDtoRepo.FindById(id);
            if (bookDto == null)
            {
                return HttpNotFound();
            }
            
            var autorsDto = _autorDtoRepo.Get();
            var autorsView = new SelectList(Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto), "Id", "AutorName");
            var unitsDto = _unitDtoRepo.Get().Where(u => u.UnitName == null);
            var unitsView = new SelectList(Mapper.Map<IEnumerable<LibraryStorageUnitDTO>, List<LibraryStorageUnitViewModel>>(unitsDto), "Id", "Title");

            ViewBag.Autors = autorsView;
            ViewBag.Units = unitsView;

            return View(Mapper.Map<BookDTO, BookViewModel>(bookDto));
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                _bookDtoRepo.Edit(Mapper.Map<BookViewModel, BookDTO>(bookView));
                return RedirectToAction("List");
            }
            return View(bookView);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bookDto = _bookDtoRepo.GetWithInclude(id);

            if (bookDto == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<BookDTO, BookViewModel>(bookDto));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var bookDto = _bookDtoRepo.FindById(id);
            if (bookDto != null)
            {
                _bookDtoRepo.Delete(bookDto);
            }
            return RedirectToAction("List");
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Books." + fileType;

                _autorDtoRepo.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Books." + fileType,
                    filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}