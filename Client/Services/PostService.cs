using Client;
using Client.Shared.Component;
using System.Net.Http.Json;

namespace Services
{
    public class PostService: IPostService
    {
        #region Dependency
        public HttpClient HttpClient { get; }
        public PostService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        #endregion \Dependency

        #region Methods

        #region GetAllPosts

        public async Task<List<Post>> GetAllPostsAsync()
        {
            try
            {
                var _posts = await HttpClient.GetFromJsonAsync<List<Post>>("api/Posts/GetAllPosts");

                return _posts;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }

        #endregion \GetAllPosts

        #endregion \Methods

    }
}
