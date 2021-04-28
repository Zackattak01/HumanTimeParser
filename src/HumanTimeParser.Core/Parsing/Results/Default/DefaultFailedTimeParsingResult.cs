using System;

namespace HumanTimeParser.Core.Parsing
{
    /// <summary>
    /// Represents a default result for a failed time parse
    /// </summary>
    public sealed class DefaultFailedTimeParsingResult : IFailedTimeParsingResult
    {
        /// <inheritdoc/> 
        public string ErrorReason { get; }
        
        /// <inheritdoc/> 
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFailedTimeParsingResult"/> class.
        /// </summary>
        /// <param name="reason">Reason for the failure.</param>
        public DefaultFailedTimeParsingResult(string reason)
        {
            ErrorReason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFailedTimeParsingResult"/> class.
        /// </summary>
        /// <param name="ex">Exception responsible for the failure.  The <see cref="ErrorReason"/> will also be set based on the exception</param>
        public DefaultFailedTimeParsingResult(Exception ex)
        {
            Exception = ex;
            ErrorReason = ex.Message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFailedTimeParsingResult"/> class.
        /// </summary>
        /// <param name="reason">Reason for the failure.</param>
        /// <param name="ex">Exception responsible for the failure.</param>
        public DefaultFailedTimeParsingResult(string reason, Exception ex)
        {
            ErrorReason = reason;
            Exception = ex;
        }

        /// <inheritdoc/>
        public override string ToString()
            => ErrorReason;
    }
}