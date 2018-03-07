using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces;

namespace Application.Core.Services
{
    public class PostService: IPostService
    {
        private readonly IAppLogger<PostService> _logger;
        private readonly IAsyncRepository<Post> _postRepository;

        public PostService(
            IAppLogger<PostService> logger,
            IAsyncRepository<Post> postRepository
            )
        {
            _logger = logger;
            _postRepository = postRepository;
        }

        public async Task CreatePostAsync(Post post)
        {
            await _postRepository.AddAsync(post);
        }

        public async Task DeletePostAsync(Post post)
        {
            await _postRepository.DeleteAsync(post);
        }

        public async Task SetPostAsync(Post post)
        {
            await _postRepository.UpdateAsync(post);
        }
    }
}
