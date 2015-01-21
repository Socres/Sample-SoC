namespace SampleSoC.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using FakeItEasy;
    using SampleSoC.Domain.Core.Interfaces;
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Web.Controllers;
    using SampleSoC.Web.Models.Home;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class HomeControllerTest
    {

        [Theory]
        [AutoFakeItEasyData]
        public void HomeController_Index_ReturnsCorrectView(
            IBlogService blogService,
            IEnumerable<BlogPost> blogPosts)
        {
            // Arrange
            A.CallTo(() => blogService.GetPosts())
                .Returns(blogPosts);
            var homeController = new HomeController(blogService);

            // Act
            var actual = homeController.Index();

            // Assert
            A.CallTo(() => blogService.GetPosts())
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(actual);
            Assert.IsType<ViewResult>(actual);
            Assert.IsType<BlogsOverviewModel>(((ViewResult)actual).Model);
            Assert.NotEmpty(((BlogsOverviewModel)((ViewResult)actual).Model).Posts);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void HomeController_Post_ReturnsCorrectView(
            IBlogService blogService,
            int blogPostId,
            BlogPost blogPost)
        {
            // Arrange
            A.CallTo(() => blogService.GetPost(blogPostId))
                .Returns(blogPost);
            var homeController = new HomeController(blogService);

            // Act
            var actual = homeController.Post(blogPostId);

            // Assert
            A.CallTo(() => blogService.GetPost(A<int>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(actual);
            Assert.IsType<ViewResult>(actual);
            Assert.IsType<BlogPostModel>(((ViewResult)actual).Model);
            Assert.NotEmpty(((BlogPostModel)((ViewResult)actual).Model).Name);
        }


        [Theory]
        [AutoFakeItEasyData]
        public void HomeController_Edit_ReturnsCorrectView(
            IBlogService blogService,
            int blogPostId,
            BlogPost blogPost)
        {
            // Arrange
            A.CallTo(() => blogService.GetPost(blogPostId))
                .Returns(blogPost);
            var homeController = new HomeController(blogService);

            // Act
            var actual = homeController.Edit(blogPostId);

            // Assert
            A.CallTo(() => blogService.GetPost(A<int>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(actual);
            Assert.IsType<ViewResult>(actual);
            Assert.IsType<BlogPostModel>(((ViewResult)actual).Model);
            Assert.NotEmpty(((BlogPostModel)((ViewResult)actual).Model).Name);
        }


        [Theory]
        [AutoFakeItEasyData]
        public void HomeController_Edit_Succeeds(
            IBlogService blogService,
            BlogPostModel model)
        {
            // Arrange
            var homeController = new HomeController(blogService);

            // Act
            var actual = homeController.Edit(model);

            // Assert
            A.CallTo(() => blogService.UpdatePost(model.Id, model.Name, model.Markdown))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(actual);
            Assert.IsType<RedirectToRouteResult>(actual);
            Assert.True(((RedirectToRouteResult)actual).RouteValues.ContainsKey("action"));
            Assert.Equal("Post", ((RedirectToRouteResult)actual).RouteValues["action"]);
            Assert.True(((RedirectToRouteResult)actual).RouteValues.ContainsKey("Id"));
            Assert.Equal(model.Id, ((RedirectToRouteResult) actual).RouteValues["Id"]);
        }


        [Theory]
        [AutoFakeItEasyData]
        public void HomeController_Edit_WithInvalidModelReturnsView(
            IBlogService blogService,
            BlogPostModel model,
            string modelError)
        {
            // Arrange
            var homeController = new HomeController(blogService);
            homeController.ModelState.AddModelError(string.Empty, modelError);

            // Act
            var actual = homeController.Edit(model);

            // Assert
            A.CallTo(() => blogService.UpdatePost(A<int>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .MustHaveHappened(Repeated.Never);
            Assert.NotNull(actual);
            Assert.IsType<ViewResult>(actual);
            Assert.IsType<BlogPostModel>(((ViewResult)actual).Model);
            Assert.Equal(model, ((ViewResult)actual).Model);
        }

    }
}
