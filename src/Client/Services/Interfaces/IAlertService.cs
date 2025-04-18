using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.Services.Interfaces
{
    /// <summary>
    /// Service for managing stock alerts
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Loads all alerts
        /// </summary>
        /// <param name="activeOnly">Whether to load active alerts only</param>
        Task LoadAlertsAsync(bool activeOnly = false);
        
        /// <summary>
        /// Gets a specific alert by ID
        /// </summary>
        /// <param name="id">The alert ID</param>
        /// <returns>The alert data</returns>
        Task<AlertDto> GetAlertAsync(Guid id);
        
        /// <summary>
        /// Creates a new alert
        /// </summary>
        /// <param name="symbol">The stock symbol</param>
        /// <param name="targetPrice">The target price</param>
        /// <param name="type">The alert type</param>
        /// <param name="notes">Optional notes</param>
        Task CreateAlertAsync(string symbol, decimal targetPrice, VibeTrader.Domain.Enums.AlertType type, string? notes);
        
        /// <summary>
        /// Updates an existing alert
        /// </summary>
        /// <param name="id">The alert ID</param>
        /// <param name="symbol">The stock symbol</param>
        /// <param name="targetPrice">The target price</param>
        /// <param name="type">The alert type</param>
        /// <param name="notes">Optional notes</param>
        Task UpdateAlertAsync(Guid id, string symbol, decimal targetPrice, VibeTrader.Domain.Enums.AlertType type, string? notes);
        
        /// <summary>
        /// Deletes an alert
        /// </summary>
        /// <param name="id">The alert ID</param>
        Task DeleteAlertAsync(Guid id);
        
        /// <summary>
        /// Clears the selected alert
        /// </summary>
        void ClearSelectedAlert();
        
        /// <summary>
        /// Clears any error messages
        /// </summary>
        void ClearError();
    }
}