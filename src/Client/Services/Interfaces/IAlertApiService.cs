using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VibeTrader.Application.Commands.CreateAlert;
using VibeTrader.Application.Commands.UpdateAlert;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.Services.Interfaces
{
    /// <summary>
    /// Service interface for communicating with the Alerts API
    /// </summary>
    public interface IAlertApiService
    {
        /// <summary>
        /// Gets all alerts with optional filter for active alerts only
        /// </summary>
        Task<List<AlertDto>> GetAlertsAsync(bool activeOnly = false);
        
        /// <summary>
        /// Gets a specific alert by ID
        /// </summary>
        Task<AlertDto> GetAlertByIdAsync(Guid id);
        
        /// <summary>
        /// Creates a new alert
        /// </summary>
        Task<AlertDto> CreateAlertAsync(CreateAlertCommand command);
        
        /// <summary>
        /// Updates an existing alert
        /// </summary>
        Task<AlertDto> UpdateAlertAsync(UpdateAlertCommand command);
        
        /// <summary>
        /// Deletes an alert
        /// </summary>
        Task DeleteAlertAsync(Guid id);
    }
}