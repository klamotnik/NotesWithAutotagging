using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Controllers;
using NotesWithAutotagging.Infrastructure.Notes;
using NotesWithAutotagging.Infrastructure.Users;

namespace NotesWithAutotagging.Tests.Api.Controller
{
    [TestFixture]
    public class TokenControllerTests
    {
        [Test]
        public void GenerateToken_ReturnsUnauthorized()
        {
            // Arrange
            var mockRepo = new Mock<IUsersRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockRepo.Setup(repo => repo.GetUser("dev", "123456"))
                .Returns(GetNull());
            var controller = new TokenController(mockRepo.Object, mockConfig.Object);

            // Act
            var result = controller.GenerateToken("dev", "123456");

            // Assert
            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
        }

        [Test]
        public void GenerateToken_ReturnsOkObjectResult_WithAToken()
        {
            // Arrange
            var mockRepo = new Mock<IUsersRepository>();
            var mockConfig = new Mock<IConfiguration>();
            mockRepo.Setup(repo => repo.GetUser("dev", "123456"))
                .Returns(GetUser());
            mockConfig.Setup(config => config["Jwt:Key"]).Returns("This is a sample secret key - please don't use in production environment.'");
            var controller = new TokenController(mockRepo.Object, mockConfig.Object);

            // Act
            var result = controller.GenerateToken("dev", "123456");

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
        }

        private User GetUser()
        {
            return new User
            {
                Id = 1,
                Name = "dev"
            };
        }

        private User GetNull()
        {
            return null;
        }
    }
}
