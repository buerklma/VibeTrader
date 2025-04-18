using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.Extensions.Logging;
using VibeTrader.Client.Services;

namespace VibeTrader.Client.State.AlertState
{
    /// <summary>
    /// Effects for alert state management
    /// </summary>
    public class AlertEffects
    {
        private readonly IAlertService _alertService;
        private readonly ILogger<AlertEffects> _logger;

        public AlertEffects(IAlertService alertService, ILogger<AlertEffects> logger)
        {
            _alertService = alertService;
            _logger = logger;
        }

        [EffectMethod]
        public async Task HandleLoadAlertsAction(LoadAlertsAction action, IDispatcher dispatcher)
        {
            try
            {
                var alerts = await _alertService.GetAlertsAsync(action.ActiveOnly);
                dispatcher.Dispatch(new LoadAlertsSuccessAction(alerts));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading alerts");
                dispatcher.Dispatch(new LoadAlertsFailureAction($"Error loading alerts: {ex.Message}"));
            }
        }

        [EffectMethod]
        public async Task HandleLoadAlertAction(LoadAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var alert = await _alertService.GetAlertByIdAsync(action.Id);
                dispatcher.Dispatch(new LoadAlertSuccessAction(alert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading alert {AlertId}", action.Id);
                dispatcher.Dispatch(new LoadAlertFailureAction($"Error loading alert: {ex.Message}"));
            }
        }

        [EffectMethod]
        public async Task HandleCreateAlertAction(CreateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var createdAlert = await _alertService.CreateAlertAsync(
                    action.Symbol,
                    action.TargetPrice,
                    action.Type);
                
                dispatcher.Dispatch(new CreateAlertSuccessAction(createdAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating alert for {Symbol}", action.Symbol);
                dispatcher.Dispatch(new CreateAlertFailureAction($"Error creating alert: {ex.Message}"));
            }
        }

        [EffectMethod]
        public async Task HandleUpdateAlertAction(UpdateAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                var updatedAlert = await _alertService.UpdateAlertAsync(
                    action.Id,
                    action.Symbol,
                    action.TargetPrice,
                    action.Type);
                
                dispatcher.Dispatch(new UpdateAlertSuccessAction(updatedAlert));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating alert {AlertId}", action.Id);
                dispatcher.Dispatch(new UpdateAlertFailureAction($"Error updating alert: {ex.Message}"));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteAlertAction(DeleteAlertAction action, IDispatcher dispatcher)
        {
            try
            {
                await _alertService.DeleteAlertAsync(action.Id);
                dispatcher.Dispatch(new DeleteAlertSuccessAction(action.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting alert {AlertId}", action.Id);
                dispatcher.Dispatch(new DeleteAlertFailureAction($"Error deleting alert: {ex.Message}"));
            }
        }
    }
}