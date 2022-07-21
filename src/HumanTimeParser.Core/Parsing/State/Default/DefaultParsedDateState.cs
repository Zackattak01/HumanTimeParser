using System;

namespace HumanTimeParser.Core.Parsing.State
{
    /// <inheritdoc/>
    public class DefaultParsedDateState : IParsedDateState
    {
        /// <inheritdoc/>
        public bool ParsedQualifiedDay { get; set; }

        /// <inheritdoc/>
        public int LastParsedTokenPosition { get; set; }

        /// <inheritdoc/>
        public int? Day { get; set; }

        /// <inheritdoc/>
        public int? Month { get; set; }

        /// <inheritdoc/>
        public int? Year { get; set; }

        /// <inheritdoc/>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultParsedDateState"/> class.
        /// </summary>
        public DefaultParsedDateState()
        {
            LastParsedTokenPosition = -1;
        }

        /// <inheritdoc/>
        public bool CanConstructValidDate()
            => Date is not null || 
               (LastParsedTokenPosition != -1 && Day is not null && (ParsedQualifiedDay || Month is not null || Year is not null));
    }
}