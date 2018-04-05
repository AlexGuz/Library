using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.ExtensionMethods;
using Library.BLL.Interfaces;
using Library.DAL.Enums;
using Library.DAL.Interfaces;
using Library.DAL.Models;

namespace Library.BLL.Services
{
    public class BookService : IGenericServiceDTO<BookDTO>
    {
        private readonly IGenericRepository<Book> _bookRepository;

        public BookService(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void Create(BookDTO bookFromWeb)
        {
            var bookForAdd = new Book
            {
                Genre = (BookGenre)Enum.Parse(typeof(BookGenre), bookFromWeb.Genre.ToString()),
                ReleaseDate = bookFromWeb.ReleaseDate,
                UnitId = bookFromWeb.Unit.Id,
                Unit = Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(bookFromWeb.Unit)
            };
            bookForAdd.Unit.UnitName = nameof(Book);
            bookForAdd.Unit.Autor = null;
            _bookRepository.Add(bookForAdd);
            _bookRepository.SaveChanges();
        }

        public void Delete(BookDTO bookFromWeb)
        {
            var bookForDelete = _bookRepository.Get().FirstOrDefault(b => b.Id == bookFromWeb.Id);
            _bookRepository.Delete(bookForDelete);
            _bookRepository.SaveChanges();
        }

        public void Edit(BookDTO bookFromWeb)
        {
            var bookForEdit = _bookRepository.Get().FirstOrDefault(u => u.Id == bookFromWeb.Id);
            if (bookForEdit == null)
            {
                throw new ObjectNotFoundException();
            }
            bookForEdit.Genre = (BookGenre)Enum.Parse(typeof(BookGenre), bookFromWeb.Genre.ToString());
            bookForEdit.ReleaseDate = bookFromWeb.ReleaseDate;

            _bookRepository.Edit(bookForEdit);
            _bookRepository.SaveChanges();
        }

        public IEnumerable<BookDTO> Get()
        {
            var books = _bookRepository.GetWithInclude(b => b.Unit.Autor);
            var booksForview = Mapper.Map<IEnumerable<Book>, List<BookDTO>>(books);
            return booksForview;
        }

        public BookDTO GetWithInclude(int? id)
        {
            var book = _bookRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            var bookForview = Mapper.Map<Book, BookDTO>(book);
            return bookForview;
        }

        public void SaveToFile(string connectionString)
        {
            _bookRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}