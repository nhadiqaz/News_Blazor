using Models;

namespace Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostAsync(int postId);
        Task<Post> AddPostAsync(Post post);
        Task<bool> IsExistPostAsync(string title);
    }
}
