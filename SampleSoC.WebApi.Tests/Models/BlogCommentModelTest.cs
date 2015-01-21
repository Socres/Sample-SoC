namespace SampleSoC.WebApi.Tests.Models
{
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.WebApi.Models;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogCommentModelTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void BlogCommentModel_Constructor_CopiesValuesFromBlogComment(
            BlogComment blogComment)
        {
            // Arrange

            // Act
            var blogCommentModel = new BlogCommentModel(blogComment);

            // Assert
            Assert.Equal(blogComment.DateCreated, blogCommentModel.DateCreated);
            Assert.Equal(blogComment.Commenter, blogCommentModel.Commenter);
            Assert.Equal(blogComment.Message, blogCommentModel.Message);
        }

    }
}
