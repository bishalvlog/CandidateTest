using CandidateWeb.Models.Base;
using CandidateWeb.Models.Requests;
using CandidateWeb.Service.Dependency;

namespace CandidateWeb.Service.Interface
{
    public interface ICandidateService : ITransientService
    {
        Task<ResponseDto<bool?>?> InsertUpdate(CreateCandidateDto candidate);
    }
}
