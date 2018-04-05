using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;
        private readonly AutorService _autorService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public BooksController(AutorService autorService, BookService bookService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _bookService = bookService;
            _autorService = autorService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var booksDto = _bookService.Get();
            if (id != null)
            {
                booksDto = _bookService.Get().Where(b => b.Id == id);
            }

            var booksForView = Mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(booksDto);

            return Json(booksForView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsDto = _autorService.Get();
            var autorsNameList = Mapper.Map<IEnumerable<AutorDTO>, List<AutorViewModel>>(autorsDto);

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BookGenreDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookFromView)
        {
            if (bookFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var libraryStorageUnitForEdit = Mapper.Map<BookViewModel, LibraryStorageUnitDTO>(bookFromView);
                _libraryStorageUnitService.Edit(libraryStorageUnitForEdit);

                var bookForEdit = Mapper.Map<BookViewModel, BookDTO>(bookFromView);
                _bookService.Edit(bookForEdit);
            }
            catch (ObjectNotFoundException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(bookFromView);
        }

        [HttpPut]
        public ActionResult Add(BookViewModel bookFromView)
        {
            if (bookFromView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var libraryStorageUnitForAdd = Mapper.Map<BookViewModel, LibraryStorageUnitDTO>(bookFromView);
            
            var bookForAdd = Mapper.Map<BookViewModel, BookDTO>(bookFromView);
            bookForAdd.Unit = libraryStorageUnitForAdd;
            _bookService.Create(bookForAdd);

            return Json(bookFromView);
        }

        public ActionResult Delete(BookViewModel bookFromView)
        {
            var bookDto = _bookService.Get().FirstOrDefault(b => b.Id == bookFromView.Id);
            var unitDto = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == bookFromView.UnitId);

            if (bookDto == null || unitDto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _bookService.Delete(bookDto);
            _libraryStorageUnitService.Delete(unitDto);

            return Json(bookFromView);
        }

        public ActionResult SaveToFile(string fileType, string path)
        {
            if (Request.PhysicalApplicationPath != null)
            {
                var filePath = Server.HtmlEncode(Request.PhysicalApplicationPath);
                string connectionString = filePath + "Books." + fileType;

                _autorService.SaveToFile(connectionString);
                return RedirectToAction("SaveToFile", "Home", new
                {
                    name = "Books." + fileType,
                    path = filePath
                });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}