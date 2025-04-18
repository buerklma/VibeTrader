using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Infrastructure.Data
{
    /// <summary>
    /// Repository implementation for Alert entities
    /// </summary>
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AlertRepository> _logger;

        public AlertRepository(ApplicationDbContext dbContext, ILogger<AlertRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<List<Alert>> GetAllAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting all alerts");
            return await _dbContext.Alerts.AsNoTracking().ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting alert by ID: {AlertId}", id);
            return await _dbContext.Alerts.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddAsync(Alert alert, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Adding new alert for symbol {Symbol}", alert.Symbol);
            await _dbContext.Alerts.AddAsync(alert, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Alert alert, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Updating alert with ID: {AlertId}", alert.Id);
            _dbContext.Alerts.Update(alert);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Alert alert, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Deleting alert with ID: {AlertId}", alert.Id);
            _dbContext.Alerts.Remove(alert);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Saving changes to database");
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}