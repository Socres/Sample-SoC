namespace SampleSoC.Web.AppStart
{
    using System.Web.Mvc;
    using Owin;
    using SampleSoC.DI;
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
            var ioCBuilder = new IoCBuilder();
            ioCBuilder.Initialize(IoC.Instance);

            IoC.Instance.RegisterMvc(GetType().Assembly);

            IoC.Instance.Build();

            DependencyResolver.SetResolver(new IoCDependencyResolver());

            app.UseIoCMiddleware();
        }
    }
}