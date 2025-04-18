using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Entities;
using VibeTrader.Infrastructure.Data;

namespace VibeTrader.Infrastructure.Services
{
    /// <summary>
    /// Background service that monitors stock prices and triggers alerts when conditions are met
    /// </summary>
    public class StockPriceMonitorService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<StockPriceMonitorService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1); // Check every minute

        public StockPriceMonitorService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<StockPriceMonitorService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        /// <summary>
        /// Executes the background service
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stock Price Monitor Service is starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAlertsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking alerts");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Stock Price Monitor Service is stopping");
        }

        /// <summary>
        /// Checks all active alerts and triggers them if conditions are met
        /// </summary>
        private async Task CheckAlertsAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking for alerts to process");

            using var scope = _serviceScopeFactory.CreateScope();
            var alertRepository = scope.ServiceProvider.GetRequiredService<IAlertRepository>();
            var stockPriceService = scope.ServiceProvider.GetRequiredService<IStockPriceService>();

            // Get all active alerts
            var alerts = await alertRepository.GetAllAsync(cancellationToken);
            var activeAlerts = alerts.Where(a => a.IsActive).ToList();

            if (activeAlerts.Count == 0)
            {
                _logger.LogInformation("No active alerts found to process");
                return;
            }

            _logger.LogInformation("Processing {Count} active alerts", activeAlerts.Count);

            // Group alerts by stock symbol to minimize API calls
            var alertsBySymbol = activeAlerts.GroupBy(a => a.Symbol);
            
            foreach (var group in alertsBySymbol)
            {
                string symbol = group.Key;
                List<Alert> symbolAlerts = group.ToList();

                try
                {
                    // Get current price for this symbol
                    decimal currentPrice = await stockPriceService.GetCurrentPriceAsync(symbol, cancellationToken);
                    _logger.LogInformation("Current price for {Symbol}: {Price}", symbol, currentPrice);

                    // Check each alert for this symbol
                    foreach (var alert in symbolAlerts)
                    {
                        if (alert.ShouldTrigger(currentPrice))
                        {
                            _logger.LogInformation("Triggering alert {AlertId} for {Symbol}", alert.Id, symbol);
                            alert.Trigger();
                            await alertRepository.UpdateAsync(alert, cancellationToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing alerts for symbol {Symbol}", symbol);
                }
            }

            // Save any changes to the database
            await alertRepository.SaveChangesAsync(cancellationToken);
        }
    }
}