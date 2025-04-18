using System.Collections.Generic;
using Fluxor;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.State
{
    /// <summary>
    /// Represents the application state for alerts
    /// </summary>
    [FeatureState]
    public class AlertsState
    {
        /// <summary>
        /// List of all alerts in the state
        /// </summary>
        public List<AlertDto> Alerts { get; }
        
        /// <summary>
        /// Currently selected alert (for editing)
        /// </summary>
        public AlertDto? CurrentAlert { get; }
        
        /// <summary>
        /// Flag indicating if data is being loaded
        /// </summary>
        public bool IsLoading { get; }
        
        /// <summary>
        /// Error message, if any
        /// </summary>
        public string? ErrorMessage { get; }
        
        /// <summary>
        /// Flag indicating if data has been loaded at least once
        /// </summary>
        public bool IsInitialized { get; }

        private AlertsState() 
        {
            Alerts = new List<AlertDto>();
            CurrentAlert = null;
            IsLoading = false;
            ErrorMessage = null;
            IsInitialized = false;
        }

        public AlertsState(
            List<AlertDto> alerts, 
            AlertDto? currentAlert,
            bool isLoading, 
            string? errorMessage,
            bool isInitialized)
        {
            Alerts = alerts;
            CurrentAlert = currentAlert;
            IsLoading = isLoading;
            ErrorMessage = errorMessage;
            IsInitialized = isInitialized;
        }
    }
}