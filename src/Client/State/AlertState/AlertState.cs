using System;
using System.Collections.Generic;
using Fluxor;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.State.AlertState
{
    /// <summary>
    /// State container for alerts
    /// </summary>
    public record AlertState
    {
        public bool IsLoading { get; init; }
        public List<AlertDto> Alerts { get; init; } = new();
        public AlertDto? CurrentAlert { get; init; }
        public string? ErrorMessage { get; init; }
    }

    /// <summary>
    /// Feature class for Fluxor state management
    /// </summary>
    public class AlertFeature : Feature<AlertState>
    {
        public override string GetName() => "Alerts";

        protected override AlertState GetInitialState()
        {
            return new AlertState
            {
                IsLoading = false,
                Alerts = new List<AlertDto>(),
                CurrentAlert = null,
                ErrorMessage = null
            };
        }
    }
}