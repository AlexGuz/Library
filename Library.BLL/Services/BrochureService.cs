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
    public class BrochureService : IGenericServiceDTO<BrochureDTO>
    {
        private readonly IGenericRepository<Brochure> _brochureRepository;

        public BrochureService(IGenericRepository<Brochure> brochureRepository)
        {
            _brochureRepository = brochureRepository;
        }

        public void Create(BrochureDTO brochureFromWeb)
        {
            var brochureAdd = new Brochure()
            {
                Type = (BrochureType)Enum.Parse(typeof(BrochureType), brochureFromWeb.Type.ToString()),
                ReleaseDate = brochureFromWeb.ReleaseDate,
                UnitId = brochureFromWeb.Unit.Id,
                Unit = Mapper.Map<LibraryStorageUnitDTO, LibraryStorageUnit>(brochureFromWeb.Unit)
            };
            brochureAdd.Unit.UnitName = nameof(Brochure);
            brochureAdd.Unit.Autor = null;
            _brochureRepository.Add(brochureAdd);
            _brochureRepository.SaveChanges();
        }

        public void Delete(BrochureDTO brochureFromWeb)
        {
            var brochureForDelete = _brochureRepository.Get().FirstOrDefault(b => b.Id == brochureFromWeb.Id);
            _brochureRepository.Delete(brochureForDelete);
            _brochureRepository.SaveChanges();
        }

        public void Edit(BrochureDTO brochureFromWeb)
        {
            var bookForEdit = _brochureRepository.Get().FirstOrDefault(u => u.Id == brochureFromWeb.Id);
            if (bookForEdit == null)
            {
                throw new ObjectNotFoundException();
            }
            bookForEdit.Type = (BrochureType)Enum.Parse(typeof(BrochureType), brochureFromWeb.Type.ToString());
            bookForEdit.ReleaseDate = brochureFromWeb.ReleaseDate;

            _brochureRepository.Edit(bookForEdit);
            _brochureRepository.SaveChanges();
        }

        public IEnumerable<BrochureDTO> Get()
        {
            var brochureList = _brochureRepository.GetWithInclude(b => b.Unit.Autor);
            var brochureListForWeb = Mapper.Map<IEnumerable<Brochure>, List<BrochureDTO>>(brochureList);
            return brochureListForWeb;
        }

        public BrochureDTO GetWithInclude(int? id)
        {
            var brochure = _brochureRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            var brochureListForWeb = Mapper.Map<Brochure, BrochureDTO>(brochure);
            return brochureListForWeb;
        }

        public void SaveToFile(string connectionString)
        {
            _brochureRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}