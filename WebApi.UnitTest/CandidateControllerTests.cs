using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Dtos;

namespace WebApi.UnitTest
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidateController _controller;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidateController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Required");
            var dto = new UpsertCandidateDto();

            // Act
            var result = await _controller.AddOrUpdateCandidate(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_ReturnsOk_WhenModelStateIsValid()
        {
            // Arrange
            var dto = new UpsertCandidateDto
            {
                Email = "test@example.com",
                FirstName = "Ram",
                LastName = "Sharma",
                PhoneNumber = "9867453215",
                PreferredCallStartTime = TimeSpan.FromHours(10),
                PreferredCallEndTime = TimeSpan.FromHours(18),
                LinkedInProfile = "https://linkedin.com/in/ramsharma",
                GitHubProfile = "https://github.com/ramsharma",
                Comments = "Sample comment"
            };

            // Act
            var result = await _controller.AddOrUpdateCandidate(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AddOrUpdateCandidate_CallsUpsertCandidateAsync_WithCorrectCandidate()
        {
            // Arrange
            var dto = new UpsertCandidateDto
            {
                Email = "test@example.com",
                FirstName = "Ram",
                LastName = "Sharma",
                PhoneNumber = "9867453215",
                PreferredCallStartTime = TimeSpan.FromHours(10),
                PreferredCallEndTime = TimeSpan.FromHours(18),
                LinkedInProfile = "https://linkedin.com/in/ramsharma",
                GitHubProfile = "https://github.com/ramsharma",
                Comments = "Sample comment"
            };

            // Act
            await _controller.AddOrUpdateCandidate(dto);

            // Assert
            _mockCandidateService.Verify(service => service.UpsertCandidateAsync(It.Is<Candidate>(c =>
                c.Email == dto.Email &&
                c.FirstName == dto.FirstName &&
                c.LastName == dto.LastName &&
                c.PhoneNumber == dto.PhoneNumber &&
                c.PreferredCallStartTime == dto.PreferredCallStartTime &&
                c.PreferredCallEndTime == dto.PreferredCallEndTime &&
                c.LinkedInProfile == dto.LinkedInProfile &&
                c.GitHubProfile == dto.GitHubProfile &&
                c.Comments == dto.Comments
            )), Times.Once);
        }
    }
}
