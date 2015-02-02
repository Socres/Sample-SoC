namespace SampleSoC.WebApi.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.WebApi.Controllers;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogControllerTest
    {

        [Theory]
        [AutoFakeItEasyData]
        public void BlogController_Get_ReturnsCorrectResult(
            IBlogService blogService,
            int blogPostId,
            List<BlogComment> blogComments)
        {
            // Arrange
            A.CallTo(() => blogService.GetComments(blogPostId))
                .Returns(blogComments);
            var blogController = new BlogController(blogService);

            // Act
            var actual = blogController.Get(blogPostId);

            // Assert
            A.CallTo(() => blogService.GetComments(A<int>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(actual);
            Assert.Equal(blogComments.Count, actual.Count());
        }

    }
}
