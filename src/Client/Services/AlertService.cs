using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using VibeTrader.Application.Commands.CreateAlert;
using VibeTrader.Application.Commands.UpdateAlert;
using VibeTrader.Application.DTOs;
using VibeTrader.Client.Services.Interfaces;
using VibeTrader.Client.State.AlertState;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Client.Services
{
    /// <summary>
    /// Service that handles alert operations and coordinates with Fluxor state
    /// </summary>
    public class AlertService : IAlertService
    {
        private readonly IDispatcher _dispatcher;
        private readonly ILogger<AlertService> _logger;
        private readonly IAlertApiService _alertApiService;

        public AlertService(
            IDispatcher dispatcher, 
            ILogger<AlertService> logger, 
            IAlertApiService alertApiService)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            _alertApiService = alertApiService;
        }

        /// <inheritdoc/>
        public Task<List<AlertDto>> GetAlertsAsync(bool activeOnly = false)
        {
            _logger.LogInformation("Getting alerts (ActiveOnly: {ActiveOnly})", activeOnly);
            return _alertApiService.GetAlertsAsync(activeOnly);
        }

        /// <inheritdoc/>
        public Task<AlertDto> GetAlertByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting alert with ID: {AlertId}", id);
            return _alertApiService.GetAlertByIdAsync(id);
        }

        /// <inheritdoc/>
        public Task LoadAlertsAsync(bool activeOnly = false)
        {
            _logger.LogInformation("Loading alerts (ActiveOnly: {ActiveOnly})", activeOnly);
            _dispatcher.Dispatch(new LoadAlertsAction(activeOnly));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<AlertDto> GetAlertAsync(Guid id)
        {
            _logger.LogInformation("Getting alert with ID: {AlertId}", id);
            _dispatcher.Dispatch(new LoadAlertAction(id));
            // Call the API service to get the alert data
            return _alertApiService.GetAlertByIdAsync(id);
        }

        /// <inheritdoc/>
        public Task<AlertDto> CreateAlertAsync(string symbol, decimal targetPrice, AlertType type)
        {
            _logger.LogInformation("Creating alert for symbol {Symbol} at price {TargetPrice}", symbol, targetPrice);
            
            var command = new CreateAlertCommand
            {
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type,
                CreatedBy = "User" // In a real app, this would come from authentication
            };
            
            return _alertApiService.CreateAlertAsync(command);
        }

        /// <inheritdoc/>
        public Task CreateAlertAsync(string symbol, decimal targetPrice, AlertType type, string? notes)
        {
            _logger.LogInformation("Creating alert through state for symbol {Symbol}", symbol);
            
            _dispatcher.Dispatch(new CreateAlertAction(symbol, targetPrice, type));
            
            var command = new CreateAlertCommand
            {
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type,
                Notes = notes,
                CreatedBy = "User" // In a real app, this would come from authentication
            };
            
            return _alertApiService.CreateAlertAsync(command);
        }

        /// <inheritdoc/>
        public Task<AlertDto> UpdateAlertAsync(Guid id, string symbol, decimal targetPrice, AlertType type)
        {
            _logger.LogInformation("Updating alert with ID: {AlertId}", id);
            
            var command = new UpdateAlertCommand
            {
                Id = id,
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type
            };
            
            return _alertApiService.UpdateAlertAsync(command);
        }

        /// <inheritdoc/>
        public Task UpdateAlertAsync(Guid id, string symbol, decimal targetPrice, AlertType type, string? notes)
        {
            _logger.LogInformation("Updating alert through state with ID: {AlertId}", id);
            
            _dispatcher.Dispatch(new UpdateAlertAction(id, symbol, targetPrice, type));
            
            var command = new UpdateAlertCommand
            {
                Id = id,
                Symbol = symbol,
                TargetPrice = targetPrice,
                Type = type,
                Notes = notes
            };
            
            return _alertApiService.UpdateAlertAsync(command);
        }

        /// <inheritdoc/>
        public Task DeleteAlertAsync(Guid id)
        {
            _logger.LogInformation("Deleting alert with ID: {AlertId}", id);
            _dispatcher.Dispatch(new DeleteAlertAction(id));
            return _alertApiService.DeleteAlertAsync(id);
        }

        /// <inheritdoc/>
        public void ClearSelectedAlert()
        {
            _logger.LogDebug("Clearing selected alert");
            _dispatcher.Dispatch(new ClearCurrentAlertAction());
        }

        /// <inheritdoc/>
        public void ClearError()
        {
            _logger.LogDebug("Clearing error message");
            _dispatcher.Dispatch(new ClearErrorAction());
        }
    }
}