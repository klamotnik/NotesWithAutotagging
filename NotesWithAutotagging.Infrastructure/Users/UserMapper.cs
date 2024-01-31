namespace NotesWithAutotagging.Infrastructure.Users
{
    public static class UserMapper
    {
        public static Contracts.Models.User ToContractUser(this Database.Models.User user)
        {
            return new Contracts.Models.User
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
