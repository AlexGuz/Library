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
   public class NewspaperService : IGenericServiceDTO<NewspaperDTO>
    {
        private readonly IGenericRepository<Newspaper> _newspaperRepository;

        public NewspaperService(IGenericRepository<Newspaper> newspaperDtoRepository)
        {
            _newspaperRepository = newspaperDtoRepository;
        }

        public void Create(NewspaperDTO newspaperDto)
        {
            newspaperDto.Unit.UnitName = nameof(Newspaper);
            _newspaperRepository.Add(Mapper.Map<NewspaperDTO, Newspaper>(newspaperDto));
            _newspaperRepository.SaveChanges();
        }

        public void Delete(NewspaperDTO newspaperDto)
        {
            _newspaperRepository.Delete(Mapper.Map<NewspaperDTO, Newspaper>(newspaperDto));
            _newspaperRepository.SaveChanges();
        }

        public void Edit(NewspaperDTO newspaperDto)
        {
            _newspaperRepository.Edit(Mapper.Map<NewspaperDTO, Newspaper>(newspaperDto));
            _newspaperRepository.SaveChanges();
        }

        public NewspaperDTO FindById(int? id)
        {
            var newspaper = _newspaperRepository.FindById(id);
            return Mapper.Map<Newspaper, NewspaperDTO>(newspaper);
        }

        public IEnumerable<NewspaperDTO> Get()
        {
            return Mapper.Map<IEnumerable<Newspaper>, List<NewspaperDTO>>(_newspaperRepository.GetWithInclude(b => b.Unit.Autor));
        }

        public NewspaperDTO GetWithInclude(int? id)
        {
            var newspaper = _newspaperRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            return Mapper.Map<Newspaper, NewspaperDTO>(newspaper);
        }

        public void SaveToFile(string connectionString)
        {
            _newspaperRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}
