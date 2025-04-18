using System.Collections.Generic;
using System.Linq;
using Fluxor;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.State.AlertState
{
    /// <summary>
    /// Reducers for alert state management
    /// </summary>
    public static class AlertReducers
    {
        // Load Alerts Reducers
        [ReducerMethod]
        public static AlertState ReduceLoadAlertsAction(AlertState state, LoadAlertsAction action)
        {
            return state with
            {
                IsLoading = true,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertsSuccessAction(AlertState state, LoadAlertsSuccessAction action)
        {
            return state with
            {
                IsLoading = false,
                Alerts = action.Alerts,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertsFailureAction(AlertState state, LoadAlertsFailureAction action)
        {
            return state with
            {
                IsLoading = false,
                ErrorMessage = action.ErrorMessage
            };
        }

        // Load Single Alert Reducers
        [ReducerMethod]
        public static AlertState ReduceLoadAlertAction(AlertState state, LoadAlertAction action)
        {
            return state with
            {
                IsLoading = true,
                CurrentAlert = null,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertSuccessAction(AlertState state, LoadAlertSuccessAction action)
        {
            return state with
            {
                IsLoading = false,
                CurrentAlert = action.Alert,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceLoadAlertFailureAction(AlertState state, LoadAlertFailureAction action)
        {
            return state with
            {
                IsLoading = false,
                CurrentAlert = null,
                ErrorMessage = action.ErrorMessage
            };
        }

        // Create Alert Reducers
        [ReducerMethod]
        public static AlertState ReduceCreateAlertAction(AlertState state, CreateAlertAction action)
        {
            return state with
            {
                IsLoading = true,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceCreateAlertSuccessAction(AlertState state, CreateAlertSuccessAction action)
        {
            var alerts = new List<AlertDto>(state.Alerts)
            {
                action.Alert
            };

            return state with
            {
                IsLoading = false,
                Alerts = alerts,
                CurrentAlert = action.Alert,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceCreateAlertFailureAction(AlertState state, CreateAlertFailureAction action)
        {
            return state with
            {
                IsLoading = false,
                ErrorMessage = action.ErrorMessage
            };
        }

        // Update Alert Reducers
        [ReducerMethod]
        public static AlertState ReduceUpdateAlertAction(AlertState state, UpdateAlertAction action)
        {
            return state with
            {
                IsLoading = true,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceUpdateAlertSuccessAction(AlertState state, UpdateAlertSuccessAction action)
        {
            // Replace the updated alert in the list
            var updatedAlerts = state.Alerts
                .Select(a => a.Id == action.Alert.Id ? action.Alert : a)
                .ToList();

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
            return state with
            {
                IsLoading = false,
                ErrorMessage = action.ErrorMessage
            };
        }

        // Delete Alert Reducers
        [ReducerMethod]
        public static AlertState ReduceDeleteAlertAction(AlertState state, DeleteAlertAction action)
        {
            return state with
            {
                IsLoading = true,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceDeleteAlertSuccessAction(AlertState state, DeleteAlertSuccessAction action)
        {
            // Remove the deleted alert from the list
            var filteredAlerts = state.Alerts
                .Where(a => a.Id != action.Id)
                .ToList();

            return state with
            {
                IsLoading = false,
                Alerts = filteredAlerts,
                // Clear the current alert if it was the one deleted
                CurrentAlert = state.CurrentAlert?.Id == action.Id ? null : state.CurrentAlert,
                ErrorMessage = null
            };
        }

        [ReducerMethod]
        public static AlertState ReduceDeleteAlertFailureAction(AlertState state, DeleteAlertFailureAction action)
        {
            return state with
            {
                IsLoading = false,
                ErrorMessage = action.ErrorMessage
            };
        }

        // Error Handling Reducer
        [ReducerMethod]
        public static AlertState ReduceClearErrorAction(AlertState state, ClearErrorAction action)
        {
            return state with
            {
                ErrorMessage = null
            };
        }
    }
}