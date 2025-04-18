using System.Collections.Generic;
using System.Linq;
using Fluxor;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.State
{
    public static class AlertsReducers
    {
        // Load Alerts reducers
        [ReducerMethod]
        public static AlertsState ReduceLoadAlertsAction(AlertsState state, LoadAlertsAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: true,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceLoadAlertsSuccessAction(AlertsState state, LoadAlertsSuccessAction action) =>
            new(
                alerts: action.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: null,
                isInitialized: true
            );

        [ReducerMethod]
        public static AlertsState ReduceLoadAlertsFailureAction(AlertsState state, LoadAlertsFailureAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: action.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        // Get Alert reducers
        [ReducerMethod]
        public static AlertsState ReduceGetAlertAction(AlertsState state, GetAlertAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: true,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceGetAlertSuccessAction(AlertsState state, GetAlertSuccessAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: action.Alert,
                isLoading: false,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceGetAlertFailureAction(AlertsState state, GetAlertFailureAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: null,
                isLoading: false,
                errorMessage: action.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        // Create Alert reducers
        [ReducerMethod]
        public static AlertsState ReduceCreateAlertAction(AlertsState state, CreateAlertAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: true,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceCreateAlertSuccessAction(AlertsState state, CreateAlertSuccessAction action)
        {
            var newAlerts = new List<AlertDto>(state.Alerts) { action.Alert };
            return new AlertsState(
                alerts: newAlerts,
                currentAlert: action.Alert,
                isLoading: false,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );
        }

        [ReducerMethod]
        public static AlertsState ReduceCreateAlertFailureAction(AlertsState state, CreateAlertFailureAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: action.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        // Update Alert reducers
        [ReducerMethod]
        public static AlertsState ReduceUpdateAlertAction(AlertsState state, UpdateAlertAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: true,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceUpdateAlertSuccessAction(AlertsState state, UpdateAlertSuccessAction action)
        {
            var updatedAlerts = state.Alerts.Select(alert => 
                alert.Id == action.Alert.Id ? action.Alert : alert).ToList();
                
            return new AlertsState(
                alerts: updatedAlerts,
                currentAlert: action.Alert,
                isLoading: false,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );
        }

        [ReducerMethod]
        public static AlertsState ReduceUpdateAlertFailureAction(AlertsState state, UpdateAlertFailureAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: action.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        // Delete Alert reducers
        [ReducerMethod]
        public static AlertsState ReduceDeleteAlertAction(AlertsState state, DeleteAlertAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: true,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceDeleteAlertSuccessAction(AlertsState state, DeleteAlertSuccessAction action)
        {
            var filteredAlerts = state.Alerts.Where(alert => alert.Id != action.AlertId).ToList();
            var newCurrentAlert = state.CurrentAlert?.Id == action.AlertId ? null : state.CurrentAlert;
            
            return new AlertsState(
                alerts: filteredAlerts,
                currentAlert: newCurrentAlert,
                isLoading: false,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );
        }

        [ReducerMethod]
        public static AlertsState ReduceDeleteAlertFailureAction(AlertsState state, DeleteAlertFailureAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: action.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        // Clear actions
        [ReducerMethod]
        public static AlertsState ReduceClearCurrentAlertAction(AlertsState state, ClearCurrentAlertAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: null,
                isLoading: false,
                errorMessage: state.ErrorMessage,
                isInitialized: state.IsInitialized
            );

        [ReducerMethod]
        public static AlertsState ReduceClearErrorAction(AlertsState state, ClearErrorAction action) =>
            new(
                alerts: state.Alerts,
                currentAlert: state.CurrentAlert,
                isLoading: false,
                errorMessage: null,
                isInitialized: state.IsInitialized
            );
    }
}