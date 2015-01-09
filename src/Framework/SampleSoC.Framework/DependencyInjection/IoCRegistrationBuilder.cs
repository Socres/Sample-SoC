namespace SampleSoC.Framework.DependencyInjection
{
    using Autofac.Builder;

    public class IoCRegistrationBuilder<T>
    {
        private IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> _registration;

        /// <summary>
        /// Registers the specified registration.
        /// </summary>
        /// <param name="registration">The registration.</param>
        public void Register(IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration)
        {
            _registration = registration;
        }

        /// <summary>
        /// Creates an instance per lifetime scope.
        /// </summary>
        /// <returns></returns>
        public IoCRegistrationBuilder<T> PerLifetimeScope()
        {
            _registration.InstancePerLifetimeScope();
            return this;
        }
    }
}
