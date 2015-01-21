namespace SampleSoC.Web.Tests.Models
{
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Web.Models.Home;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogPostSummaryModelTest
    {

        [Theory]
        [AutoFakeItEasyData]
        public void BlogPostSummaryModel_Constructor_CopiesValuesFromBlogPost(
            BlogPost blogPost)
        {
            // Arrange

            // Act
            var blogPostSummaryModel = new BlogPostSummaryModel(blogPost);

            // Assert
            Assert.Equal(blogPost.Id, blogPostSummaryModel.Id);
            Assert.Equal(blogPost.DateCreated, blogPostSummaryModel.DateCreated);
            Assert.Equal(blogPost.Name, blogPostSummaryModel.Name);
            Assert.Equal(blogPost.Markdown, blogPostSummaryModel.MarkdownPreview);
        }

    }
}
