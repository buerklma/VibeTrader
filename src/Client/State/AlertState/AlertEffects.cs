using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using VibeTrader.Client.Services.Interfaces;

namespace VibeTrader.Client.State.AlertState
{
    /// <summary>
    /// Effects for handling side effects of alert state changes
    /// </summary>
    public class AlertEffects
    {
        private readonly IAlertApiService _alertApiService;
        private readonly ILogger<AlertEffects> _logger;

        public AlertEffects(IAlertApiService alertApiService, ILogger<AlertEffects> logger)
        {
            _alertApiService = alertApiService;
            _logger = logger;
        }

        [EffectMethod]
        public async Task HandleLoadAlertsAction(LoadAlertsAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading alerts from API (ActiveOnly: {ActiveOnly})", action.ActiveOnly);
                var alerts = await _alertApiService.GetAlertsAsync(action.ActiveOnly);
                dispatcher.Dispatch(new LoadAlertsSuccessAction(alerts));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading alerts");
                dispatcher.Dispatch(new LoadAlertsFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleLoadAlertAction(LoadAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Loading alert details for ID: {AlertId}", action.Id);
                var alert = await _alertApiService.GetAlertByIdAsync(action.Id);
                dispatcher.Dispatch(new LoadAlertSuccessAction(alert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading alert with ID {AlertId}", action.Id);
                dispatcher.Dispatch(new LoadAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleCreateAlertAction(CreateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Creating alert for symbol: {Symbol}", action.Symbol);
                
                var command = new VibeTrader.Application.Commands.CreateAlert.CreateAlertCommand
                {
                    Symbol = action.Symbol,
                    TargetPrice = action.TargetPrice,
                    Type = action.Type,
                    CreatedBy = "User" // In a real app, this would come from authentication
                };
                
                var createdAlert = await _alertApiService.CreateAlertAsync(command);
                dispatcher.Dispatch(new CreateAlertSuccessAction(createdAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating alert for symbol {Symbol}", action.Symbol);
                dispatcher.Dispatch(new CreateAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleUpdateAlertAction(UpdateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Updating alert with ID: {AlertId}", action.Id);
                
                var command = new VibeTrader.Application.Commands.UpdateAlert.UpdateAlertCommand
                {
                    Id = action.Id,
                    Symbol = action.Symbol,
                    TargetPrice = action.TargetPrice,
                    Type = action.Type
                };
                
                var updatedAlert = await _alertApiService.UpdateAlertAsync(command);
                dispatcher.Dispatch(new UpdateAlertSuccessAction(updatedAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating alert with ID {AlertId}", action.Id);
                dispatcher.Dispatch(new UpdateAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteAlertAction(DeleteAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                _logger.LogInformation("Deleting alert with ID: {AlertId}", action.Id);
                await _alertApiService.DeleteAlertAsync(action.Id);
                dispatcher.Dispatch(new DeleteAlertSuccessAction(action.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting alert with ID {AlertId}", action.Id);
                dispatcher.Dispatch(new DeleteAlertFailureAction(ex.Message));
            }
        }
    }
}