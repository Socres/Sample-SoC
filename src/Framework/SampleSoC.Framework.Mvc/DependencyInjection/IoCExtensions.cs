namespace SampleSoC.Framework.Mvc.DependencyInjection
{
    using System.Reflection;
    using Autofac;
    using Autofac.Integration.Mvc;
    using SampleSoC.Framework.DependencyInjection;

    /// <summary>
    /// Extensions for IoC
    /// </summary>
    public static class IoCExtensions
    {
        public static void RegisterMvc(this IoC ioc, Assembly assembly)
        {
            ioc.Builder.RegisterControllers(assembly);
            ioc.Builder.RegisterModelBinders(assembly);
            ioc.Builder.RegisterModelBinderProvider();
            ioc.Builder.RegisterModule<AutofacWebTypesModule>();
        }
    }
}
