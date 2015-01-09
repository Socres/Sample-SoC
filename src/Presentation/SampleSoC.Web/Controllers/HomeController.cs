namespace SampleSoC.Web.Controllers
{
    using System.Web.Mvc;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Web.Models.Home;

    public class HomeController : Controller
    {
        private readonly IBlogService _blogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="blogService">The blog service.</param>
        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// Shows the overview of all BlogPosts
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var blogPosts = _blogService.GetPosts();
            var model = new BlogsOverviewModel(blogPosts);
            return View(model);
        }

        /// <summary>
        /// Shows the details of a BlogPost
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Post(int id)
        {
            var blogPost = _blogService.GetPost(id);
            var model = new BlogPostModel(blogPost);
            return View(model);
        }

        /// <summary>
        /// Edit the details of a BlogPost
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var blogPost = _blogService.GetPost(id);
            var model = new BlogPostModel(blogPost);
            return View(model);
        }

        /// <summary>
        /// Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(BlogPostModel model)
        {
            if (ModelState.IsValid)
            {
                _blogService.UpdatePost(model.Id, model.Name, model.Markdown);
                return RedirectToAction("Post", new { model.Id });
            }

            return View(model);
        }

    }
}