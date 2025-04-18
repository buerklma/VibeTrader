namespace VibeTrader.Domain.Enums
{
    /// <summary>
    /// Defines the type of stock price alert
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// Alert triggers when the stock price goes above the target price
        /// </summary>
        Above = 1,
        
        /// <summary>
        /// Alert triggers when the stock price goes below the target price
        /// </summary>
        Below = 2
    }
}