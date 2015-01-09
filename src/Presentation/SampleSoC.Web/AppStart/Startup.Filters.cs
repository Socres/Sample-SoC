namespace SampleSoC.Web.AppStart
{
    using System.Web.Mvc;
    using Socres.Web.Mvc.FilterAttributes;

    /// <summary>
    /// Class for configuring global filters.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the global MVC filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public void SetupGlobalMvcFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureBasedActionAttribute());
        }
    }
}