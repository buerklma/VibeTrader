using System;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for stock alerts
    /// </summary>
    public class AlertDto
    {
        /// <summary>
        /// Unique identifier for the alert
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Stock symbol this alert is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Target price that triggers this alert
        /// </summary>
        public decimal TargetPrice { get; set; }
        
        /// <summary>
        /// Type of alert (Above or Below the target price)
        /// </summary>
        public AlertType Type { get; set; }
        
        /// <summary>
        /// Date and time when the alert was created
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Date and time when the alert was triggered, or null if not yet triggered
        /// </summary>
        public DateTime? TriggeredOn { get; set; }
        
        /// <summary>
        /// Indicates if the alert is currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// User who created the alert
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Optional notes about the alert
        /// </summary>
        public string? Notes { get; set; }
    }
}