using Domain.Entities;

namespace Application.Interfaces
{

    public interface ICandidateService
    {
        Task UpsertCandidateAsync(Candidate candidate);
    }
}
