using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TimeSpan? PreferredCallStartTime { get; set; }
        public TimeSpan? PreferredCallEndTime { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        public string Comments { get; set; } = string.Empty;

    }
}


