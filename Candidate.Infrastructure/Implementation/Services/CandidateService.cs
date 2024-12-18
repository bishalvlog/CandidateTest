using Candidate.Application.DTOs.Candidate;
using Candidate.Application.Interfaces.Repository;
using Candidate.Application.Interfaces.Services;
using Candidate.Domain.Entities;

namespace Candidate.Infrastructure.Implementation.Services
{
    public class CandidateService(IGenericRepository genericRepository) : ICandidateService
    {
        public bool InsertCandidate(CreateCandidateDto candidateDto)
        {
            try
            {
                var existsCandidate = genericRepository.Exists<Candidates>(c => c.Email == candidateDto.Email);

                if (existsCandidate)
                {
                    return false;
                }
                else
                {
                    var candidate = new Candidates
                    {
                        FirstName = candidateDto.FirstName,
                        LastName = candidateDto.LastName,
                        Email = candidateDto.Email,
                        PhoneNumber = candidateDto.PhoneNumber,
                        LinkedInProfileURL = candidateDto.LinkedInProfileURL,
                        GitHubProfileURL = candidateDto.GitHubProfileURL,
                        Comment = candidateDto.Comment,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    genericRepository.Insert(candidate);

                    return true;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("An error occurred while inserting the candidate.", ex);
            }
        }
    }
}
