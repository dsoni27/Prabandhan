using Microsoft.Extensions.Options;
using Parbandhan.Interfaces;
using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly ApiSettings _api;
        public AuthService(HttpClient httpClient, ITokenService tokenService, JwtService jwtService, ApiSettings api)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<ApiResponse<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel)
        {
            try
            {
                var url = $"{_api.BaseUrl}{_api.Auth.Login}";
                var response = await _httpClient.PostAsJsonAsync(url, loginRequestModel);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<LoginResponseModel>
                    {
                        Status = (int)response.StatusCode,
                        Message = response.ReasonPhrase,
                        Result = null
                    };
                }
                else
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseModel>>();
                    var userClaims = _jwtService.DecodeToken(loginResponse.Result.Data.AccessToken);
                    _tokenService.SetToken(loginResponse.Result.Data.AccessToken, loginResponse.Result.Data.RefreshToken, userClaims);
                    return loginResponse!;
                }
            }
            finally
            {
            }
        }

        public bool IsTokenExpired()
        {
            return _tokenService.UserClaims?.Expiry <= DateTime.UtcNow;
        }

        public async Task<string> RefreshAccessTokenAsync()
        {
            // Call refresh API using _userSession.RefreshToken
            //string newAccessToken = await CallRefreshApi();

            //var claims = _jwtService.DecodeToken(newAccessToken);
            //_tokenService.RefreshAccessToken(newAccessToken);

            //return newAccessToken;

            return string.Empty;
        }
    }
}
