using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
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
            return View();
        }

        public JsonResult GetData(int? id)
        {
            var booksDtos = _bookDtoRepo.Get();
            if (id != null)
            {
                booksDtos = _bookDtoRepo.Get().Where(b => b.Id == id);
            }

            var booksView = booksDtos.Select(
                    b =>
                        new BookViewModel
                        {
                            Id = b.Id,
                            Title = b.Unit.Title,
                            Autor = Mapper.Map<AutorDTO, AutorViewModel>(b.Unit.Autor),
                            Genre = b.Genre.ToString(),
                            ReleaseDate = b.ReleaseDate,
                            UnitId = b.Unit.Id,
                        }).ToList();

            return Json(booksView, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorsList()
        {
            var autorsNameList = _autorDtoRepo.Get().Select(a => new AutorViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname
            });

            return Json(autorsNameList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BookGenreDTO));
            return Json(genreList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookView)
        {
            if (bookView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForEdit = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == bookView.UnitId);

            if (unitForEdit != null)
            {
                unitForEdit.Title = bookView.Title;
                unitForEdit.AutorId = bookView.Autor.Id;
                unitForEdit.Autor = _autorDtoRepo.Get().FirstOrDefault(a => a.Id == bookView.Autor.Id);
                _unitDtoRepo.Edit(unitForEdit);
            }

            var bookForEdit = _bookDtoRepo.Get().FirstOrDefault(u => u.Id == bookView.Id);

            if (bookForEdit != null)
            {
                bookForEdit.Unit = unitForEdit;
                bookForEdit.Genre = (BookGenreDTO)Enum.Parse(typeof(BookGenreDTO), bookView.Genre);
                bookForEdit.ReleaseDate = bookView.ReleaseDate;

                _bookDtoRepo.Edit(bookForEdit);
            }
            return Json(bookView);
        }

        [HttpPut]
        public ActionResult Add(BookViewModel bookView)
        {
            if (bookView == null || !ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var unitForAdd = new LibraryStorageUnitDTO
            {
                AutorId = bookView.Autor.Id,
                Title = bookView.Title
            };

            var bookForAdd = new BookDTO
            {
                Genre = (BookGenreDTO)Enum.Parse(typeof(BookGenreDTO), bookView.Genre),
                ReleaseDate = bookView.ReleaseDate,
                Unit = unitForAdd,
                UnitId = unitForAdd.Id
            };

            _bookDtoRepo.Create(bookForAdd);
            return Json(bookView);
        }

        public ActionResult Delete(BookViewModel bookView)
        {
            var bookDto = _bookDtoRepo.Get().FirstOrDefault(b => b.Id == bookView.Id);
            var unitDto = _unitDtoRepo.Get().FirstOrDefault(u => u.Id == bookView.UnitId);

            if (bookDto == null || unitDto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _bookDtoRepo.Delete(bookDto);
            _unitDtoRepo.Delete(unitDto);
            return Json(bookView);
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