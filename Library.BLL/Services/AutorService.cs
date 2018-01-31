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
    public class AutorService : IGenericServiceDTO<AutorDTO>
    {
        private readonly IGenericRepository<Autor> _autorRepository;

        public AutorService(IGenericRepository<Autor> autorDtoRepository)
        {
            _autorRepository = autorDtoRepository;
        }

        public void Create(AutorDTO autorDto)
        {
            _autorRepository.Add(Mapper.Map<AutorDTO, Autor>(autorDto));
            _autorRepository.SaveChanges();
        }

        public void Delete(AutorDTO autorDto)
        {
            _autorRepository.Delete(Mapper.Map<AutorDTO, Autor>(autorDto));
            _autorRepository.SaveChanges();
        }

        public void Edit(AutorDTO autorDto)
        {
            _autorRepository.Edit(Mapper.Map<AutorDTO, Autor>(autorDto));
            _autorRepository.SaveChanges();
        }

        public AutorDTO FindById(int? id)
        {
            var autor = _autorRepository.FindById(id);
            return Mapper.Map<Autor, AutorDTO>(autor);
        }

        public IEnumerable<AutorDTO> Get()
        {
            return Mapper.Map<IEnumerable<Autor>, List<AutorDTO>>(_autorRepository.Get());
        }

        public AutorDTO GetWithInclude(int? id)
        {
            var autor = _autorRepository.GetWithInclude(a => a.Units).FirstOrDefault(a => a.Id == id);
            return Mapper.Map<Autor, AutorDTO>(autor);
        }

        public void SaveToFile(string connectionString)
        {
            _autorRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}