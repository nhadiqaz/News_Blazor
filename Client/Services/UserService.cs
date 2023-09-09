using Client.Pages.User;
using Models;
using System.Net;
using System.Net.Http.Json;
using ViewModels;

namespace Services
{
    public class UserService : IUserService
    {
        #region Dependency
        public HttpClient HttpClient { get; }
        public ILogService LogService { get; }

        public UserService(HttpClient httpClient,ILogService logService)
        {
            HttpClient = httpClient;
            LogService = logService;
        }

        #endregion \Dependency


        #region Methods

        #region IsExistEmailAsync
        public async Task<bool> IsExistEmailAsync(string email)
        {
            try
            {
                var _response = await HttpClient.GetAsync(requestUri: $"api/Users/IsExistEmail/{email}");

                if (_response.IsSuccessStatusCode)
                {
                    var _content = _response.Content.ReadAsStringAsync().Result;

                    var _isExistEmail = Convert.ToBoolean(_content);

                    return _isExistEmail;
                }
                else
                {
                    var _message = "سرور دچار خطا شده است";

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

        #endregion \IsExistEmailAsync

        #region AddUserAsync

        public async Task AddUserAsync(RegisterUserViewModel registerUserViewModel)
        {
            try
            {
                var _response = await HttpClient.PostAsJsonAsync<RegisterUserViewModel>(requestUri: "api/Users/RegisterUser", registerUserViewModel);

                if (_response.StatusCode==HttpStatusCode.BadRequest)
                {
                    var _message = await _response.Content.ReadAsStringAsync();

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

        #endregion \AddUserAsync

        #endregion \Methods
    }
}
