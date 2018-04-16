﻿using System.Web.Http;
using Ninject;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Library.BLL.Infrastructure;
using Library.WEB.IoC;
using WebApiConfig = Library.WEB.ApiControllers.WebApiConfig;

namespace Library.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            Mapper.Initialize(m =>
            {
                m.AddProfile<AutomapperConfigurationWeb>();
                m.AddProfile<AutoMapperConfigurationBLL>();
            });
            
            NinjectModule serviceModuleWeb = new ServiceModuleWeb();
            NinjectModule serviceModule = new ServiceModule("Library");
            var kernel = new StandardKernel(serviceModule, serviceModuleWeb);
            var ninjectResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectResolver);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}