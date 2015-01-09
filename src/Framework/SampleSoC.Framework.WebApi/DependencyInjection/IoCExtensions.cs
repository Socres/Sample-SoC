namespace SampleSoC.Framework.WebApi.DependencyInjection
{
    using System.Reflection;
    using Autofac.Integration.WebApi;
    using SampleSoC.Framework.DependencyInjection;

    /// <summary>
    /// Extensions for IoC
    /// </summary>
    public static class IoCExtensions
    {
        public static void RegisterWebApi(this IoC ioc, Assembly assembly)
        {
            ioc.Builder.RegisterApiControllers(assembly);
        }
    }
}
