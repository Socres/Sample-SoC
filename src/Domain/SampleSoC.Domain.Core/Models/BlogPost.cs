namespace SampleSoC.Domain.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SampleSoC.Domain.Core.Models.Base;

    public class BlogPost : IdentityModelBase
    {
        public BlogPost()
        {
            DateCreated = DateTime.UtcNow;
        }

        [StringLength(150)]
        [Required(AllowEmptyStrings=false)]
        public string Name { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Markdown { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }
    }
}
