using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VibeTrader.Application.Interfaces;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Implementation of the AlertRepository interface for alert entity operations
    /// </summary>
    public class AlertRepository : IAlertRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertRepository"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        public AlertRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public async Task<List<Alert>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Alerts
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Alerts
                .FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddAsync(Alert alert, CancellationToken cancellationToken)
        {
            await _context.Alerts.AddAsync(alert, cancellationToken);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Alert alert, CancellationToken cancellationToken)
        {
            _context.Entry(alert).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteAsync(Alert alert, CancellationToken cancellationToken)
        {
            _context.Alerts.Remove(alert);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}