using Client.Shared.Component;
using Microsoft.AspNetCore.Components.Forms;
using ViewModels;

namespace Services
{
    public interface IPostService
    {
        Task<List<Models.Post>> GetAllPostsAsync();
        Task AddPostAsync(AddPostViewModel addPostViewModel);
        Task<string> UploadImageAsync(MultipartFormDataContent content);
        Task<string> GetImageUrlAsync(string imageName);
        Task<bool> IsPostExistAsync(string title);
    }
}
