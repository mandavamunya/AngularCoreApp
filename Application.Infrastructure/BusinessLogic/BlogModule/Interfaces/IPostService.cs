using Application.Core.Entities;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IPostService
    {
        Task<Post> GetPostById(int postId);
        Task CreatePostAsync(Post post);
        Task SetPostAsync(Post post);
        Task DeletePostAsync(Post post);
    }
}
