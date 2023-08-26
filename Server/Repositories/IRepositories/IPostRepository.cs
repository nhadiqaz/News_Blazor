using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostAsync(int postId);
        Task<Post> AddPostAsync(Post post);
        Task<Post> UpdatePostAsync(int postId, EditPostViewModel editPostViewModel);
        Task<bool> IsExistPostAsync(string title);
        Task<bool> IsExistPostAsync(int postId);
        Task DeletePostAsync(int postId);
        Task<string> GetImageName(int postId);
    }
}
