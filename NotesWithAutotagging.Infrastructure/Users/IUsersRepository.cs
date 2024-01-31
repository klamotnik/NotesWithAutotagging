using NotesWithAutotagging.Contracts.Models;

namespace NotesWithAutotagging.Infrastructure.Users
{
    public interface IUsersRepository
    {
        User GetUser(string name, string password);
    }
}
