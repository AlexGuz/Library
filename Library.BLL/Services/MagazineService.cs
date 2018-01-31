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
    public class MagazineService : IGenericServiceDTO<MagazineDTO>
    {
        private readonly IGenericRepository<Magazine> _magazineRepository;

        public MagazineService(IGenericRepository<Magazine> magazineDtoRepository)
        {
            _magazineRepository = magazineDtoRepository;
        }

        public void Create(MagazineDTO magazineDto)
        {
            magazineDto.Unit.UnitName = nameof(Magazine);
            _magazineRepository.Add(Mapper.Map<MagazineDTO, Magazine>(magazineDto));
            _magazineRepository.SaveChanges();
        }

        public void Delete(MagazineDTO magazineDto)
        {
            _magazineRepository.Delete(Mapper.Map<MagazineDTO, Magazine>(magazineDto));
            _magazineRepository.SaveChanges();
        }

        public void Edit(MagazineDTO magazineDto)
        {
            _magazineRepository.Edit(Mapper.Map<MagazineDTO, Magazine>(magazineDto));
            _magazineRepository.SaveChanges();
        }

        public MagazineDTO FindById(int? id)
        {
            var magazine = _magazineRepository.FindById(id);
            return Mapper.Map<Magazine, MagazineDTO>(magazine);
        }

        public IEnumerable<MagazineDTO> Get()
        {
            return Mapper.Map<IEnumerable<Magazine>, List<MagazineDTO>>(_magazineRepository.GetWithInclude(b => b.Unit.Autor));
        }

        public MagazineDTO GetWithInclude(int? id)
        {
            var magazine = _magazineRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            return Mapper.Map<Magazine, MagazineDTO>(magazine);
        }

        public void SaveToFile(string connectionString)
        {
            _magazineRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}
