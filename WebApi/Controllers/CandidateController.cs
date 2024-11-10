using Domain.Entities;
using Domain.Interfaces.CandidateManagement.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] UpsertCandidateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Candidate candidate = new Candidate
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                PreferredCallStartTime = dto.PreferredCallStartTime,
                PreferredCallEndTime = dto.PreferredCallEndTime,
                LinkedInProfile = dto.LinkedInProfile,
                GitHubProfile = dto.GitHubProfile,
                Comments = dto.Comments
            };
            await _candidateService.UpsertCandidateAsync(candidate);
            return Ok(new { Message = "Candidate processed successfully" });
        }
    }
}

