using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.DTOs;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Exceptions;

namespace VibeTrader.Application.Queries.GetAlertById
{
    /// <summary>
    /// Query to get a specific alert by its unique identifier
    /// </summary>
    public class GetAlertByIdQuery : IRequest<AlertDto>
    {
        /// <summary>
        /// ID of the alert to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Handler for the GetAlertByIdQuery
    /// </summary>
    public class GetAlertByIdQueryHandler : IRequestHandler<GetAlertByIdQuery, AlertDto>
    {
        private readonly IAlertRepository _alertRepository;

        public GetAlertByIdQueryHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<AlertDto> Handle(GetAlertByIdQuery request, CancellationToken cancellationToken)
        {
            var alert = await _alertRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (alert == null)
            {
                throw new NotFoundException("Alert", request.Id);
            }

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