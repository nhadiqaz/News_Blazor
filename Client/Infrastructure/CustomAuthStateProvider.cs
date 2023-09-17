using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Client.Infrastructure
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        #region Dependency
        public ILocalStorageService LocalStorageService { get; }
        public HttpClient HttpClient { get; }

        public CustomAuthStateProvider(ILocalStorageService localStorageService,HttpClient httpClient)
        {
            LocalStorageService = localStorageService;
            HttpClient = httpClient;
        }

        #endregion \Dependency


        #region Methods

        #region GetAuthenticationState

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState? _state = null;

            var _token = await LocalStorageService.GetItemAsStringAsync(key: "token");

            await Task.Run(() =>
            {
                var _identiry = new ClaimsIdentity();

                HttpClient.DefaultRequestHeaders.Authorization = null;

                if (string.IsNullOrEmpty(_token) == false)
                {
                    _identiry = new ClaimsIdentity(ParseClaimsFromJwt(_token), "jwt");
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.Replace("\"", ""));
                }

                var _claimsPrincipal = new ClaimsPrincipal(_identiry);

                _state = new AuthenticationState(_claimsPrincipal);

                NotifyAuthenticationStateChanged(Task.FromResult(_state));
            });

            return _state;

        }

        #endregion GetAuthenticationState

        #region ParseClaimsFromJwt

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var _claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return _claims;
        }

        #endregion \ParseClaimsFromJwt

        #region ParseBase64WithoutPadding

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        #endregion \ParseBase64WithoutPadding

        #endregion \Methods
    }
}
