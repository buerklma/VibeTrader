using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Exceptions;

namespace VibeTrader.Application.Commands.DeleteAlert
{
    /// <summary>
    /// Command to delete an existing stock alert
    /// </summary>
    public class DeleteAlertCommand : IRequest<Unit>
    {
        /// <summary>
        /// Unique identifier of the alert to delete
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Handler for the DeleteAlertCommand
    /// </summary>
    public class DeleteAlertCommandHandler : IRequestHandler<DeleteAlertCommand, Unit>
    {
        private readonly IAlertRepository _alertRepository;

        public DeleteAlertCommandHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<Unit> Handle(DeleteAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = await _alertRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (alert == null)
            {
                throw new NotFoundException("Alert", request.Id);
            }

            await _alertRepository.DeleteAsync(alert, cancellationToken);
            await _alertRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}