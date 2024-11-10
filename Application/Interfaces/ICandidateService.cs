using Domain.Entities;

namespace Domain.Interfaces
{
    namespace CandidateManagement.Core.Interfaces
    {
        public interface ICandidateService
        {
            Task UpsertCandidateAsync(Candidate candidate);
        }
    }
}
