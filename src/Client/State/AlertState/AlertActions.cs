using System;
using System.Collections.Generic;
using VibeTrader.Application.DTOs;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Client.State.AlertState
{
    // Load Alerts Actions
    public record LoadAlertsAction(bool ActiveOnly);
    public record LoadAlertsSuccessAction(List<AlertDto> Alerts);
    public record LoadAlertsFailureAction(string ErrorMessage);

    // Load Single Alert Actions
    public record LoadAlertAction(Guid Id);
    public record LoadAlertSuccessAction(AlertDto Alert);
    public record LoadAlertFailureAction(string ErrorMessage);

    // Create Alert Actions
    public record CreateAlertAction(string Symbol, decimal TargetPrice, AlertType Type);
    public record CreateAlertSuccessAction(AlertDto Alert);
    public record CreateAlertFailureAction(string ErrorMessage);

    // Update Alert Actions
    public record UpdateAlertAction(Guid Id, string Symbol, decimal TargetPrice, AlertType Type);
    public record UpdateAlertSuccessAction(AlertDto Alert);
    public record UpdateAlertFailureAction(string ErrorMessage);

    // Delete Alert Actions
    public record DeleteAlertAction(Guid Id);
    public record DeleteAlertSuccessAction(Guid Id);
    public record DeleteAlertFailureAction(string ErrorMessage);

    // Clear Actions
    public record ClearCurrentAlertAction();
    public record ClearErrorAction();
}