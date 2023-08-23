using Client;
using Client.Shared.Component;
using Generator;
using Microsoft.AspNetCore.Components.Forms;
using Models;
using Resources;
using System.Data;
using System.Net;
using System.Net.Http.Json;
using ViewModels;

namespace Services
{
    public class PostService : IPostService
    {
        #region Dependency
        public HttpClient HttpClient { get; }
        public ILogService LogService { get; }

        public PostService(HttpClient httpClient, ILogService logService)
        {
            HttpClient = httpClient;
            LogService = logService;
        }

        #endregion \Dependency

        #region Methods

        #region GetAllPosts

        public async Task<List<Models.Post>> GetAllPostsAsync()
        {
            try
            {
                var _posts = await HttpClient.GetFromJsonAsync<List<Models.Post>>("api/Posts/GetAllPosts");

                foreach (var post in _posts)
                {
                    post.ImageUrl = await GetImageUrlAsync(post.ImageName);
                }

                return _posts;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }

        #endregion \GetAllPosts

        #region AddPost

        public async Task AddPostAsync(AddPostViewModel addPostViewModel)
        {
            try
            {
                var _response = await HttpClient.PostAsJsonAsync<AddPostViewModel>("api/Posts/AddPost", addPostViewModel);

                if (_response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var _message = await _response.Content.ReadAsStringAsync();
                    throw new Exception(_message);
                }
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
            }
        }

        #endregion \AddPost

        #region UploadImage

        public async Task<string> UploadImageAsync(MultipartFormDataContent content)
        {
            try
            {
                var _response = await HttpClient.PostAsync("api/Uploads/UploadImage", content);

                var _content = await _response.Content.ReadAsStringAsync();
                return _content;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);

                throw new Exception(ex.Message);
            }
        }

        #endregion \UploadImage

        #region GetImageUrl

        public async Task<string> GetImageUrlAsync(string imageName)
        {
            try
            {
                if (imageName == "Default")
                {
                    return "/Images/Post/Default.jpg";
                }

                string _imageUrl;

                var _response = await HttpClient.GetAsync($"api/Posts/GetImage/{imageName}");

                if (_response.IsSuccessStatusCode)
                {
                    var _imageData = await _response.Content.ReadAsByteArrayAsync();
                    var _base64Image = Convert.ToBase64String(_imageData);
                    var _mimeType = _response.Content.Headers.ContentType.MediaType;

                    _imageUrl = $"data:{_mimeType};base64,{_base64Image}";
                }
                else
                {
                    var _message = await _response.Content.ReadAsStringAsync();

                    throw new Exception(_message);
                }
                return _imageUrl;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);

                throw new Exception(ex.Message);
            }
        }

        #endregion \GetImageUrl

        #region IsPostExistAsync

        public async Task<bool> IsPostExistAsync(string title)
        {
            try
            {
                var _response = await HttpClient.GetAsync(requestUri: $"api/Posts/IsExistPost/{title}");

                if (_response.IsSuccessStatusCode)
                {
                    var _content = _response.Content.ReadAsStringAsync().Result;

                    var _isExistPost = Convert.ToBoolean(_content);

                    return _isExistPost;
                }
                else
                {
                    var _message = "سرور دچار مشکل شده است";

                    throw new Exception(_message);
                }
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion \Methods
    }
}
