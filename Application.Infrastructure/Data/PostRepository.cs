using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data
{
    public class PostRepository : EfRepository<Post>, IPostRepository
    {
        public PostRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Post> GetPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _dbContext.Posts.ToListAsync();
        }
    }
}
