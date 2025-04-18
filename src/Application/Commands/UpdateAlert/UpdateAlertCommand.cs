using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.DTOs;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Enums;
using VibeTrader.Domain.Exceptions;

namespace VibeTrader.Application.Commands.UpdateAlert
{
    /// <summary>
    /// Command to update an existing stock price alert
    /// </summary>
    public class UpdateAlertCommand : IRequest<AlertDto>
    {
        /// <summary>
        /// Unique identifier of the alert to update
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// New stock symbol for the alert
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// New target price that will trigger the alert
        /// </summary>
        public decimal TargetPrice { get; set; }
        
        /// <summary>
        /// New type of the alert (Above or Below target price)
        /// </summary>
        public AlertType Type { get; set; }
    }

    /// <summary>
    /// Handler for the UpdateAlertCommand
    /// </summary>
    public class UpdateAlertCommandHandler : IRequestHandler<UpdateAlertCommand, AlertDto>
    {
        private readonly IAlertRepository _alertRepository;

        public UpdateAlertCommandHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<AlertDto> Handle(UpdateAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = await _alertRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (alert == null)
                throw new NotFoundException($"Alert with ID {request.Id} not found");
            
            alert.Update(request.Symbol, request.TargetPrice, request.Type);
            
            await _alertRepository.UpdateAsync(alert, cancellationToken);
            await _alertRepository.SaveChangesAsync(cancellationToken);
            
            // Map to DTO
            var alertDto = new AlertDto
            {
                Id = alert.Id,
                Symbol = alert.Symbol,
                TargetPrice = alert.TargetPrice,
                Type = alert.Type,
                CreatedOn = alert.CreatedOn,
                TriggeredOn = alert.TriggeredOn,
                IsActive = alert.IsActive
            };
            
            return alertDto;
        }
    }
}