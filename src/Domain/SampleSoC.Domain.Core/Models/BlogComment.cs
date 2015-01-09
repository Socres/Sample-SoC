namespace SampleSoC.Domain.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SampleSoC.Domain.Core.Models.Base;

    public class BlogComment : IdentityModelBase
    {
        public BlogComment()
        {
            DateCreated = DateTime.UtcNow;            
        }

        [Required]
        public virtual int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [StringLength(150)]
        [Required]
        public string Commenter { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
