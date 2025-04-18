using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VibeTrader.Infrastructure.Data
{
    /// <summary>
    /// Factory for creating DbContext instances during design-time operations like migrations
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Get the directory of the current assembly
            var projectRootPath = Directory.GetCurrentDirectory();
            
            // Build a configuration from appsettings.json in the API project
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(projectRootPath, "..", "API"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            // Create DbContextOptionsBuilder
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}