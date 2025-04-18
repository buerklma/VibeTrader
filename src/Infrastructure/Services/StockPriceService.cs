using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VibeTrader.Application.Interfaces;

namespace VibeTrader.Infrastructure.Services
{
    /// <summary>
    /// Implementation of IStockPriceService that retrieves stock price data
    /// </summary>
    public class StockPriceService : IStockPriceService
    {
        private readonly ILogger<StockPriceService> _logger;
        private readonly Random _random; // Using random for demo purposes

        public StockPriceService(ILogger<StockPriceService> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        /// <summary>
        /// Gets the current price for a stock symbol
        /// In a real application, this would call an external API
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., MSFT, AAPL)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The current price of the stock</returns>
        public Task<decimal> GetCurrentPriceAsync(string symbol, CancellationToken cancellationToken)
        {
            // NOTE: This is a mock implementation for demo purposes
            // In a real application, this would call an external stock price API
            _logger.LogInformation("Fetching current price for {Symbol}", symbol);

            // Generate a random price between 10 and 1000
            decimal price = Math.Round((decimal)(_random.NextDouble() * 990 + 10), 2);
            
            _logger.LogInformation("Current price for {Symbol}: {Price}", symbol, price);
            
            return Task.FromResult(price);
        }
    }
}