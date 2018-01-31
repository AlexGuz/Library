using Ninject;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Library.BLL.Infrastructure;
using Library.WEB.Utill;
using Ninject.Web.Mvc;

namespace Library.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            Mapper.Initialize(m =>
            {
                m.AddProfile<AutomapperConfigurationWEB>();
                m.AddProfile<AutoMapperConfigurationBLL>();
            });
            

            NinjectModule serviceModuleWeb = new ServiceModuleWeb();
            NinjectModule serviceModule = new ServiceModule("Library");
            var kernel = new StandardKernel(serviceModule, serviceModuleWeb);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}