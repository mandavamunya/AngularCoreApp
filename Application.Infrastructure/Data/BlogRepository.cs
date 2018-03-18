using Application.Core.Entities;
using Application.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Infrastructure.Data
{
    public class BlogRepository : EfRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AppIdentityDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return _dbContext.Blogs
                .Include(o => o.Posts)
                .ToList();
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            return await _dbContext.Blogs
                .Include(o => o.Posts)
                .ToListAsync();
        }

    }
}
