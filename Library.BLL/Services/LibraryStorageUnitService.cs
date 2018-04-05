using System;
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

        public void Create(LibraryStorageUnitDTO libraryStorageUnitFromWeb)
        {
            throw new NotSupportedException("The selected function is not available for this entity");
        }

        public void Delete(LibraryStorageUnitDTO libraryStorageUnitFromWeb)
        {
            var delUnit = _libraryStorageUnitRepository.Get().FirstOrDefault(u => u.Id == libraryStorageUnitFromWeb.Id);
            _libraryStorageUnitRepository.Delete(delUnit);
            _libraryStorageUnitRepository.SaveChanges();
        }

        public void Edit(LibraryStorageUnitDTO libraryStorageUnitFromWeb)
        {
            var libraryStorageUnitForEdit = _libraryStorageUnitRepository.Get().FirstOrDefault(u => u.Id == libraryStorageUnitFromWeb.Id);
            if (libraryStorageUnitForEdit != null)
            {
                libraryStorageUnitForEdit.Title = libraryStorageUnitFromWeb.Title;
                libraryStorageUnitForEdit.AutorId = libraryStorageUnitFromWeb.Autor.Id;
                libraryStorageUnitForEdit.Autor = Mapper.Map<AutorDTO, Autor>(libraryStorageUnitFromWeb.Autor);

                _libraryStorageUnitRepository.Edit(libraryStorageUnitForEdit);
                _libraryStorageUnitRepository.SaveChanges();
            }
        }

        public IEnumerable<LibraryStorageUnitDTO> Get()
        {
            var libraryStorageUnitList = _libraryStorageUnitRepository.GetWithInclude(u => u.Autor);
            var libraryStorageUnitForViewList =
                Mapper.Map<IEnumerable<LibraryStorageUnit>, List<LibraryStorageUnitDTO>>(libraryStorageUnitList);

            return libraryStorageUnitForViewList;
        }

        public LibraryStorageUnitDTO GetWithInclude(int? id)
        {
            var libraryStorageUnit = _libraryStorageUnitRepository.GetWithInclude(u => u.Autor).FirstOrDefault(b => b.Id == id);
            var libraryStorageUnitForView = Mapper.Map<LibraryStorageUnit, LibraryStorageUnitDTO>(libraryStorageUnit);
            return libraryStorageUnitForView;
        }

        public void SaveToFile(string connectionString)
        {
            _libraryStorageUnitRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}