using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Application.Interfaces
{
    /// <summary>
    /// Repository interface for alert entity operations
    /// </summary>
    public interface IAlertRepository
    {
        /// <summary>
        /// Gets all alerts
        /// </summary>
        Task<List<Alert>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// Gets an alert by its unique identifier
        /// </summary>
        Task<Alert?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Adds a new alert
        /// </summary>
        Task AddAsync(Alert alert, CancellationToken cancellationToken);
        
        /// <summary>
        /// Updates an existing alert
        /// </summary>
        Task UpdateAsync(Alert alert, CancellationToken cancellationToken);
        
        /// <summary>
        /// Deletes an alert
        /// </summary>
        Task DeleteAsync(Alert alert, CancellationToken cancellationToken);
        
        /// <summary>
        /// Saves changes to the database
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}