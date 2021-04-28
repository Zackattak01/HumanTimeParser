using System;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    /// <inheritdoc/>
    public abstract class TokenizerBase : ITokenizer
    {
        private ISectionizer _sectionizer;

        /// <inheritdoc/>
        public IToken CurrentToken { get; private set; }

        /// <inheritdoc/>
        public ISectionizer Sectionizer
        {
            get => _sectionizer;
            set => _sectionizer = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        public virtual IToken NextToken()
        {
            var token = TokenizeSection(Sectionizer.NextSection());
            if (token is UnknownToken)
                return NextToken();

            CurrentToken = token;
            return token;
        }

        /// <inheritdoc/>
        public virtual IToken PeekNextToken()
        {
            var token = TokenizeSection(Sectionizer.PeekNextSection());
            if (token is null)
                return NextToken();

            CurrentToken = token;
            return token;
        }

        /// <inheritdoc/>
        public void SkipToken() => Sectionizer.SkipSection();
        
        /// <summary>
        /// Tokenizes the given section.
        /// </summary>
        /// <param name="section">The section to be tokenized</param>
        /// <returns>A token, null if no token was found</returns>
        protected abstract IToken TokenizeSection(Section section);
    }
}