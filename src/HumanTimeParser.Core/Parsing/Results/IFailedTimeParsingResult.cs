using System;

namespace HumanTimeParser.Core.Parsing
{
    public interface IFailedTimeParsingResult : ITimeParsingResult
    {
        public string ErrorReason { get; }
        
        public Exception Exception { get; }
    }
}