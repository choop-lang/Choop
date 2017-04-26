namespace Choop.Compiler
{
    /// <summary>
    /// Specifies the possible compiler error types.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Indicates a generic compiler error.
        /// </summary>
        Generic,
        /// <summary>
        /// Indicates that the input could not be matched to any lexer rule.
        /// </summary>
        TokenRecognitionError,
        /// <summary>
        /// Indicates that a required token was missing.
        /// </summary>
        TokenMissing,
        /// <summary>
        /// Indicates that an unexpected token was encountered.
        /// </summary>
        ExtraneousToken,
        /// <summary>
        /// Indicates that, when choosing between multiple parser rules, no matching rule was found.
        /// </summary>
        NoViableAlternative
    }
}
