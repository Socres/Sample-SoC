namespace SampleSoC.Web.Tests.Models
{
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Web.Models.Home;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogPostModelTest
    {

        [Theory]
        [AutoFakeItEasyData]
        public void BlogPostModel_Constructor_CopiesValuesFromBlogPost(
            BlogPost blogPost)
        {
            // Arrange

            // Act
            var blogPostModel = new BlogPostModel(blogPost);

            // Assert
            Assert.Equal(blogPost.Id, blogPostModel.Id);
            Assert.Equal(blogPost.DateCreated, blogPostModel.DateCreated);
            Assert.Equal(blogPost.Name, blogPostModel.Name);
            Assert.Equal(blogPost.Markdown, blogPostModel.Markdown);
        }

    }
}
