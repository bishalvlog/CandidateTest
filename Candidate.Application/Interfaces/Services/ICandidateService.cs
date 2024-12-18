using Candidate.Application.Common.Service;
using Candidate.Application.DTOs.Candidate;

namespace Candidate.Application.Interfaces.Services
{
    public interface ICandidateService :ITransientService
    {
        bool InsertCandidate(CreateCandidateDto candidateDto);
    }
}
