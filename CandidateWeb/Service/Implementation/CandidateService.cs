using CandidateWeb.Models.Base;
using CandidateWeb.Models.Constants;
using CandidateWeb.Models.Requests;
using CandidateWeb.Service.Base;
using CandidateWeb.Service.Interface;
using System.Text.Json;

namespace CandidateWeb.Service.Implementation
{
    public class CandidateService(IBaseService baseService) : ICandidateService
    {
        public async Task<ResponseDto<bool?>?> InsertUpdate(CreateCandidateDto candidate)
        {
            var jsonRequest = JsonSerializer.Serialize(candidate);

            var content = new StringContent(jsonRequest,System.Text.Encoding.UTF8, "application/json");

            var response = await baseService.PostAsync<bool?>(ApiEndpoints.Candidate.InsertUpdateCandidate,content);

            return response;
        }
    }
}
