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
    public class MagazineService : IGenericServiceDTO<MagazineDTO>
    {
        private readonly IGenericRepository<Magazine> _magazineRepository;

        public MagazineService(IGenericRepository<Magazine> magazineRepository)
        {
            _magazineRepository = magazineRepository;
        }

        public void Create(MagazineDTO magazineFromWeb)
        {
            var magazineForAdd = new Magazine()
            {
                Style = (StylesOfPublications)Enum.Parse(typeof(StylesOfPublications), magazineFromWeb.Style.ToString()),
                ReleaseDate = magazineFromWeb.ReleaseDate,
                UnitId = magazineFromWeb.Unit.Id,
                Unit = Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(magazineFromWeb.Unit)
            };
            magazineForAdd.Unit.UnitName = nameof(Magazine);
            magazineForAdd.Unit.Autor = null;
            _magazineRepository.Add(magazineForAdd);
            _magazineRepository.SaveChanges();
        }

        public void Delete(MagazineDTO magazineFromWeb)
        {
            var magazineForDelete = _magazineRepository.Get().FirstOrDefault(n => n.Id == magazineFromWeb.Id);
            _magazineRepository.Delete(magazineForDelete);
            _magazineRepository.SaveChanges();
        }

        public void Edit(MagazineDTO magazineFromWeb)
        {
            var magazineForEdit = _magazineRepository.Get().FirstOrDefault(u => u.Id == magazineFromWeb.Id);
            if (magazineForEdit == null)
            {
                throw new ObjectNotFoundException();
            }
            magazineForEdit.Style = (StylesOfPublications)Enum.Parse(typeof(StylesOfPublications), magazineFromWeb.Style.ToString());
            magazineForEdit.ReleaseDate = magazineFromWeb.ReleaseDate;

            _magazineRepository.Edit(magazineForEdit);
            _magazineRepository.SaveChanges();
        }

        public IEnumerable<MagazineDTO> Get()
        {
            var magazineList = _magazineRepository.GetWithInclude(b => b.Unit.Autor);
            var magazineListForWeb = Mapper.Map<IEnumerable<Magazine>, List<MagazineDTO>>(magazineList);
            return magazineListForWeb;
        }

        public MagazineDTO GetWithInclude(int? id)
        {
            var magazine = _magazineRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            var magazineForWeb = Mapper.Map<Magazine, MagazineDTO>(magazine);
            return magazineForWeb;
        }

        public void SaveToFile(string connectionString)
        {
            _magazineRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}