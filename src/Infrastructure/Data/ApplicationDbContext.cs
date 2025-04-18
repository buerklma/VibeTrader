using Microsoft.EntityFrameworkCore;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Infrastructure.Data
{
    /// <summary>
    /// The main database context for the VibeTrader application
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class
        /// </summary>
        /// <param name="options">The options for this context</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the alerts database set
        /// </summary>
        public DbSet<Alert> Alerts { get; set; } = null!;

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}