using Microsoft.Extensions.DependencyInjection;
using NotesWithAutotagging.Infrastructure.Notes;
using System.Diagnostics.CodeAnalysis;

namespace NotesWithAutotagging.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class AddNotesRepositoryConfiguration
    {
        public static void AddNotesRepository(this IServiceCollection services)
        { 
            services.AddTransient<INotesRepository, NotesRepository>();
        }
    }
}
