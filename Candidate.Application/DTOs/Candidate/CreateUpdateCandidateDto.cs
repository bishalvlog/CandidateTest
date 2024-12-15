namespace Candidate.Application.DTOs.Candidate
{
    public class CreateUpdateCandidateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? BestCallTime { get; set; }

        public string LinkedInProfileURL { get; set; }

        public string GitHubProfileURL { get; set; }

        public string Comment { get; set; }
    }
}

