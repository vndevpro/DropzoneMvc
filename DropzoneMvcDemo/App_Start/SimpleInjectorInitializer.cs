using DropzoneMvcDemo.App_Start;
using GdNet.Integrations.DropzoneMvc.Controllers;
using log4net;
using Rabbit.IOC;
using SimpleInjector.Packaging;
using System;
using System.Linq;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]

namespace DropzoneMvcDemo.App_Start
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using System.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SimpleInjectorInitializer));

        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            try
            {
                //SerializationContext.Current.Initialize(new CustomDataContractJsonStrategy());

                ConfigureInjector();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString());
                throw;
            }
        }

        private static void ConfigureInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(typeof(SimpleInjectorInitializer).Assembly);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            ModuleHelper.GetModuleTypes(typeof(SimpleInjectorInitializer).Assembly)
                .CreateModules()
                .Cast<IPackage>()
                .ToList()
                .ForEach(x => x.RegisterServices(container));
        }
    }
}