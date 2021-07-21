using System;

namespace BikeDistributor.Domain.SeedWork
{
    /// <summary>
    /// Business rules validation exception used in <see cref="Domain"/>
    /// </summary>
    public class BusinessRuleValidationException : Exception
    {
        /// <inheritdoc cref="BusinessRuleValidationException"/>
        /// <param name="message"></param>
        public BusinessRuleValidationException(string message) : base(message) { }
    }
}
