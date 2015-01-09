namespace SampleSoC.Framework.Mvc.DependencyInjection
{
    using Owin;
    using SampleSoC.Framework.DependencyInjection;

    public static class IoCMiddleware
    {
        /// <summary>
        /// Uses the IoC middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void UseIoCMiddleware(this IAppBuilder app)
        {
            app.UseAutofacMiddleware(IoC.Instance().Container);
            app.UseAutofacMvc();
        }
    }
}
