using System;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    /// <inheritdoc/>
    public abstract class TokenizerBase : ITokenizer
    {
        private ISectionizer _sectionizer;

        private IToken _peekedToken;

        /// <summary>
        /// A reusable <see cref="EOFToken"/> to avoid initializing one each parse
        /// </summary>
        protected static readonly EOFToken EOFToken = new EOFToken(); 

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
            CurrentToken = token;
            return token;
        }

        /// <inheritdoc/>
        public virtual IToken PeekNextToken()
        {
            var token = TokenizeSection(Sectionizer.PeekNextSection());

            _peekedToken = token;
            return token;
        }

        /// <inheritdoc/>
        public void SkipToken()
        {
            Sectionizer.AdvanceSection();
            
            if(_peekedToken is not null)
                CurrentToken = _peekedToken;
        }

        /// <summary>
        /// Tokenizes the given section.
        /// </summary>
        /// <param name="section">The section to be tokenized</param>
        /// <returns>A token, <see cref="UnknownToken"/> if no token was found</returns>
        protected abstract IToken TokenizeSection(Section section);
    }
}