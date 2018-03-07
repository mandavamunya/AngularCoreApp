using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces;

namespace Application.Core.Services
{
    public class BlogService: IBlogService
    {
        private readonly IAppLogger<BlogService> _logger;
        private readonly IAsyncRepository<Blog> _blogRepository;

        public BlogService(
            IAppLogger<BlogService> logger,
            IAsyncRepository<Blog> blogRepository
            )
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        public async Task CreateBlogAsync(Blog blog)
        {
            await _blogRepository.AddAsync(blog);
        }

        public async Task DeleteBlogAsync(Blog blog)
        {
            await _blogRepository.DeleteAsync(blog);
        }

        public async Task SetBogAsync(Blog blog)
        {
            await _blogRepository.UpdateAsync(blog);
        }
    }
}
