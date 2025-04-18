using System;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Domain.Entities
{
    /// <summary>
    /// Represents a stock price alert that can be triggered when conditions are met
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Unique identifier for the alert
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Stock symbol this alert is for (e.g., AAPL, MSFT)
        /// </summary>
        public string Symbol { get; private set; }
        
        /// <summary>
        /// Target price that triggers this alert
        /// </summary>
        public decimal TargetPrice { get; private set; }
        
        /// <summary>
        /// Type of alert (Above or Below the target price)
        /// </summary>
        public AlertType Type { get; private set; }
        
        /// <summary>
        /// Date and time when the alert was created
        /// </summary>
        public DateTime CreatedOn { get; private set; }
        
        /// <summary>
        /// Date and time when the alert was triggered, or null if not yet triggered
        /// </summary>
        public DateTime? TriggeredOn { get; private set; }
        
        /// <summary>
        /// Indicates if the alert is currently active
        /// </summary>
        public bool IsActive { get; private set; }

        // Private constructor for EF Core
        private Alert() { }

        /// <summary>
        /// Creates a new stock alert
        /// </summary>
        public Alert(string symbol, decimal targetPrice, AlertType type)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Symbol cannot be empty", nameof(symbol));

            Id = Guid.NewGuid();
            Symbol = symbol.ToUpper().Trim();
            TargetPrice = targetPrice;
            Type = type;
            CreatedOn = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Updates the alert properties
        /// </summary>
        public void Update(string symbol, decimal targetPrice, AlertType type)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Symbol cannot be empty", nameof(symbol));

            Symbol = symbol.ToUpper().Trim();
            TargetPrice = targetPrice;
            Type = type;
        }

        /// <summary>
        /// Triggers the alert, marking it as inactive and recording the trigger time
        /// </summary>
        public void Trigger()
        {
            if (!IsActive)
                return;

            TriggeredOn = DateTime.UtcNow;
            IsActive = false;
        }

        /// <summary>
        /// Reactivates the alert if it was previously triggered
        /// </summary>
        public void Reactivate()
        {
            TriggeredOn = null;
            IsActive = true;
        }

        /// <summary>
        /// Deactivates the alert without triggering it
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}