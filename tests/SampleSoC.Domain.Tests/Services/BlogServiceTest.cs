namespace SampleSoC.Domain.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FakeItEasy;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Domain.Services;
    using Socres.FakingEasy.AutoFakeItEasy;
    using Xunit;
    using Xunit.Extensions;

    public class BlogServiceTest
    {
        [Theory]
        [AutoFakeItEasyData]
        public void BlogService_GetPosts_ReturnsCorrectResults(
            IUnitOfWork unitOfWork,
            List<BlogPost> blogPosts)
        {
            // Arrange
            Expression<Func<BlogPost, DateTime>> sortExpression = bp => bp.DateCreated;
            Expression<Func<BlogPost, bool>> specification = bp => bp.Id > 0;
            A.CallTo(() =>
                        unitOfWork.BlogPosts.Fetch(
                            A<Expression<Func<BlogPost, DateTime>>>.Ignored,
                            A<Expression<Func<BlogPost, bool>>>.Ignored))
                            .WhenArgumentsMatch(args =>
                                args.Get<Expression<Func<BlogPost, DateTime>>>(0).ToString() == sortExpression.ToString()
                                && args.Get<Expression<Func<BlogPost, bool>>>(1).ToString() == specification.ToString())
                        .Returns(blogPosts);
            var blogService = new BlogService(unitOfWork);

            // Act
            var actual = blogService.GetPosts();

            // Assert
            A.CallTo(() =>
                unitOfWork.BlogPosts.Fetch(
                    A<Expression<Func<BlogPost, DateTime>>>.Ignored,
                    A<Expression<Func<BlogPost, bool>>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(blogPosts, actual);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void BlogService_GetPost_ReturnsCorrectResult(
            IUnitOfWork unitOfWork,
            int blogPostId,
            BlogPost blogPost)
        {
            // Arrange
            A.CallTo(() =>
                unitOfWork.BlogPosts.GetById(blogPostId))
                .Returns(blogPost);
            var blogService = new BlogService(unitOfWork);

            // Act
            var actual = blogService.GetPost(blogPostId);

            // Assert
            A.CallTo(() =>
                unitOfWork.BlogPosts.GetById(A<int>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(blogPost, actual);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void BlogService_UpdatePost_Succeeds(
            IUnitOfWork unitOfWork,
            int blogPostId,
            string name,
            string markdown,
            BlogPost blogPost)
        {
            // Arrange
            A.CallTo(() =>
                unitOfWork.BlogPosts.GetById(blogPostId))
                .Returns(blogPost);
            var blogService = new BlogService(unitOfWork);

            // Act
            blogService.UpdatePost(blogPostId, name, markdown);

            // Assert
            A.CallTo(() =>
                unitOfWork.BlogPosts.GetById(A<int>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() =>
                unitOfWork.Save())
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(blogPost.Name, name);
            Assert.Equal(blogPost.Markdown, markdown);
        }

        [Theory]
        [AutoFakeItEasyData]
        public void BlogService_GetComments_ReturnsCorrectResults(
            IUnitOfWork unitOfWork,
            int blogPostId,
            List<BlogComment> blogComments)
        {
            // Arrange
            Expression<Func<BlogComment, DateTime>> sortExpression = bc => bc.DateCreated;
            A.CallTo(() =>
                unitOfWork.BlogComments.Fetch(
                    A<Expression<Func<BlogComment, DateTime>>>.Ignored,
                    A<Expression<Func<BlogComment, bool>>>.Ignored))
                .WhenArgumentsMatch(args =>
                    args.Get<Expression<Func<BlogComment, DateTime>>>(0).ToString() == sortExpression.ToString())
                .Returns(blogComments);
            var blogService = new BlogService(unitOfWork);

            // Act
            var actual = blogService.GetComments(blogPostId);

            // Assert
            A.CallTo(() =>
                unitOfWork.BlogComments.Fetch(
                    A<Expression<Func<BlogComment, DateTime>>>.Ignored,
                    A<Expression<Func<BlogComment, bool>>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            Assert.Equal(blogComments, actual);
        }
    }
}
