using Library.DAL;
using Ninject.Modules;
using Library.DAL.Interfaces;
using Library.DAL.Repositories;
using System.Data.Entity;

namespace Library.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule 
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind(typeof (IGenericRepository<>)).To(typeof (LibraryRepository<>)).WithConstructorArgument(connectionString);
            Bind<DbContext>().To<LibraryDBContext>();
        }
    }
}
