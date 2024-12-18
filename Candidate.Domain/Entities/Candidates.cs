using Candidate.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Candidate.Domain.Entities
{
    public class Candidates : BaseEntity<Guid>
    {
        [Required]
        public string FirstName {  get; set; }

        [Required]
        public string LastName { get; set; }    

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public TimeSpan? BestCallTime { get; set; }

        public string? LinkedInProfileURL { get; set; }

        public string? GitHubProfileURL { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
