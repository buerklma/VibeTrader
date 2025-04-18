namespace VibeTrader.Domain.Enums
{
    /// <summary>
    /// Defines the type of stock price alert
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// Alert triggers when price goes above target price
        /// </summary>
        PriceAbove = 1,

        /// <summary>
        /// Alert triggers when price goes below target price
        /// </summary>
        PriceBelow = 2
    }
}