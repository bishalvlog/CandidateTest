namespace CandidateWeb.Models.Requests
{
    public class CreateCandidateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public TimeSpan? BestCallTime { get; set; }  = null;

        public string LinkedInProfileURL { get; set; }

        public string GitHubProfileURL { get; set; }

        public string Comment { get; set; }
    }
}
