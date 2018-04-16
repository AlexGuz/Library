using AutoMapper;
using Library.BLL.DTO;
using Library.WEB.ViewModels;

namespace Library.WEB
{
    public class AutomapperConfigurationWeb : Profile
    {
        public AutomapperConfigurationWeb()
        {
            CreateMap<AutorViewModel, AutorDTO>();
            CreateMap<AutorDTO, AutorViewModel>();

            CreateMap<BookViewModel, BookDTO>();
            CreateMap<BookDTO, BookViewModel>()
                .ForMember(view => view.Title, view => view.MapFrom(dto => dto.Unit.Title))
                .ForMember(view => view.Autor, view => view.MapFrom(dto => dto.Unit.Autor));

            CreateMap<BookViewModel, LibraryStorageUnitDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(view => view.UnitId))
                .ForMember(dto => dto.Title, dto => dto.MapFrom(view => view.Title))
                .ForMember(dto => dto.AutorId, dto => dto.MapFrom(view => view.Autor.Id));

            CreateMap<BrochureViewModel, BrochureDTO>();
            CreateMap<BrochureDTO, BrochureViewModel>()
                .ForMember(view => view.Title, view => view.MapFrom(dto => dto.Unit.Title))
                .ForMember(view => view.Autor, view => view.MapFrom(dto => dto.Unit.Autor));

            CreateMap<BrochureViewModel, LibraryStorageUnitDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(view => view.UnitId))
                .ForMember(dto => dto.Title, dto => dto.MapFrom(view => view.Title))
                .ForMember(dto => dto.AutorId, dto => dto.MapFrom(view => view.Autor.Id));

            CreateMap<LibraryStorageUnitViewModel, LibraryStorageUnitDTO>();
            CreateMap<LibraryStorageUnitDTO, LibraryStorageUnitViewModel>()
                .ForMember(view => view.AutorName, view => view.MapFrom(dto => dto.Autor.AutorName));

            CreateMap<MagazineViewModel, MagazineDTO>();
            CreateMap<MagazineDTO, MagazineViewModel>()
                .ForMember(view => view.Title, view => view.MapFrom(dto => dto.Unit.Title))
                .ForMember(view => view.Autor, view => view.MapFrom(dto => dto.Unit.Autor));

            CreateMap<MagazineViewModel, LibraryStorageUnitDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(view => view.UnitId))
                .ForMember(dto => dto.Title, dto => dto.MapFrom(view => view.Title))
                .ForMember(dto => dto.AutorId, dto => dto.MapFrom(view => view.Autor.Id));

            CreateMap<NewspaperViewModel, NewspaperDTO>();
            CreateMap<NewspaperDTO, NewspaperViewModel>()
                .ForMember(view => view.Title, view => view.MapFrom(dto => dto.Unit.Title))
                .ForMember(view => view.Autor, view => view.MapFrom(dto => dto.Unit.Autor));

            CreateMap<NewspaperViewModel, LibraryStorageUnitDTO>()
                .ForMember(dto => dto.Id, dto => dto.MapFrom(view => view.UnitId))
                .ForMember(dto => dto.Title, dto => dto.MapFrom(view => view.Title))
                .ForMember(dto => dto.AutorId, dto => dto.MapFrom(view => view.Autor.Id));
        }
    }
}