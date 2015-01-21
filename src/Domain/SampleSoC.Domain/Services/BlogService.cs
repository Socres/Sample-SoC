namespace SampleSoC.Domain.Services
{
    using System.Collections.Generic;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Core.Models;

    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogPost> GetPosts()
        {
            return _unitOfWork.BlogPosts.Fetch(
                bp => bp.DateCreated,
                bp => bp.Id > 0);
        }

        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BlogPost GetPost(int id)
        {
            return _unitOfWork.BlogPosts.GetById(id);
        }

        /// <summary>
        /// Updates the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="markdown">The markdown.</param>
        public void UpdatePost(int id, string name, string markdown)
        {
            var post = _unitOfWork.BlogPosts.GetById(id);
            post.Name = name;
            post.Markdown = markdown;
            _unitOfWork.Save();
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns></returns>
        public IEnumerable<BlogComment> GetComments(int postId)
        {
            return _unitOfWork.BlogComments.Fetch(
                bc => bc.DateCreated,
                bc => bc.BlogPostId == postId);
        }
    }
}
