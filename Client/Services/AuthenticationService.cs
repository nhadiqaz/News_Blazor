using Models;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ViewModels;
using ViewModels.User;

namespace Services
{
    public class AuthenticationService: IAuthenticationService
    {
        #region Dependency
        public HttpClient HttpClient { get; }
        public ILogService LogService { get; }

        public AuthenticationService(HttpClient httpClient,ILogService logService)
        {
            HttpClient = httpClient;
            LogService = logService;
        }


        #endregion \Dependency

        #region Methodes

        #region GetToken

        public async Task<string> GetTokenAsync(LogInUserViewModel logInUserViewModel)
        {
            try
            {
                //var response = await HttpClient.PostAsJsonAsync(requestUri: "api/Authentications/Autentication", userViewModel);
                var _response = await HttpClient.PostAsJsonAsync<LogInUserViewModel>(requestUri: "api/Authentications/Authentication", logInUserViewModel);

                if (_response.IsSuccessStatusCode)
                {
                    var _token = await _response.Content.ReadAsStringAsync();
                    return _token;
                }
                else if (_response.StatusCode == HttpStatusCode.NotFound)
                {
                    var _message = await _response.Content.ReadAsStringAsync();
                    throw new Exception(_message);
                }
                else if (_response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var _message = await _response.Content.ReadAsStringAsync();
                    throw new Exception(_message);
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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetToken

        #region GetTokenUpdateEmail

        public async Task<string> GetTokenUpdateEmailAsync(string email)
        {
            try
            {
                var _updateTokenViewModel = new UpdateTokenViewModel
                {
                    Email = email
                };

                var _response = await HttpClient.PostAsJsonAsync<UpdateTokenViewModel>(requestUri: $"api/Authentications/AuthenticationUpdateEmail", _updateTokenViewModel);

                if (_response.IsSuccessStatusCode)
                {
                    var _token = await _response.Content.ReadAsStringAsync();

                    return _token;
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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetTokenUpdateEmail

        #endregion \Methodes

    }
}
