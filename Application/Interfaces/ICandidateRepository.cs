using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task SaveChangesAsync();
    }
}
