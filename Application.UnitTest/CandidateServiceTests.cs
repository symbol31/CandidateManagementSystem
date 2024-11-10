using Moq;
using Domain.Services;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UnitTest
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _repositoryMock;
        private readonly Mock<ILogger<CandidateService>> _loggerMock;
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
        {
            _repositoryMock = new Mock<ICandidateRepository>();
            _loggerMock = new Mock<ILogger<CandidateService>>();
            _candidateService = new CandidateService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldAddNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "Ram", LastName = "Sharma" };
            _repositoryMock.Setup(r => r.GetByEmailAsync(candidate.Email)).ReturnsAsync((Candidate)null);

            // Act
            await _candidateService.UpsertCandidateAsync(candidate);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(candidate), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ShouldUpdateExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var candidate = new Candidate { Email = "test@example.com", FirstName = "Ram", LastName = "Sharma" };
            var existingCandidate = new Candidate { Email = "test@example.com", FirstName = "Shyam", LastName = "Prasad" };
            _repositoryMock.Setup(r => r.GetByEmailAsync(candidate.Email)).ReturnsAsync(existingCandidate);

            // Act
            await _candidateService.UpsertCandidateAsync(candidate);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(existingCandidate), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}

