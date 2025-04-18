using System;
using System.Collections.Generic;
using VibeTrader.Application.Commands.CreateAlert;
using VibeTrader.Application.Commands.UpdateAlert;
using VibeTrader.Application.DTOs;

namespace VibeTrader.Client.State
{
    // Load Alerts
    public class LoadAlertsAction { 
        public bool ActiveOnly { get; }
        
        public LoadAlertsAction(bool activeOnly = false)
        {
            ActiveOnly = activeOnly;
        }
    }
    
    public class LoadAlertsSuccessAction
    {
        public List<AlertDto> Alerts { get; }
        
        public LoadAlertsSuccessAction(List<AlertDto> alerts)
        {
            Alerts = alerts;
        }
    }
    
    public class LoadAlertsFailureAction
    {
        public string ErrorMessage { get; }
        
        public LoadAlertsFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
    
    // Get Alert by Id
    public class GetAlertAction
    {
        public Guid AlertId { get; }
        
        public GetAlertAction(Guid alertId)
        {
            AlertId = alertId;
        }
    }
    
    public class GetAlertSuccessAction
    {
        public AlertDto Alert { get; }
        
        public GetAlertSuccessAction(AlertDto alert)
        {
            Alert = alert;
        }
    }
    
    public class GetAlertFailureAction
    {
        public string ErrorMessage { get; }
        
        public GetAlertFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
    
    // Create Alert
    public class CreateAlertAction
    {
        public CreateAlertCommand Command { get; }
        
        public CreateAlertAction(CreateAlertCommand command)
        {
            Command = command;
        }
    }
    
    public class CreateAlertSuccessAction
    {
        public AlertDto Alert { get; }
        
        public CreateAlertSuccessAction(AlertDto alert)
        {
            Alert = alert;
        }
    }
    
    public class CreateAlertFailureAction
    {
        public string ErrorMessage { get; }
        
        public CreateAlertFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
    
    // Update Alert
    public class UpdateAlertAction
    {
        public UpdateAlertCommand Command { get; }
        
        public UpdateAlertAction(UpdateAlertCommand command)
        {
            Command = command;
        }
    }
    
    public class UpdateAlertSuccessAction
    {
        public AlertDto Alert { get; }
        
        public UpdateAlertSuccessAction(AlertDto alert)
        {
            Alert = alert;
        }
    }
    
    public class UpdateAlertFailureAction
    {
        public string ErrorMessage { get; }
        
        public UpdateAlertFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
    
    // Delete Alert
    public class DeleteAlertAction
    {
        public Guid AlertId { get; }
        
        public DeleteAlertAction(Guid alertId)
        {
            AlertId = alertId;
        }
    }
    
    public class DeleteAlertSuccessAction
    {
        public Guid AlertId { get; }
        
        public DeleteAlertSuccessAction(Guid alertId)
        {
            AlertId = alertId;
        }
    }
    
    public class DeleteAlertFailureAction
    {
        public string ErrorMessage { get; }
        
        public DeleteAlertFailureAction(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
    
    // Clear Alert selection/error
    public class ClearCurrentAlertAction { }
    public class ClearErrorAction { }
}