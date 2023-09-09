using Models;
using System.Net;
using System.Net.Http.Json;
using ViewModels;

namespace Services
{
    public class AutenticationService: IAutenticationService
    {
        #region Dependency
        public HttpClient HttpClient { get; }
        public ILogService LogService { get; }

        public AutenticationService(HttpClient httpClient,ILogService logService)
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
                var _response = await HttpClient.PostAsJsonAsync<LogInUserViewModel>(requestUri: "api/Authentications/Autentication", logInUserViewModel);

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
                    var _message = "The server has an error";
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

        #endregion \GetToken

        #endregion \Methodes

    }
}
