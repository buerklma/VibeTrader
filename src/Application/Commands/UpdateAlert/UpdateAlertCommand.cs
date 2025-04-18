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
    /// Command to update an existing stock alert
    /// </summary>
    public class UpdateAlertCommand : IRequest<AlertDto>
    {
        /// <summary>
        /// Unique identifier of the alert to update
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// New stock symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// New target price
        /// </summary>
        public decimal TargetPrice { get; set; }
        
        /// <summary>
        /// New alert type
        /// </summary>
        public AlertType Type { get; set; }
        
        /// <summary>
        /// New notes (optional)
        /// </summary>
        public string? Notes { get; set; }
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
            {
                throw new NotFoundException("Alert", request.Id);
            }

            alert.Update(
                request.Symbol,
                request.TargetPrice,
                request.Type,
                request.Notes);

            await _alertRepository.UpdateAsync(alert, cancellationToken);
            await _alertRepository.SaveChangesAsync(cancellationToken);

            return new AlertDto
            {
                Id = alert.Id,
                Symbol = alert.Symbol,
                TargetPrice = alert.TargetPrice,
                Type = alert.Type,
                CreatedOn = alert.CreatedOn,
                TriggeredOn = alert.TriggeredOn,
                IsActive = alert.IsActive,
                CreatedBy = alert.CreatedBy,
                Notes = alert.Notes
            };
        }
    }
}