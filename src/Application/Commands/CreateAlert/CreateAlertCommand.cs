using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.DTOs;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Entities;
using VibeTrader.Domain.Enums;

namespace VibeTrader.Application.Commands.CreateAlert
{
    /// <summary>
    /// Command to create a new stock alert
    /// </summary>
    public class CreateAlertCommand : IRequest<AlertDto>
    {
        /// <summary>
        /// Stock symbol (e.g. MSFT, AAPL)
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Target price that will trigger the alert
        /// </summary>
        public decimal TargetPrice { get; set; }
        
        /// <summary>
        /// Type of alert (above or below target price)
        /// </summary>
        public AlertType Type { get; set; }
        
        /// <summary>
        /// User who created the alert
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Optional notes about the alert
        /// </summary>
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Handler for the CreateAlertCommand
    /// </summary>
    public class CreateAlertCommandHandler : IRequestHandler<CreateAlertCommand, AlertDto>
    {
        private readonly IAlertRepository _alertRepository;

        public CreateAlertCommandHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<AlertDto> Handle(CreateAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = new Alert(
                request.Symbol,
                request.TargetPrice,
                request.Type,
                request.CreatedBy,
                request.Notes);

            await _alertRepository.AddAsync(alert, cancellationToken);
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