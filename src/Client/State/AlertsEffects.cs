using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using VibeTrader.Client.Services.Interfaces;

namespace VibeTrader.Client.State
{
    /// <summary>
    /// Fluxor effects for handling alert-related asynchronous operations
    /// </summary>
    public class AlertsEffects
    {
        private readonly IAlertApiService _alertApiService;
        private readonly ILogger<AlertsEffects> _logger;

        public AlertsEffects(IAlertApiService alertApiService, ILogger<AlertsEffects> logger)
        {
            _alertApiService = alertApiService;
            _logger = logger;
        }

        [EffectMethod]
        public async Task HandleLoadAlertsAction(LoadAlertsAction action, IDispatcher dispatcher)
        {
            try
            {
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
        public async Task HandleGetAlertAction(GetAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var alert = await _alertApiService.GetAlertByIdAsync(action.AlertId);
                dispatcher.Dispatch(new GetAlertSuccessAction(alert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting alert with ID {AlertId}", action.AlertId);
                dispatcher.Dispatch(new GetAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleCreateAlertAction(CreateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var createdAlert = await _alertApiService.CreateAlertAsync(action.Command);
                dispatcher.Dispatch(new CreateAlertSuccessAction(createdAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating alert for symbol {Symbol}", action.Command.Symbol);
                dispatcher.Dispatch(new CreateAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleUpdateAlertAction(UpdateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var updatedAlert = await _alertApiService.UpdateAlertAsync(action.Command);
                dispatcher.Dispatch(new UpdateAlertSuccessAction(updatedAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating alert with ID {AlertId}", action.Command.Id);
                dispatcher.Dispatch(new UpdateAlertFailureAction(ex.Message));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteAlertAction(DeleteAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                await _alertApiService.DeleteAlertAsync(action.AlertId);
                dispatcher.Dispatch(new DeleteAlertSuccessAction(action.AlertId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting alert with ID {AlertId}", action.AlertId);
                dispatcher.Dispatch(new DeleteAlertFailureAction(ex.Message));
            }
        }
    }
}