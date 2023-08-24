using Client.Shared.Component;
using Microsoft.AspNetCore.Components.Forms;
using ViewModels;

namespace Services
{
    public interface IPostService
    {
        Task<List<Models.Post>> GetAllPostsAsync();
        Task<EditPostViewModel> GetPostAsync(int postId);
        Task AddPostAsync(AddPostViewModel addPostViewModel);
        Task<string> UploadImageAsync(MultipartFormDataContent content);
        Task<string> GetImageUrlAsync(string imageName);
        Task<bool> IsPostExistAsync(string title);
        Task DeleteImageAsync(string imageName);
        Task UpdatePostAsync(int postId, EditPostViewModel editPostViewModel);
    }
}
