namespace SampleSoC.Web.AppStart
{
    using System.Web.Mvc;
    using Owin;
    using SampleSoC.Data;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Services;
    using SampleSoC.Framework.DependencyInjection;
    using SampleSoC.Framework.Mvc.DependencyInjection;

    /// <summary>
    /// Class for configuring IOC.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the unity Dependency Injection.
        /// </summary>
        /// <param name="app"></param>
        public void SetupIoC(IAppBuilder app)
        {
            IoC.Instance().Register<IUnitOfWork, UnitOfWork>().PerLifetimeScope();
            IoC.Instance().Register<IBlogService, BlogService>().PerLifetimeScope();

            IoC.Instance().RegisterMvc(GetType().Assembly);

            IoC.Instance().Build();

            DependencyResolver.SetResolver(new IoCDependencyResolver());

            app.UseIoCMiddleware();
        }
    }
}