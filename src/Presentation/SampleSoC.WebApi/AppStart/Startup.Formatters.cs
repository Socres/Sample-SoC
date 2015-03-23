namespace SampleSoC.WebApi.AppStart
{
    using System.Net.Http.Formatting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Class for configuring formatters.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the web API formaters.
        /// </summary>
        /// <param name="formatters">The formatters.</param>
        public void SetupWebApiFormatters(MediaTypeFormatterCollection formatters)
        {
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}