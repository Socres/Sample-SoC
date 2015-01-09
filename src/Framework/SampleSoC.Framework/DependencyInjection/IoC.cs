namespace SampleSoC.Framework.DependencyInjection
{
    using System;
    using Autofac;

    public class IoC
    {
        private static readonly Lazy<IoC> LazyIoC = new Lazy<IoC>(
            () => new IoC(),
            true);

        internal readonly ContainerBuilder Builder;
        internal IContainer Container;

        /// <summary>
        /// Prevents a default instance of the <see cref="IoC"/> class from being created.
        /// </summary>
        private IoC()
        {
            Builder = new ContainerBuilder();
        }

        /// <summary>
        /// Instances this instance.
        /// </summary>
        public static IoC Instance()
        {
            return LazyIoC.Value;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        public void Build()
        {
            Container = Builder.Build();
        }

        /// <summary>
        /// Registers the specified implementation type.
        /// </summary>
        /// <param name="implementationType">Type of the implementation.</param>
        public IoCRegistrationBuilder<object> Register(Type implementationType)
        {
            var result = new IoCRegistrationBuilder<object>();
            Builder.RegisterType(implementationType);
            return result;
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns></returns>
        public IoCRegistrationBuilder<TImplementation> Register<TService, TImplementation>()
        {
            var result = new IoCRegistrationBuilder<TImplementation>();
            result.Register(Builder.RegisterType<TImplementation>().As<TService>());
            return result;
        }

        /// <summary>
        /// Determines whether the specified service type is registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>()
        {
            return Container.IsRegistered<T>();
        }

        /// <summary>
        /// Determines whether the specified service type is registered.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType)
        {
            return Container.IsRegistered(serviceType);
        }

        /// <summary>
        /// Resolves the specified service type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object Resolve(Type serviceType)
        {
            return Container.Resolve(serviceType);
        }
    }
}
