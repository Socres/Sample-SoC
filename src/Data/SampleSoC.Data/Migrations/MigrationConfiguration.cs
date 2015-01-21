namespace SampleSoC.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SampleSoC.Data.Context;
    using SampleSoC.Domain.Core.Models;

    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<SampleSoCContext>
    {
        private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public MigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SampleSoCContext context)
        {
            var startDate = DateTime.UtcNow.AddDays(new Random().Next(30, 100) * -1);
            context.BlogPosts.AddOrUpdate(
                bp => bp.Name,
                CreateBlogPost(startDate, 1),
                CreateBlogPost(startDate, 2),
                CreateBlogPost(startDate, 3));
        }

        private static BlogPost CreateBlogPost(DateTime startDate, int identifier)
        {
            var name = "Sample BlogPost " + identifier;
            var blogPost = new BlogPost
            {
                DateCreated = startDate.AddDays(identifier * -1),
                Name = name,
                Markdown =
                    "This is a " + name + Environment.NewLine + Environment.NewLine +
                    "* [Bing][bing-url]" + Environment.NewLine + Environment.NewLine +
                    "* [Google][google-url]" + Environment.NewLine + Environment.NewLine +
                    "* [Yahoo][yahoo-url]" + Environment.NewLine + Environment.NewLine +
                    "[bing-url]: http://www.bing.com" + Environment.NewLine + Environment.NewLine +
                    "[google-url]: http://www.google.com" + Environment.NewLine + Environment.NewLine +
                    "[yahoo-url]: http://www.yahoo.com" + Environment.NewLine + Environment.NewLine +
                    "# Header 1" + Environment.NewLine + Environment.NewLine +
                    String.Join(Environment.NewLine, Enumerable.Repeat(LoremIpsum, 4)) + Environment.NewLine + Environment.NewLine +
                    "## Header 2" + Environment.NewLine + Environment.NewLine +
                    String.Join(Environment.NewLine, Enumerable.Repeat(LoremIpsum, 3)) + Environment.NewLine + Environment.NewLine +
                    "### Header 3" + Environment.NewLine + Environment.NewLine +
                    String.Join(Environment.NewLine, Enumerable.Repeat(LoremIpsum, 2))
            };

            blogPost.BlogComments = CreateBlogComments(blogPost);

            return blogPost;
        }

        private static ICollection<BlogComment> CreateBlogComments(BlogPost blogPost)
        {
            var comments = new Collection<BlogComment>
            {
                new BlogComment
                {
                    DateCreated = blogPost.DateCreated.AddMinutes(new Random().Next(15, 99999)),
                    Commenter = "Anonymous",
                    Message = "Great post!"
                },
                new BlogComment
                {
                    DateCreated = blogPost.DateCreated.AddMinutes(new Random().Next(15, 99999)),
                    Commenter = "Anonymous",
                    Message = "Awesome!"
                },
                new BlogComment
                {
                    DateCreated = blogPost.DateCreated.AddMinutes(new Random().Next(15, 99999)),
                    Commenter = "Anonymous",
                    Message = "Terrible! Ridiculous..nonsense.."
                }
            };

            return comments;
        }
    }
}
