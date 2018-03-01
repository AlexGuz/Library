using AutoMapper;
using Library.BLL.DTO;
using Library.DAL.Models;

namespace Library.BLL.Infrastructure
{
    public class AutoMapperConfigurationBLL : Profile
    {
        public AutoMapperConfigurationBLL()
        {
            CreateMap<Autor, AutorDTO>();
            CreateMap<AutorDTO, Autor>();
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<Brochure, BrochureDTO>();
            CreateMap<BrochureDTO, Brochure>();
            CreateMap<LibraryStorageUnit, LibraryStorageUnitDTO>();
            CreateMap<LibraryStorageUnitDTO, LibraryStorageUnit>();
            CreateMap<Magazine, MagazineDTO>();
            CreateMap<MagazineDTO, Magazine>();
            CreateMap<Newspaper, NewspaperDTO>();
            CreateMap<NewspaperDTO, Newspaper>();
        }
    }
}