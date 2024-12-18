using CandidateWeb.Models.Base;
using CandidateWeb.Service.Dependency;

namespace CandidateWeb.Service.Base
{
    public interface IBaseService :IScopedService
    {
        Task<ResponseDto<T?>?> PostAsync<T>(string endpoint, StringContent stringContent, IList<string>? path = null);

        Task<ResponseDto<T?>?> UpdateAsync<T>(string endpoint, string updateType, StringContent stringContent);
    }
}
