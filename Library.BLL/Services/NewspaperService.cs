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
    public class NewspaperService : IGenericServiceDTO<NewspaperDTO>
    {
        private readonly IGenericRepository<Newspaper> _newspaperRepository;

        public NewspaperService(IGenericRepository<Newspaper> newspaperRepository)
        {
            _newspaperRepository = newspaperRepository;
        }

        public void Create(NewspaperDTO newspaperFromWeb)
        {
            var newspaperForAdd = new Newspaper()
            {
                Type = (NewspaperType)Enum.Parse(typeof(NewspaperType), newspaperFromWeb.Type.ToString()),
                ReleaseDate = newspaperFromWeb.ReleaseDate,
                UnitId = newspaperFromWeb.Unit.Id,
                Unit = Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(newspaperFromWeb.Unit)
            };
            newspaperForAdd.Unit.UnitName = nameof(Newspaper);
            newspaperForAdd.Unit.Autor = null;
            _newspaperRepository.Add(newspaperForAdd);
            _newspaperRepository.SaveChanges();
        }

        public void Delete(NewspaperDTO newspaperFromWeb)
        {
            var newspaperForDelete = _newspaperRepository.Get().FirstOrDefault(n => n.Id == newspaperFromWeb.Id);
            _newspaperRepository.Delete(newspaperForDelete);
            _newspaperRepository.SaveChanges();
        }

        public void Edit(NewspaperDTO newspaperFromWeb)
        {
            var newspaperForEdit = _newspaperRepository.Get().FirstOrDefault(u => u.Id == newspaperFromWeb.Id);
            if (newspaperForEdit == null)
            {
                throw new ObjectNotFoundException();
            }
            newspaperForEdit.Type = (NewspaperType)Enum.Parse(typeof(NewspaperType), newspaperFromWeb.Type.ToString());
            newspaperForEdit.ReleaseDate = newspaperFromWeb.ReleaseDate;

            _newspaperRepository.Edit(newspaperForEdit);
            _newspaperRepository.SaveChanges();
        }

        public IEnumerable<NewspaperDTO> Get()
        {
            var newspaperList = _newspaperRepository.GetWithInclude(b => b.Unit.Autor);
            var newspaperListForWeb = Mapper.Map<IEnumerable<Newspaper>, List<NewspaperDTO>>(newspaperList);
            return newspaperListForWeb;
        }

        public NewspaperDTO GetWithInclude(int? id)
        {
            var newspaper = _newspaperRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            var newspaperForWeb = Mapper.Map<Newspaper, NewspaperDTO>(newspaper);
            return newspaperForWeb;
        }

        public void SaveToFile(string connectionString)
        {
            _newspaperRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}