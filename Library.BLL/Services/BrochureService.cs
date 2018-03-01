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
    public class BrochureService : IGenericServiceDTO<BrochureDTO>
    {
        private readonly IGenericRepository<Brochure> _brochureRepository;

        public BrochureService(IGenericRepository<Brochure> brochureDtoRepository)
        {
            _brochureRepository = brochureDtoRepository;
        }

        public void Create(BrochureDTO brochureDto)
        {
            brochureDto.Unit.UnitName = nameof(Brochure);
            _brochureRepository.Add(Mapper.Map<BrochureDTO, Brochure>(brochureDto));
            _brochureRepository.SaveChanges();
        }

        public void Delete(BrochureDTO brochureDto)
        {
            var delBrochure = _brochureRepository.Get().FirstOrDefault(b => b.Id == brochureDto.Id);
            _brochureRepository.Delete(delBrochure);
            _brochureRepository.SaveChanges();
        }

        public void Edit(BrochureDTO brochureDto)
        {
            _brochureRepository.Edit(Mapper.Map<BrochureDTO, Brochure>(brochureDto));
            _brochureRepository.SaveChanges();
        }

        public BrochureDTO FindById(int? id)
        {
            var brochure = _brochureRepository.FindById(id);
            return Mapper.Map<Brochure, BrochureDTO>(brochure);
        }

        public IEnumerable<BrochureDTO> Get()
        {
            return Mapper.Map<IEnumerable<Brochure>, List<BrochureDTO>>(_brochureRepository.GetWithInclude(b => b.Unit.Autor));
        }

        public BrochureDTO GetWithInclude(int? id)
        {
            var brochure = _brochureRepository.GetWithInclude(b => b.Unit.Autor, b => b.Unit.Title).FirstOrDefault(b => b.Id == id);
            return Mapper.Map<Brochure, BrochureDTO>(brochure);
        }

        public void SaveToFile(string connectionString)
        {
            _brochureRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}