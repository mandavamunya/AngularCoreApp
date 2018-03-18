using Application.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Infrastructure.Data
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetBlogs();
        Task<IEnumerable<Blog>> GetBlogsAsync();
    }
}
