using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.DTOs;
using VibeTrader.Application.Interfaces;

namespace VibeTrader.Application.Queries.GetAlerts
{
    /// <summary>
    /// Query to get all alerts with optional filter for active alerts only
    /// </summary>
    public class GetAlertsQuery : IRequest<List<AlertDto>>
    {
        /// <summary>
        /// When true, returns only active alerts
        /// </summary>
        public bool ActiveOnly { get; set; }
    }

    /// <summary>
    /// Handler for the GetAlertsQuery
    /// </summary>
    public class GetAlertsQueryHandler : IRequestHandler<GetAlertsQuery, List<AlertDto>>
    {
        private readonly IAlertRepository _alertRepository;

        public GetAlertsQueryHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<List<AlertDto>> Handle(GetAlertsQuery request, CancellationToken cancellationToken)
        {
            var alerts = await _alertRepository.GetAllAsync(cancellationToken);
            
            if (request.ActiveOnly)
                alerts = alerts.Where(a => a.IsActive).ToList();
            
            // Map to DTOs
            return alerts.Select(alert => new AlertDto
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
            }).ToList();
        }
    }
}