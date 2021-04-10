using System;
using System.Runtime.Serialization;

namespace HumanTimeParser.Core.Tokenization
{
    
    [Serializable]
    public class UnknownTokenException : Exception
    {
        public UnknownTokenException() { }
        public UnknownTokenException(string message) : base(message) { }
        public UnknownTokenException(string message, Exception inner) : base(message, inner) { }

        protected UnknownTokenException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}