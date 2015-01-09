namespace SampleSoC.WebApi.AppStart
{
    using System.Web.Http;

    /// <summary>
    /// Class for configuring ASP.NET MVC routes.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the routes.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void SetupRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}