using System.ComponentModel.DataAnnotations;

namespace Candidate.Domain.Common.Base
{
    public class BaseEntity<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
