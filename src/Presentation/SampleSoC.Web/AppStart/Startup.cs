using Microsoft.Owin;
using SampleSoC.Web.AppStart;

[assembly: OwinStartup(typeof(Startup))]

namespace SampleSoC.Web.AppStart
{
    using System.Web.Mvc;
    using System.Web.Routing;
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
            AreaRegistration.RegisterAllAreas();

            SetupBundles();
            SetupGlobalMvcFilters(GlobalFilters.Filters);
            SetupRoutes(RouteTable.Routes);

            SetupIoC(app);

            InitializeSystem();
        }
    }
}