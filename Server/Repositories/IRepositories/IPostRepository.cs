using Models;

namespace Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
    }
}
