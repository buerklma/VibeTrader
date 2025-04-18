using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VibeTrader.Application.Commands.CreateAlert;
using VibeTrader.Application.Commands.DeleteAlert;
using VibeTrader.Application.Commands.UpdateAlert;
using VibeTrader.Application.DTOs;
using VibeTrader.Application.Queries.GetAlertById;
using VibeTrader.Application.Queries.GetAlerts;

namespace VibeTrader.API.Controllers
{
    /// <summary>
    /// Controller for managing stock price alerts
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlertsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all stock alerts
        /// </summary>
        /// <param name="activeOnly">Filter to only show active alerts</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of alerts</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AlertDto>>> GetAlerts([FromQuery] bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAlertsQuery { ActiveOnly = activeOnly }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific alert by ID
        /// </summary>
        /// <param name="id">Alert ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Alert details</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlertDto>> GetAlertById(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAlertByIdQuery { Id = id }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new stock alert
        /// </summary>
        /// <param name="command">Create alert command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created alert details</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AlertDto>> CreateAlert(CreateAlertCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetAlertById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing stock alert
        /// </summary>
        /// <param name="id">Alert ID</param>
        /// <param name="command">Update alert command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated alert details</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AlertDto>> UpdateAlert(Guid id, UpdateAlertCommand command, CancellationToken cancellationToken = default)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch between route and body");

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a stock alert
        /// </summary>
        /// <param name="id">Alert ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success indicator</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAlert(Guid id, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new DeleteAlertCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}