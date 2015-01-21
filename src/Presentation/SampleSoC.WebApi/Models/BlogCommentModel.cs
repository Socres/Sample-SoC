namespace SampleSoC.WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SampleSoC.Domain.Core.Models;

    public class BlogCommentModel
    {
        public DateTime DateCreated { get; set; }

        [StringLength(150)]
        [Required]
        public string Commenter { get; set; }

        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogCommentModel"/> class.
        /// </summary>
        /// <param name="blogComment">The blog comment.</param>
        public BlogCommentModel(BlogComment blogComment)
        {
            DateCreated = blogComment.DateCreated;
            Commenter = blogComment.Commenter;
            Message = blogComment.Message;
        }
    }
}
