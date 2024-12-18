using CandidateWeb.Models.Base;
using CandidateWeb.Models.Constants;
using CandidateWeb.Service.HTTP;
using System.Net.Http.Json;

namespace CandidateWeb.Service.Base
{
    public class BaseService(ApiHttpClient apiHttpClient) : IBaseService
    {
        private readonly HttpClient _httpClient = apiHttpClient.HttpClient;

        public async Task<ResponseDto<T?>?> PostAsync<T>(string endpoint, StringContent stringContent, IList<string>? path = null)
        {
            try
            {
                if (path is { Count: > 0 })
                {
                    endpoint = path.Aggregate(endpoint, (current, parameter) => current + ("/" + parameter));
                }

                var response = await _httpClient.PostAsync($"/api/{endpoint}", stringContent);

                return await response.Content.ReadFromJsonAsync<ResponseDto<T?>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured while handing your request: {ex.Message}");
            }

            return default;
        }

        public async Task<ResponseDto<T?>?> UpdateAsync<T>(string endpoint, string updateType, StringContent stringContent)
        {
            try
            {

                var response = updateType == Constants.UpdateType.Patch
                    ? await _httpClient.PatchAsync($"/api/{endpoint}", stringContent)
                    : await _httpClient.PutAsync($"/api/{endpoint}", stringContent);

                return await response.Content.ReadFromJsonAsync<ResponseDto<T?>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured while handing your request: {ex.Message}");
            }

            return default;
        }
    }
}
