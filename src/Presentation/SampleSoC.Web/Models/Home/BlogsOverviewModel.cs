namespace SampleSoC.Web.Models.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using SampleSoC.Domain.Core.Models;

    public class BlogsOverviewModel
    {
        public IEnumerable<BlogPostSummaryModel> Posts { get; set; }

        public BlogsOverviewModel(IEnumerable<BlogPost> blogPosts)
        {
            Posts = new List<BlogPostSummaryModel>();
            ((List<BlogPostSummaryModel>) Posts).AddRange(
                blogPosts.Select(blogPost => 
                    new BlogPostSummaryModel(blogPost)));
        }
    }
}
