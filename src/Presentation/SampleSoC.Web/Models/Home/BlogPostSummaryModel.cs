namespace SampleSoC.Web.Models.Home
{
    using System;
    using SampleSoC.Domain.Core.Models;

    public class BlogPostSummaryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MarkdownPreview { get; set; }

        public DateTime DateCreated { get; set; }

        public BlogPostSummaryModel(BlogPost blogPost)
        {
            Id = blogPost.Id;
            Name = blogPost.Name;
            DateCreated = blogPost.DateCreated;
            MarkdownPreview = blogPost.Markdown;
        }

    }
}
