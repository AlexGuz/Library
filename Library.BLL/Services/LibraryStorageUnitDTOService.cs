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
    public class LibraryStorageUnitService : IGenericServiceDTO<LibraryStorageUnitDTO>
    {
        private readonly IGenericRepository<LibraryStorageUnit> _libraryStorageUnitRepository;

        public LibraryStorageUnitService(IGenericRepository<LibraryStorageUnit> libraryStorageUnitDtoRepository)
        {
            _libraryStorageUnitRepository = libraryStorageUnitDtoRepository;
        }

        public void Create(LibraryStorageUnitDTO libraryStorageUnitDto)
        {
            _libraryStorageUnitRepository.Add(Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(libraryStorageUnitDto));
            _libraryStorageUnitRepository.SaveChanges();
        }

        public void Delete(LibraryStorageUnitDTO libraryStorageUnitDto)
        {
            _libraryStorageUnitRepository.Delete(Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(libraryStorageUnitDto));
            _libraryStorageUnitRepository.SaveChanges();
        }

        public void Edit(LibraryStorageUnitDTO libraryStorageUnitDto)
        {
            libraryStorageUnitDto.UnitName = null;
            _libraryStorageUnitRepository.Edit(Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(libraryStorageUnitDto));
            _libraryStorageUnitRepository.SaveChanges();
        }

        public LibraryStorageUnitDTO FindById(int? id)
        {
            var libraryStorageUnit = _libraryStorageUnitRepository.FindById(id);
            return Mapper.Map<LibraryStorageUnit, LibraryStorageUnitDTO>(libraryStorageUnit);
        }

        public IEnumerable<LibraryStorageUnitDTO> Get()
        {
            return Mapper.Map<IEnumerable<LibraryStorageUnit>, List<LibraryStorageUnitDTO>>(_libraryStorageUnitRepository.GetWithInclude(u => u.Autor));
        }

        public LibraryStorageUnitDTO GetWithInclude(int? id)
        {
            var libraryStorageUnit = _libraryStorageUnitRepository.GetWithInclude(u => u.Autor).FirstOrDefault(b => b.Id == id); 
            return Mapper.Map<LibraryStorageUnit, LibraryStorageUnitDTO>(libraryStorageUnit);
        }

        public void SaveToFile(string connectionString)
        {
            _libraryStorageUnitRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}
