namespace SampleSoC.Domain.Core.Interfaces
{
    using System.Collections.Generic;
    using SampleSoC.Domain.Core.Models;

    public interface IBlogService
    {
        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BlogPost> GetPosts();

        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        BlogPost GetPost(int id);

        /// <summary>
        /// Updates the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="markdown">The markdown.</param>
        void UpdatePost(int id, string name, string markdown);

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns></returns>
        IEnumerable<BlogComment> GetComments(int postId);
    }
}
