namespace SampleSoC.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Core.Models;

    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="blogService">The blog service.</param>
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="blogId">The blog identifier.</param>
        /// <returns></returns>
        public IEnumerable<BlogComment> Get(int blogId)
        {
            return _blogService.GetComments(blogId);
        }
    }
}
