using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.EnumsDTO;
using Library.BLL.Services;
using Library.WEB.ViewModels;

namespace Library.WEB.ApiControllers.Controllers
{
    public class BooksController : ApiController
    {
        private readonly BookService _bookService;
        private readonly LibraryStorageUnitService _libraryStorageUnitService;

        public BooksController(BookService bookService, LibraryStorageUnitService libraryStorageUnitService)
        {
            _bookService = bookService;
            _libraryStorageUnitService = libraryStorageUnitService;
        }

        [Route("api/Books/{id:int?}")]
        [HttpGet]
        public IHttpActionResult GetData(int? id = null)
        {
            var booksDto = _bookService.Get();
            if (id != null)
            {
                booksDto = _bookService.Get().Where(b => b.Id == id);
            }

            var booksForView = Mapper.Map<IEnumerable<BookDTO>, List<BookViewModel>>(booksDto);

            return Ok(booksForView);
        }

        [Route("api/Books/Genre")]
        [HttpGet]
        public IHttpActionResult GenreList()
        {
            var genreList = Enum.GetNames(typeof(BookGenreDTO));
            return Ok(genreList);
        }

        [Route("api/Books")]
        [HttpPost]
        public IHttpActionResult Edit(BookViewModel bookFromView)
        {
            if (bookFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                return BadRequest();
            }

            return Ok(bookFromView);
        }

        [Route("api/Books")]
        [HttpPut]
        public IHttpActionResult Add(BookViewModel bookFromView)
        {
            if (bookFromView == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var libraryStorageUnitForAdd = Mapper.Map<BookViewModel, LibraryStorageUnitDTO>(bookFromView);

            var bookForAdd = Mapper.Map<BookViewModel, BookDTO>(bookFromView);
            bookForAdd.Unit = libraryStorageUnitForAdd;
            _bookService.Create(bookForAdd);

            return Ok(bookFromView);
        }

        [Route("api/Books")]
        [HttpDelete]
        public IHttpActionResult Delete(BookViewModel bookFromView)
        {
            var bookDto = _bookService.Get().FirstOrDefault(b => b.Id == bookFromView.Id);
            var unitDto = _libraryStorageUnitService.Get().FirstOrDefault(u => u.Id == bookFromView.UnitId);

            if (bookDto == null || unitDto == null)
            {
                return BadRequest();
            }
            _bookService.Delete(bookDto);
            _libraryStorageUnitService.Delete(unitDto);

            return Ok(bookFromView);
        }
    }
}