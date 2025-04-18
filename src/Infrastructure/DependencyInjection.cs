using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VibeTrader.Application.Interfaces;
using VibeTrader.Infrastructure.Data;
using VibeTrader.Infrastructure.Services;

namespace VibeTrader.Infrastructure
{
    /// <summary>
    /// Extension methods for configuring infrastructure services
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure services to the service collection
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Register repositories
            services.AddScoped<IAlertRepository, AlertRepository>();
            
            // Register services
            services.AddScoped<IStockPriceService, StockPriceService>();

            // Register background services
            services.AddHostedService<StockPriceMonitorService>();

            return services;
        }
    }
}