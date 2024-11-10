using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CandidateRepostiory
    {
        public class CandidateRepository : ICandidateRepository
        {
            private readonly CandidateDbContext _context;

            public CandidateRepository(CandidateDbContext context)
            {
                _context = context;
            }

            public async Task<Candidate?> GetByEmailAsync(string email)
            {
                return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
            }

            public async Task AddAsync(Candidate candidate)
            {
                await _context.Candidates.AddAsync(candidate);
            }

            public Task UpdateAsync(Candidate candidate)
            {
                _context.Candidates.Update(candidate);
                return Task.CompletedTask;
            }

            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}
