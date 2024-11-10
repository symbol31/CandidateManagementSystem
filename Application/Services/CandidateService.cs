using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly ILogger<CandidateService> _logger;

        public CandidateService(ICandidateRepository repository, ILogger<CandidateService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task UpsertCandidateAsync(Candidate candidate)
        {
            _logger.LogInformation("Processing candidate with email: {Email}", candidate.Email);
            var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);

            if (existingCandidate != null)
            {
                _logger.LogInformation("Updating existing candidate: {Email}", candidate.Email);
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber ?? existingCandidate.PhoneNumber;
                existingCandidate.Email = candidate.Email;
                existingCandidate.PreferredCallStartTime = candidate.PreferredCallStartTime ?? existingCandidate.PreferredCallStartTime;
                existingCandidate.PreferredCallEndTime = candidate.PreferredCallEndTime ?? existingCandidate.PreferredCallEndTime;
                existingCandidate.LinkedInProfile = candidate.LinkedInProfile ?? existingCandidate.LinkedInProfile;
                existingCandidate.GitHubProfile = candidate.GitHubProfile ?? existingCandidate.GitHubProfile;
                existingCandidate.Comments = candidate.Comments;

                await _repository.UpdateAsync(existingCandidate);
            }
            else
            {
                _logger.LogInformation("Adding new candidate: {Email}", candidate.Email);
                await _repository.AddAsync(candidate);
            }

            await _repository.SaveChangesAsync();
        }
    }
}
