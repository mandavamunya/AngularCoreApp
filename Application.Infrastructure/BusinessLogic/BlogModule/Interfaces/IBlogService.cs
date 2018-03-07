using Application.Core.Entities;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IBlogService
    {
        Task CreateBlogAsync(Blog blog);
        Task SetBogAsync(Blog blog);
        Task DeleteBlogAsync(Blog blog);
    }
}
