namespace SampleSoC.Framework.WebApi.DependencyInjection
{
    using System.Web.Http;
    using Owin;
    using SampleSoC.Framework.DependencyInjection;

    public static class IoCMiddleware
    {
        /// <summary>
        /// Uses the IoC middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="config">The configuration.</param>
        public static void UseIoCMiddleware(this IAppBuilder app, HttpConfiguration config)
        {
            app.UseAutofacMiddleware(IoC.Instance().Container);
            app.UseAutofacWebApi(config);
        }
    }
}
