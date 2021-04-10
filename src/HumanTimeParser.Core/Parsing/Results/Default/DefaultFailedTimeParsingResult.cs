using System;

namespace HumanTimeParser.Core.Parsing
{
    public sealed class DefaultFailedTimeParsingResult : IFailedTimeParsingResult
    {
        public string ErrorReason { get; }
        public Exception Exception { get; }

        public DefaultFailedTimeParsingResult(string reason)
        {
            ErrorReason = reason;
        }

        public DefaultFailedTimeParsingResult(Exception ex)
        {
            Exception = ex;
            ErrorReason = ex.Message;
        }

        public DefaultFailedTimeParsingResult(string reason, Exception ex)
        {
            ErrorReason = reason;
            Exception = ex;
        }

        public override string ToString()
            => ErrorReason;
    }
}