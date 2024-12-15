using Candidate.Application.DTOs.Candidate;
using Candidate.Application.Interfaces.Repository;
using Candidate.Application.Interfaces.Services;
using Candidate.Domain.Entities;

namespace Candidate.Infrastructure.Implementation.Services
{
    public class CandidateService(IGenericRepository genericRepository) : ICandidateService
    {
        public bool InsertUpdateCandidate(CreateUpdateCandidateDto candidateDto)
        {
            var existsCandidate = genericRepository.Exists<Candidates>(c => c.Email == candidateDto.Email);

            if (existsCandidate) 
            { 
                candidateDto.FirstName = candidateDto.LastName;
                candidateDto.LastName = candidateDto.FirstName;
                candidateDto.PhoneNumber = candidateDto.PhoneNumber;
                candidateDto.Comment = candidateDto.Comment;
                candidateDto.BestCallTime = candidateDto.BestCallTime;
                candidateDto.LinkedInProfileURL = candidateDto.LinkedInProfileURL;
                candidateDto.GitHubProfileURL = candidateDto.GitHubProfileURL;

                genericRepository.Update(candidateDto);

                return true;
            }
            else
            {
                var candidate = new Candidates
                {
                    FirstName = candidateDto.FirstName,
                    LastName = candidateDto.LastName,
                    Email = candidateDto.Email,
                    PhoneNumber = candidateDto.PhoneNumber,
                    BestCallTime = candidateDto.BestCallTime,
                    LinkedInProfileURL = candidateDto.LinkedInProfileURL,
                    GitHubProfileURL = candidateDto.GitHubProfileURL,
                    Comment = candidateDto.Comment,
                };

                genericRepository.Insert(candidate);

                return false;
            }
        }
    }
}
