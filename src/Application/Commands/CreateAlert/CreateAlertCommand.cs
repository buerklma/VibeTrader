using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.DTOs;
using VibeTrader.Domain.Entities;
using VibeTrader.Domain.Enums;
using VibeTrader.Application.Interfaces;

namespace VibeTrader.Application.Commands.CreateAlert
{
    /// <summary>
    /// Command to create a new stock price alert
    /// </summary>
    public class CreateAlertCommand : IRequest<AlertDto>
    {
        /// <summary>
        /// Stock symbol for the alert
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        
        /// <summary>
        /// Target price that will trigger the alert
        /// </summary>
        public decimal TargetPrice { get; set; }
        
        /// <summary>
        /// Type of the alert (Above or Below target price)
        /// </summary>
        public AlertType Type { get; set; }
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
            var alert = new Alert(request.Symbol, request.TargetPrice, request.Type);
            
            await _alertRepository.AddAsync(alert, cancellationToken);
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