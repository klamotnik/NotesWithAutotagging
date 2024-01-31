using NotesWithAutotagging.Database.Models;
using NotesWithAutotagging.Infrastructure.Users;

namespace NotesWithAutotagging.Tests.Infrastructure.Notes
{
    [TestFixture]
    public class UserMapperTest
    {
        [Test]
        public void UserMapper_FromDatabaseUser_ToContractsNote()
        {
            var userDb = new User
            {
                Id = 1,
                Name = "dev",
                Password = "123456"
            };

            var user = userDb.ToContractUser();

            Assert.IsNotNull(user);
            Assert.AreEqual(userDb.Id, user.Id);
            Assert.AreEqual(userDb.Name, user.Name);
        }
    }
}
