namespace SampleSoC.DI
{
    using SampleSoC.Data;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Services;
    using SampleSoC.Framework.DependencyInjection;

    public class IoCBuilder
    {
        public void Initialize(IoC ioc)
        {
            IoC.Instance.Register<IUnitOfWork, UnitOfWork>().PerLifetimeScope();
            IoC.Instance.Register<IBlogService, BlogService>().PerLifetimeScope();
        }
    }
}
