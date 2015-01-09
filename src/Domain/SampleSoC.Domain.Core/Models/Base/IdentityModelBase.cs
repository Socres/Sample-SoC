namespace SampleSoC.Domain.Core.Models.Base
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base class for domain models with an ID.
    /// </summary>
    public abstract class IdentityModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [Key]
        public int Id { get; set; }
    }
}