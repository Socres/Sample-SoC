using Microsoft.Owin;
using SampleSoC.WebApi.AppStart;

[assembly: OwinStartup(typeof(Startup))]

namespace SampleSoC.WebApi.AppStart
{
    using System.Web.Http;
    using Owin;

    /// <summary>
    /// Owin Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration(); 

            SetupRoutes(config);

            SetupIoC(app, config);

            InitializeSystem();

            app.UseWebApi(config);
        }
    }
}