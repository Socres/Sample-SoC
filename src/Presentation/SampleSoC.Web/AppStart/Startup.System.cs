namespace SampleSoC.Web.AppStart
{
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Framework.DependencyInjection;

    /// <summary>
    /// Class for startup functionality.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initializes the system.
        /// </summary>
        public void InitializeSystem()
        {
            var unitOfWork = IoC.Instance().Resolve<IUnitOfWork>();
            unitOfWork.Initialize();
        }
    }
}