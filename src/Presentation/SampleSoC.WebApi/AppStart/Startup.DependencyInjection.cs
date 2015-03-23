namespace SampleSoC.WebApi.AppStart
{
    using System.Web.Http;
    using Owin;
    using SampleSoC.DI;
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
            var ioCBuilder = new IoCBuilder();
            ioCBuilder.Initialize(IoC.Instance);

            IoC.Instance.RegisterWebApi(GetType().Assembly);

            IoC.Instance.Build();

            config.DependencyResolver = new IoCDependencyResolver();

            app.UseIoCMiddleware(config);
        }
    }
}