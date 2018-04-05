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

        public AutorService(IGenericRepository<Autor> autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public void Create(AutorDTO autorFromWeb)
        {
            var newAutor = Mapper.Map<AutorDTO, Autor>(autorFromWeb);
            _autorRepository.Add(newAutor);
            _autorRepository.SaveChanges();
        }

        public void Delete(AutorDTO autorFromWeb)
        {
            var autorForDelete = Mapper.Map<AutorDTO, Autor>(autorFromWeb);
            _autorRepository.Delete(autorForDelete);
            _autorRepository.SaveChanges();
        }

        public void Edit(AutorDTO autorFromWeb)
        {
            var autorForEdit = Mapper.Map<AutorDTO, Autor>(autorFromWeb);
            _autorRepository.Edit(autorForEdit);
            _autorRepository.SaveChanges();
        }

        public IEnumerable<AutorDTO> Get()
        {
            var autorsList = _autorRepository.Get();
            var autorsForViewList = Mapper.Map<IEnumerable<Autor>, List<AutorDTO>>(autorsList)
                .Select(
                    a =>
                        new AutorDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Surname = a.Surname,
                            FoundingDate = a.FoundingDate != 0 ? a.FoundingDate : null
                        }).ToList();

            return autorsForViewList;
        }

        public AutorDTO GetWithInclude(int? id)
        {
            var autor = _autorRepository.GetWithInclude(a => a.Units).FirstOrDefault(a => a.Id == id);
            var autorForView = Mapper.Map<Autor, AutorDTO>(autor);
            return autorForView;
        }

        public void SaveToFile(string connectionString)
        {
            _autorRepository.Get().ToList().ToXMLFile(connectionString);
        }
    }
}