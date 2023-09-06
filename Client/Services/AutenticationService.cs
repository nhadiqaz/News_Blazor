using Models;
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

        public async Task<string> GetTokenAsync(UserLogInViewModel userViewModel)
        {
            try
            {
                //var _result = await HttpClient.PostAsJsonAsync(requestUri: "api/Authentications/Autentication", userViewModel);
                var _result = await HttpClient.PostAsJsonAsync<UserLogInViewModel>(requestUri: "api/Authentications/Autentication", userViewModel);

                //string _token;

                if (_result.IsSuccessStatusCode)
                {
                    var _token = await _result.Content.ReadAsStringAsync();
                    return _token;
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
