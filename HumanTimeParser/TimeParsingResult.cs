using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace BullyBot
{
    public struct TimeParsingResult
    {
        public DateTime? DateTime { get; }

        public bool Success { get; }

        public string ErrorReason { get; }

        internal TimeParsingResult(bool success, DateTime? dt, string errorReason)
        {
            DateTime = dt;
            Success = success;
            ErrorReason = errorReason;
        }

        internal static TimeParsingResult FromNullableDateTime(DateTime? dt, string errorReason = null)
        {
            if (dt is null)
            {
                return new TimeParsingResult(false, null, errorReason ?? "No parseable time was found");
            }
            else
            {
                return new TimeParsingResult(true, dt, null);
            }
        }
    }
}