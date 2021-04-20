using System;
using HumanTimeParser.Core.Tokenization;

namespace HumanTimeParser.Core.Parsing
{
    /// <inheritdoc/>
    public abstract class TimeParserBase : ITimeParser
    {
        protected ITokenizer Tokenizer { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeParserBase"/> class.
        /// </summary>
        /// <param name="tokenizer">The tokenizer to be used for the parsing operation</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected TimeParserBase(ITokenizer tokenizer)
        {
            Tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
        }

        /// <inheritdoc/>
        public abstract ITimeParsingResult Parse(string input);
    }
}