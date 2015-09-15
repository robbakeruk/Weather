using System.Net;
using Weather.Service.Core.Adapters;
using Weather.Service.Models;
using Weather.Service.Repository;
using Weather.Service.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Weather.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Weather.App_Start.NinjectWebCommon), "Stop")]

namespace Weather.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Adapters
            kernel.Bind<IJsonConvertAdapter>().To<JsonConvertAdapter>();
            kernel.Bind<IRestClientAdapter>().To<RestClientAdapter>();

            //Repositories
            kernel.Bind<IWeatherRepository>().To<WeatherRepository>();

            //Services
            kernel.Bind<IWeatherAggregatorService>().To<WeatherAggregatorService>();
            kernel.Bind<IWeatherUnitConverterService>().To<WeatherUnitConverterService>();
            kernel.Bind<IWeatherServiceStore>().ToConstant(GetRegisteredServices());
        }

        private static IWeatherServiceStore GetRegisteredServices()
        {
            var servicesStore = new WeatherServiceStore();

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "http://localhost:18888/api",
                Resource = "Weather/{location}"
            });

            servicesStore.RegisterService(new WeatherServiceDetail()
            {
                ServiceUrl = "http://localhost:17855/api",
                Resource = "Weather/{location}"
            });

            return servicesStore;
        }        
    }
}
