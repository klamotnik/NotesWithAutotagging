using Microsoft.AspNetCore.Mvc;
using Moq;
using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Controllers;
using NotesWithAutotagging.Infrastructure.Notes;

namespace NotesWithAutotagging.Tests.Api.Controller
{
    [TestFixture]
    public class NotesControllerTests
    {
        [Test]
        public void GetList_ReturnsOkObjectResult_WithAListOfNotes()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.GetNotes())
                .Returns(GetTestNotes());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.GetList();

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var value = okObjectResult.Value as IEnumerable<Note>;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(2, value.Count());
        }

        [Test]
        public void GetItem_ReturnsOkObjectResult_WithANote()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.GetNote(1))
                .Returns(GetNote());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.GetItem(1);

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var value = okObjectResult.Value as Note;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(1, value.Id);
        }

        [Test]
        public void GetItem_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.GetNote(1))
                .Returns(GetNoteNull());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.GetItem(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public void Create_ReturnsOkObjectResult_WithANote()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.CreateNote("+48 123 456 789"))
                .Returns(GetNote());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.Create("+48 123 456 789");

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var value = okObjectResult.Value as Note;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(1, value.Id);
            Assert.AreEqual("+48 123 456 789", value.Content);
        }

        [Test]
        public void Update_ReturnsOkObjectResult_WithANote()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.EditNote(1, "+48 123 456 789"))
                .Returns(GetNote());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.Update(1, "+48 123 456 789");

            // Assert
            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var value = okObjectResult.Value as Note;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(1, value.Id);
            Assert.AreEqual("+48 123 456 789", value.Content);
        }

        [Test]
        public void Update_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.EditNote(1, "aaa"))
                .Returns(GetNoteNull());
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.Update(1, "aaa");

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        [Test]
        public void Delete_ReturnsOkResult()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.DeleteNote(1))
                .Returns(true);
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            var okObjectResult = result as OkResult;
            Assert.IsNotNull(okObjectResult);
        }

        [Test]
        public void Delete_ReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<INotesRepository>();
            mockRepo.Setup(repo => repo.DeleteNote(1))
                .Returns(false);
            var controller = new NotesController(mockRepo.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
        }

        private IEnumerable<Note> GetTestNotes()
        {
            return new[]
            {
                new Note
                {
                    Content = "+48 123 456 789",
                    Id = 1,
                    Tags = [ "PHONE" ]
                },

                new Note
                {
                    Content = "a@a.a",
                    Id = 2,
                    Tags = [ "EMAIL" ]
                }
            };
        }

        private Note GetNote()
        {
            return new Note
            {
                Content = "+48 123 456 789",
                Id = 1,
                Tags = ["PHONE"]
            };
        }

        private Note GetNoteAaa()
        {
            return new Note
            {
                Content = "aaa",
                Id = 1,
                Tags = []
            };
        }

        private Note GetNoteNull()
        {
            return null;
        }
    }
}
