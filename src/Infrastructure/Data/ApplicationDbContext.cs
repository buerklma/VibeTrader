using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core database context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Alerts DbSet
        /// </summary>
        public DbSet<Alert> Alerts { get; set; } = null!;

        /// <summary>
        /// Creates a new instance of the ApplicationDbContext
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configure the model that was discovered by convention from the entity types
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}