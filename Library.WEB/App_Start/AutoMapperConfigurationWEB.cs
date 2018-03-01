using AutoMapper;
using Library.BLL.DTO;
using Library.WEB.Models;

namespace Library.WEB
{
    public class AutomapperConfigurationWEB : Profile
    {
        public AutomapperConfigurationWEB()
        {
            CreateMap<AutorViewModel, AutorDTO>();
            CreateMap<AutorDTO, AutorViewModel>();
            CreateMap<BookViewModel, BookDTO>();
            CreateMap<BookDTO, BookViewModel>();
            CreateMap<BrochureViewModel, BrochureDTO>();
            CreateMap<BrochureDTO, BrochureViewModel>();
            CreateMap<LibraryStorageUnitViewModel, LibraryStorageUnitDTO>();
            CreateMap<LibraryStorageUnitDTO, LibraryStorageUnitViewModel>();
            CreateMap<MagazineViewModel, MagazineDTO>();
            CreateMap<MagazineDTO, MagazineViewModel>();
            CreateMap<NewspaperViewModel, NewspaperDTO>();
            CreateMap<NewspaperDTO, NewspaperViewModel>();
        }
    }
}