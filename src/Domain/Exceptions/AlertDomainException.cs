using System;

namespace VibeTrader.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a domain operation on an Alert entity fails
    /// </summary>
    public class AlertDomainException : Exception
    {
        public AlertDomainException(string message) : base(message)
        {
        }

        public AlertDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}