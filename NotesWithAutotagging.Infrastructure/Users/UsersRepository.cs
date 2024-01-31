using NotesWithAutotagging.Contracts.Models;
using NotesWithAutotagging.Database;

namespace NotesWithAutotagging.Infrastructure.Users
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly NotesWithAutotaggingDbContext notesWithAutotaggingDbContext;

        public UsersRepository(NotesWithAutotaggingDbContext notesWithAutotaggingDbContext)
        {
            this.notesWithAutotaggingDbContext = notesWithAutotaggingDbContext;
        }

        public User GetUser(string name, string password)
        {
            return notesWithAutotaggingDbContext.Users.FirstOrDefault(p => p.Name == name && p.Password == password)?.ToContractUser();
        }
    }
}
