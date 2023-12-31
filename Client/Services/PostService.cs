﻿using Client;
using Client.Shared.Component;
using Generator;
using Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Models;
using Resources;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ViewModels;

namespace Services
{
    public class PostService : IPostService
    {
        #region Dependency
        protected HttpClient HttpClient { get; }
        protected ILogService LogService { get; }

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
                var _posts = await HttpClient.GetFromJsonAsync<List<Models.Post>>(requestUri: "api/Posts/GetAllPosts");

                foreach (var post in _posts)
                {
                    post.ImageUrl = await GetImageUrlAsync(post.ImageName);
                }

                return _posts;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ShowPostsViewModel> GetAllPostsAsync(int pageId = 1, string filterPostTilte = "")
        {
            try
            {
                var _posts = await HttpClient.GetFromJsonAsync<ShowPostsViewModel>(requestUri: $"api/Posts/GetAllPosts/{pageId}/{filterPostTilte}");

                foreach (var post in _posts.Posts)
                {
                    post.ImageUrl = await GetImageUrlAsync(post.ImageName);
                }

                return _posts;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetAllPosts

        #region GetPost

        public async Task<Models.Post> GetPostAsync(int postId)
        {
            try
            {
                var _post = await HttpClient.GetFromJsonAsync<Models.Post>(requestUri: $"api/Posts/GetPost/{postId}");

                _post.ImageUrl = await GetImageUrlAsync(_post.ImageName);

                return _post;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetPost

        #region GetPostAsync_Edit

        public async Task<EditPostViewModel> GetPostAsync_Edit(int postId)
        {
            try
            {
                var _response = await HttpClient.GetFromJsonAsync<Models.Post>(requestUri: $"api/Posts/GetPost/{postId}");

                var _postViewModel = _response.ConvertTo_EditPostViewModel();

                _postViewModel.ImageUrl = await GetImageUrlAsync(_postViewModel.ImageName);

                return _postViewModel;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetPostAsync_Edit

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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \AddPost

        #region UploadImage

        public async Task<string> UploadImageAsync(MultipartFormDataContent content)
        {
            try
            {
                var _response = await HttpClient.PostAsync("api/Files/UploadImage", content);

                var _content = await _response.Content.ReadAsStringAsync();

                return _content;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetImageUrl

        #region IsPostExist

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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsPostExist

        #region DeleteImage

        public async Task DeleteImageAsync(string imageName)
        {
            try
            {
                await HttpClient.DeleteAsync(requestUri: $"api/Files/DeleteImage/{imageName}");
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \DeleteImage

        #region UpdatePost

        public async Task UpdatePostAsync(int postId, EditPostViewModel editPostViewModel)
        {
            try
            {
                var _response = await HttpClient.PatchAsJsonAsync<EditPostViewModel>(requestUri: $"api/Posts/UpdatePost/{postId}", editPostViewModel);

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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \UpdatePost

        #region DeletePostAsync

        public async Task DeletePostAsync(int postId)
        {
            try
            {
                await HttpClient.DeleteAsync(requestUri: $"api/Posts/DeletePost/{postId}");
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \DeletePostAsync

        #region GetImageNameAsync

        public async Task<string> GetImageNameAsync(int postId)
        {
            try
            {
                var _response = await HttpClient.GetStringAsync(requestUri: $"api/Posts/GetImageName/{postId}");

                return _response;
            }
            catch (Exception ex)
            {
                var _log = new Log(ex.Message);
                await LogService.AddLogAsync(_log);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        #endregion \GetImageNameAsync

        #endregion \Methods
    }
}
