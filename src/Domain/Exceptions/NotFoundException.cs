using System;

namespace VibeTrader.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified entity name and key
        /// </summary>
        /// <param name="name">The name of the entity</param>
        /// <param name="key">The key of the entity</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}