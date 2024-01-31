using NotesWithAutotagging.Infrastructure.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace NotesWithAutotagging.Api.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class AddRepositoriesConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddNotesRepository();
        }
    }
}
