namespace SampleSoC.Web.Tests.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Web.Models.Home;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogsOverviewModelTest
    {

        [Theory]
        [AutoFakeItEasyData]
        public void BlogsOverviewModel_Constructor_CopiesValuesFromBlogPost(
            List<BlogPost> blogPosts)
        {
            // Arrange

            // Act
            var blogsOverviewModel = new BlogsOverviewModel(blogPosts);

            // Assert
            Assert.NotNull(blogsOverviewModel.Posts);
            Assert.Equal(blogPosts.Count, blogsOverviewModel.Posts.Count());
        }

    }
}
