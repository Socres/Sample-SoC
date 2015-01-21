namespace SampleSoC.WebApi.AppStart
{
    using System.Web.Http;
    using Owin;
    using SampleSoC.Data;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Services;
    using SampleSoC.Framework.DependencyInjection;
    using SampleSoC.Framework.WebApi.DependencyInjection;

    /// <summary>
    /// Class for configuring IOC.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the unity Dependency Injection.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        public void SetupIoC(IAppBuilder app, HttpConfiguration config)
        {
            IoC.Instance.Register<IUnitOfWork, UnitOfWork>().PerLifetimeScope();
            IoC.Instance.Register<IBlogService, BlogService>().PerLifetimeScope();

            IoC.Instance.RegisterWebApi(GetType().Assembly);

            IoC.Instance.Build();

            //DependencyResolver.SetResolver(new IoCDependencyResolver());

            app.UseIoCMiddleware(config);
        }
    }
}