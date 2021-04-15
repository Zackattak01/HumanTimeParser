using System;

namespace HumanTimeParser.Core.Parsing
{
    /// <summary>
    /// Represents a basic failed time parsing result
    /// </summary>
    public interface IFailedTimeParsingResult : ITimeParsingResult
    {
        /// <summary>
        /// Gets the reason for the failure, null if none
        /// </summary>
        public string ErrorReason { get; }
        
        /// <summary>
        /// Gets the exception responsible for the failure, null if none
        /// </summary>
        public Exception Exception { get; }
    }
}