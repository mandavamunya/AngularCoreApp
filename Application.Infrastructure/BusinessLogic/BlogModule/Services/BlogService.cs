using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces;
using Application.Core.Specifications;

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

        public async Task<IEnumerable<Blog>> GetAllBlogItems(int blogId)
        {
            var spec = new BlogWithItemsSpecification(blogId);
            return await _blogRepository.ListAsync(spec);
        }

        public async Task<Blog> GetBlogById(int blogId)
        {
            return await _blogRepository.GetByIdAsync(blogId);
        }

        public async Task SetBogAsync(Blog blog)
        {
            await _blogRepository.UpdateAsync(blog);
        }
    }
}
