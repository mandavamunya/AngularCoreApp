using Application.Core.Entities;
using Application.Core.Exceptions;
using Application.Core.Interfaces;
using Application.Core.Services;
using Moq;
using System;
using Xunit;

namespace UnitTests.Application.Core.Services.BlogServiceTests
{
    public class SetBlog
    {
        private int _invalidId = -1;
        private Mock<IAsyncRepository<Blog>> _mockBlogRepo;

        public SetBlog()
        {
            _mockBlogRepo = new Mock<IAsyncRepository<Blog>>();
        }

        [Fact]
        public async void ThrowsGivenInvalidBlogId()
        {
            var blogService = new BlogService(null, _mockBlogRepo.Object);

            await Assert.ThrowsAsync<BlogNotFoundException>(
                async () => await blogService.GetBlogById(_invalidId)
            );
        }

        [Fact]
        public async void ThrowsGivenNullBlogServiceArguments()
        {
            var blogService = new BlogService(null, _mockBlogRepo.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await blogService.SetBogAsync(null)
            );

        }

    }
}
