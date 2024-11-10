using Domain.Entities;
using Domain.Interfaces.CandidateManagement.Core.Interfaces;
using Domain.Interfaces;

namespace Domain.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;

        public CandidateService(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task UpsertCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);

            if (existingCandidate != null)
            {
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
                await _repository.AddAsync(candidate);
            }

            await _repository.SaveChangesAsync();
        }
    }
}
