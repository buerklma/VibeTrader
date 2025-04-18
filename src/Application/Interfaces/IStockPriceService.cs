using System.Threading;
using System.Threading.Tasks;

namespace VibeTrader.Application.Interfaces
{
    /// <summary>
    /// Service for retrieving stock price information
    /// </summary>
    public interface IStockPriceService
    {
        /// <summary>
        /// Gets the current price for a stock symbol
        /// </summary>
        /// <param name="symbol">The stock symbol (e.g., MSFT, AAPL)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The current price of the stock</returns>
        Task<decimal> GetCurrentPriceAsync(string symbol, CancellationToken cancellationToken);
    }
}