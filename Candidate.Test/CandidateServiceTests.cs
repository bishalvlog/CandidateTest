using Candidate.Application.DTOs.Candidate;
using Candidate.Application.Interfaces.Repository;
using Candidate.Domain.Entities;
using Candidate.Infrastructure.Implementation.Services;
using FluentAssertions;
using Moq;

namespace Candidate.Test
{
    public class CandidateServiceTests
    {
        private readonly Mock<IGenericRepository> _mockRepository;

        private readonly CandidateService _candidateService;

        public CandidateServiceTests() 
        {
            _mockRepository = new Mock<IGenericRepository>();
            _candidateService = new CandidateService(_mockRepository.Object);
        }

        [Fact]
        public void InsertCandidate()
        {
            // Arrange
            var candidateDto = new CreateCandidateDto
            {
                FirstName = "jenny",
                LastName = "khadgi",
                Email = "jenny.khadgi@example.com",
                PhoneNumber = "987654321",
                BestCallTime = TimeSpan.FromHours(10),
                LinkedInProfileURL = "https://linkedin.com/in/janedoe",
                GitHubProfileURL = "https://github.com/janedoe",
                Comment = "New candidate"
            };

             // Act
            var result = _candidateService.InsertCandidate(candidateDto);

            // Assert
            result.Should().BeTrue();
            _mockRepository.Verify(repo => repo.Insert(It.IsAny<Candidates>()), Times.Once);
        }
    }
}
