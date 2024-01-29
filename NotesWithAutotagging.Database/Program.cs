using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace test
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var serviceProvider = CreateServices())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    // Put the database update into a scope to ensure
                    // that all resources will be disposed.
                    UpdateDatabase(scope.ServiceProvider);
                }
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static ServiceProvider CreateServices()
        {
            Configuration.GetConnectionString("PgDatabase");
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddPostgres()
                    // Set the connection string
                    .WithGlobalConnectionString("ConnectionStrings.PgDatabase")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}