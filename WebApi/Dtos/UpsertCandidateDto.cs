using System.ComponentModel.DataAnnotations;
namespace WebApi.Dtos
{
    public class UpsertCandidateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public TimeSpan? PreferredCallStartTime { get; set; }
        public TimeSpan? PreferredCallEndTime { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
