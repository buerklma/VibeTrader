using System;
using VibeTrader.Domain.Enums;
using VibeTrader.Domain.Exceptions;

namespace VibeTrader.Domain.Entities
{
    /// <summary>
    /// Represents a stock price alert entity
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Unique identifier for the alert
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Stock symbol the alert is for (e.g., MSFT, AAPL)
        /// </summary>
        public string Symbol { get; private set; }

        /// <summary>
        /// Target price that will trigger the alert
        /// </summary>
        public decimal TargetPrice { get; private set; }

        /// <summary>
        /// Type of the alert (above or below the target price)
        /// </summary>
        public AlertType Type { get; private set; }

        /// <summary>
        /// Date and time when the alert was created
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Date and time when the alert was triggered, null if not triggered yet
        /// </summary>
        public DateTime? TriggeredOn { get; private set; }

        /// <summary>
        /// Indicates if the alert is currently active
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// User who created the alert
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// Optional notes about the alert
        /// </summary>
        public string? Notes { get; private set; }

        // For EF Core
        protected Alert() { }

        /// <summary>
        /// Creates a new stock alert
        /// </summary>
        public Alert(string symbol, decimal targetPrice, AlertType type, string createdBy, string? notes = null)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new AlertDomainException("Symbol cannot be empty");

            if (targetPrice <= 0)
                throw new AlertDomainException("Target price must be greater than zero");

            if (string.IsNullOrWhiteSpace(createdBy))
                throw new AlertDomainException("Creator must be specified");
            
            Id = Guid.NewGuid();
            Symbol = symbol.ToUpper().Trim();
            TargetPrice = targetPrice;
            Type = type;
            CreatedBy = createdBy;
            Notes = notes;
            CreatedOn = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Updates the alert properties
        /// </summary>
        public void Update(string symbol, decimal targetPrice, AlertType type, string? notes)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new AlertDomainException("Symbol cannot be empty");

            if (targetPrice <= 0)
                throw new AlertDomainException("Target price must be greater than zero");

            Symbol = symbol.ToUpper().Trim();
            TargetPrice = targetPrice;
            Type = type;
            Notes = notes;
        }

        /// <summary>
        /// Triggers the alert when the target price is reached
        /// </summary>
        public void Trigger()
        {
            if (!IsActive)
                throw new AlertDomainException("Cannot trigger an inactive alert");

            if (TriggeredOn.HasValue)
                throw new AlertDomainException("Alert has already been triggered");

            TriggeredOn = DateTime.UtcNow;
            IsActive = false;
        }

        /// <summary>
        /// Deactivates the alert
        /// </summary>
        public void Deactivate()
        {
            if (!IsActive)
                return;
                
            IsActive = false;
        }

        /// <summary>
        /// Reactivates the alert if it was deactivated but not triggered
        /// </summary>
        public void Reactivate()
        {
            if (IsActive)
                return;
                
            if (TriggeredOn.HasValue)
                throw new AlertDomainException("Cannot reactivate a triggered alert");
                
            IsActive = true;
        }

        /// <summary>
        /// Checks if the current stock price should trigger this alert
        /// </summary>
        public bool ShouldTrigger(decimal currentPrice)
        {
            if (!IsActive || TriggeredOn.HasValue)
                return false;

            return Type switch
            {
                AlertType.PriceAbove => currentPrice >= TargetPrice,
                AlertType.PriceBelow => currentPrice <= TargetPrice,
                _ => throw new AlertDomainException($"Unknown alert type: {Type}")
            };
        }
    }
}