using Parbandhan.Interfaces;
using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class StorageLocationService : IStorageLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _api;

        public StorageLocationService(HttpClient httpClient, ApiSettings api)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<ApiResponse<List<StorageLocationModel>>> GetAllAsync()
        {
            var url = $"{_api.BaseUrl}{_api.StorageLocation.GetAllStorageLocation}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<List<StorageLocationModel>>
                {
                    Status = (int)response.StatusCode,
                    Message = response.ReasonPhrase
                };
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<StorageLocationModel>>>(json);
            return apiResponse!;
        }


        public async Task<ApiResponse<List<StorageLocationModel>>> GetByAssetTypesAsync(IEnumerable<int> assetTypeIds)
        {
            var query = string.Join("&", assetTypeIds.Select(id => $"atIds={id}"));
            var url = $"{_api.BaseUrl}{_api.StorageLocation.GetStorageLocationFilter}{query}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<List<StorageLocationModel>>
                {
                    Status = (int)response.StatusCode,
                    Message = response.ReasonPhrase
                };
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<StorageLocationModel>>>(json
                            , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return apiResponse!;
        }
    }
}
