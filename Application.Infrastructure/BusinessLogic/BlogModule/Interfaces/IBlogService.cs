using Application.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogItems(int blogId);
        Task<Blog> GetBlogById(int blogId);
        Task CreateBlogAsync(Blog blog);
        Task SetBogAsync(Blog blog);
        Task DeleteBlogAsync(Blog blog);
    }
}
