using Library.BLL.Services;
using Ninject.Modules;

namespace Library.WEB.Utill
{
    public class ServiceModuleWeb : NinjectModule
    {
        public override void Load()
        {
            Bind<AutorService>().To<AutorService>();
            Bind<BookService>().To<BookService>();
            Bind<BrochureService>().To<BrochureService>();
            Bind<LibraryStorageUnitService>().To<LibraryStorageUnitService>();
            Bind<MagazineService>().To<MagazineService>();
            Bind<NewspaperService>().To<NewspaperService>();
        }
    }
}