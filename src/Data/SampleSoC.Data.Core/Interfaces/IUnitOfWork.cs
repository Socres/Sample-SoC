namespace SampleSoC.Data.Core.Interfaces
{
    using SampleSoC.Domain.Core.Models;

    /// <summary>
    /// Represents the Unit Of Work pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the blog posts.
        /// </summary>
        IRepository<BlogPost> BlogPosts { get; }

        /// <summary>
        /// Gets the blog comments.
        /// </summary>
        IRepository<BlogComment> BlogComments { get; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Initializes this Database.
        /// </summary>
        void Initialize();
    }
}
