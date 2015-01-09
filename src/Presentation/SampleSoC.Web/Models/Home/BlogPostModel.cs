namespace SampleSoC.Web.Models.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SampleSoC.Domain.Core.Models;

    public class BlogPostModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(150)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Markdown { get; set; }

        public BlogPostModel()
        {
            
        }

        public BlogPostModel(BlogPost blogPost)
        {
            Id = blogPost.Id;
            Name = blogPost.Name;
            DateCreated = blogPost.DateCreated;
            Markdown = blogPost.Markdown;
        }
    }
}
