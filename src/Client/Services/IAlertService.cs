using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VibeTrader.Application.DTOs;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Client.Services
{
    /// <summary>
    /// Service for managing stock alerts
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Gets all alerts
        /// </summary>
        /// <param name="activeOnly">Whether to return only active alerts</param>
        Task<List<AlertDto>> GetAlertsAsync(bool activeOnly = false);
        
        /// <summary>
        /// Gets an alert by ID
        /// </summary>
        /// <param name="id">The ID of the alert</param>
        Task<AlertDto> GetAlertByIdAsync(Guid id);
        
        /// <summary>
        /// Creates a new alert
        /// </summary>
        /// <param name="symbol">The stock symbol</param>
        /// <param name="targetPrice">The target price</param>
        /// <param name="type">The alert type</param>
        Task<AlertDto> CreateAlertAsync(string symbol, decimal targetPrice, AlertType type);
        
        /// <summary>
        /// Updates an existing alert
        /// </summary>
        /// <param name="id">The ID of the alert</param>
        /// <param name="symbol">The stock symbol</param>
        /// <param name="targetPrice">The target price</param>
        /// <param name="type">The alert type</param>
        Task<AlertDto> UpdateAlertAsync(Guid id, string symbol, decimal targetPrice, AlertType type);
        
        /// <summary>
        /// Deletes an alert
        /// </summary>
        /// <param name="id">The ID of the alert to delete</param>
        Task DeleteAlertAsync(Guid id);
    }
}