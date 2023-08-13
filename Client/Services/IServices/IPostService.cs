using Client.Shared.Component;

namespace Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
    }
}
