using Fluxor;
using System.Collections.Generic;
using VibeTrader.Application.DTOs;
using VibeTrader.Client.State.AlertState;

namespace VibeTrader.Client.State.AlertState
{
    /// <summary>
    /// Reducers for the alert state changes in the Fluxor store
    /// </summary>
    public static class AlertReducers
    {
        [ReducerMethod]
        public static AlertState ReduceLoadAlertsAction(AlertState state, LoadAlertsAction action)
        {
            return state with { IsLoading = true, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertsSuccessAction(AlertState state, LoadAlertsSuccessAction action)
        {
            return state with { IsLoading = false, Alerts = action.Alerts, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertsFailureAction(AlertState state, LoadAlertsFailureAction action)
        {
            return state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertAction(AlertState state, LoadAlertAction action)
        {
            return state with { IsLoading = true, CurrentAlert = null, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertSuccessAction(AlertState state, LoadAlertSuccessAction action)
        {
            return state with { IsLoading = false, CurrentAlert = action.Alert, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertFailureAction(AlertState state, LoadAlertFailureAction action)
        {
            return state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
        }

        [ReducerMethod]
        public static AlertState ReduceCreateAlertAction(AlertState state, CreateAlertAction action)
        {
            return state with { IsLoading = true, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceCreateAlertSuccessAction(AlertState state, CreateAlertSuccessAction action)
        {
            var updatedAlerts = new List<AlertDto>(state.Alerts) { action.Alert };
            return state with { IsLoading = false, Alerts = updatedAlerts, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceCreateAlertFailureAction(AlertState state, CreateAlertFailureAction action)
        {
            return state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
        }

        [ReducerMethod]
        public static AlertState ReduceUpdateAlertAction(AlertState state, UpdateAlertAction action)
        {
            return state with { IsLoading = true, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceUpdateAlertSuccessAction(AlertState state, UpdateAlertSuccessAction action)
        {
            // Update the alert in the list
            var updatedAlerts = new List<AlertDto>();
            foreach (var alert in state.Alerts)
            {
                if (alert.Id == action.Alert.Id)
                {
                    updatedAlerts.Add(action.Alert);
                }
                else
                {
                    updatedAlerts.Add(alert);
                }
            }

            return state with 
            { 
                IsLoading = false, 
                Alerts = updatedAlerts, 
                CurrentAlert = action.Alert, 
                ErrorMessage = null 
            };
        }

        [ReducerMethod]
        public static AlertState ReduceUpdateAlertFailureAction(AlertState state, UpdateAlertFailureAction action)
        {
            return state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
        }

        [ReducerMethod]
        public static AlertState ReduceDeleteAlertAction(AlertState state, DeleteAlertAction action)
        {
            return state with { IsLoading = true, ErrorMessage = null };
        }

        [ReducerMethod]
        public static AlertState ReduceDeleteAlertSuccessAction(AlertState state, DeleteAlertSuccessAction action)
        {
            // Remove the alert from the list
            var updatedAlerts = state.Alerts.FindAll(a => a.Id != action.Id);
            
            // Clear the current alert if it was deleted
            var currentAlert = state.CurrentAlert?.Id == action.Id ? null : state.CurrentAlert;

            return state with 
            { 
                IsLoading = false, 
                Alerts = updatedAlerts, 
                CurrentAlert = currentAlert, 
                ErrorMessage = null 
            };
        }

        [ReducerMethod]
        public static AlertState ReduceDeleteAlertFailureAction(AlertState state, DeleteAlertFailureAction action)
        {
            return state with { IsLoading = false, ErrorMessage = action.ErrorMessage };
        }

        [ReducerMethod]
        public static AlertState ReduceClearCurrentAlertAction(AlertState state, ClearCurrentAlertAction action)
        {
            return state with { CurrentAlert = null };
        }

        [ReducerMethod]
        public static AlertState ReduceClearErrorAction(AlertState state, ClearErrorAction action)
        {
            return state with { ErrorMessage = null };
        }
    }
}