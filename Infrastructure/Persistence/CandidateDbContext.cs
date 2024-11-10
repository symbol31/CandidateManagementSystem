using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CandidateDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

        }
    }
}
