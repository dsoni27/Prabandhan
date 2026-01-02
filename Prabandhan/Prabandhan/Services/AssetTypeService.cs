using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Parbandhan.Interfaces;
using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _api;

        public AssetTypeService(HttpClient httpClient, ApiSettings api)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<ApiResponse<List<AssetTypeModel>>> GetAssetTypesAsync()
        {
            var url = $"{_api.BaseUrl}{_api.AssetType.GetAllAssetType}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<List<AssetTypeModel>>
                {
                    Status = (int)response.StatusCode,
                    Message = response.ReasonPhrase
                };
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<AssetTypeModel>>>(json);
        }
    }
}
