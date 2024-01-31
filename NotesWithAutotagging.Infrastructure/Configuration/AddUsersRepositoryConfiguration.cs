using Microsoft.Extensions.DependencyInjection;
using NotesWithAutotagging.Infrastructure.Users;
using System.Diagnostics.CodeAnalysis;

namespace NotesWithAutotagging.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class AddUsersRepositoryConfiguration
    {
        public static void AddUsersRepository(this IServiceCollection services)
        { 
            services.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}
