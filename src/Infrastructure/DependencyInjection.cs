using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeTrader.Application.Interfaces;
using VibeTrader.Infrastructure.Data;
using VibeTrader.Infrastructure.Data.Repositories;

namespace VibeTrader.Infrastructure
{
    /// <summary>
    /// Contains extension methods for setting up infrastructure services in an IServiceCollection
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure services to the specified IServiceCollection
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="configuration">The configuration instance</param>
        /// <returns>The same service collection so that multiple calls can be chained</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register repositories
            services.AddScoped<IAlertRepository, AlertRepository>();

            return services;
        }
    }
}