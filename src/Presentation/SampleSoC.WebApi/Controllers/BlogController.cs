namespace SampleSoC.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.WebApi.Models;

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
        /// <param name="id">The blog identifier.</param>
        /// <returns></returns>
        public IEnumerable<BlogCommentModel> Get(int id)
        {
            return _blogService.GetComments(id)
                .Select(bc => new BlogCommentModel(bc));
        }
    }
}
