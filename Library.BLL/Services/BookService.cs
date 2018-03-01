using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Library.BLL.DTO;
using Library.BLL.ExtensionMethods;
using Library.BLL.Interfaces;
using Library.DAL.Interfaces;
using Library.DAL.Models;

namespace Library.BLL.Services
{
    public class BookService : IGenericServiceDTO<BookDTO>
    {
        private readonly IGenericRepository<Book> _bookRepository;

        public BookService(IGenericRepository<Book> bookDtoRepository)
        {
            _bookRepository = bookDtoRepository;
        }

        public void Create(BookDTO bookDto)
        {
            bookDto.Unit.UnitName = nameof(Book);
            _bookRepository.Add(Mapper.Map<BookDTO, Book>(bookDto));
            _bookRepository.SaveChanges();
        }

        public void Delete(BookDTO bookDto)
        {
            var delBook = _bookRepository.Get().FirstOrDefault(b => b.Id == bookDto.Id);
            _bookRepository.Delete(delBook);
            _bookRepository.SaveChanges();
        }

        public void Edit(BookDTO bookDto)
        {
            _bookRepository.Edit(Mapper.Map<BookDTO, Book>(bookDto));
            _bookRepository.SaveChanges();
        }

        public BookDTO FindById(int? id)
        {
            var book = _bookRepository.FindById(id);
            return Mapper.Map<Book, BookDTO>(book);
        }

        public IEnumerable<BookDTO> Get()
        {
            return Mapper.Map<IEnumerable<Book>, List<BookDTO>>(_bookRepository.GetWithInclude(b => b.Unit.Autor));
        }

        public BookDTO GetWithInclude(int? id)
        {
            var book = _bookRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            return Mapper.Map<Book, BookDTO>(book);
        }

        public void SaveToFile(string connectionString)
        {
            _bookRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}