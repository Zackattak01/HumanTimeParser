using System;
using HumanTimeParser.Core.Sectioning;
using HumanTimeParser.Core.Tokenization.Tokens;

namespace HumanTimeParser.Core.Tokenization
{
    /// <inheritdoc/>
    public abstract class TokenizerBase : ITokenizer
    {
        /// <inheritdoc/>
        public IToken CurrentToken { get; private set; }
        protected ISectionizer Sectionizer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizerBase"/> class.
        /// </summary>
        /// <param name="sectionizer">The sectionizer to use.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TokenizerBase(ISectionizer sectionizer)
        {
            Sectionizer = sectionizer ?? throw new ArgumentNullException(nameof(sectionizer));
        }

        /// <inheritdoc/>
        public virtual IToken NextToken()
        {
            var token = TokenizeSection(Sectionizer.NextSection());
            if (token is null)
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
        
        protected abstract IToken TokenizeSection(Section section);
    }
}