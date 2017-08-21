namespace Choop.Compiler
{
    /// <summary>
    /// Specifies the possible compiler error types.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Indicates an unspecified compiler error.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Indicates a generic error that occured in the lexer.
        /// </summary>
        GenericLexerError,

        /// <summary>
        /// Indicates a generic error that occured in the parser.
        /// </summary>
        GenericParserError,

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
        NoViableAlternative,

        /// <summary>
        /// Indiciates that a declaration which shared its name with another declaration was detected.
        /// </summary>
        DuplicateDeclaration,

        /// <summary>
        /// Indicates that a module had already been imported into the sprite.
        /// </summary>
        ModuleAlreadyImported,

        /// <summary>
        /// Indicates that an argument was invalid.
        /// </summary>
        InvalidArgument,

        /// <summary>
        /// Indicates that the type was not compatible with the target type.
        /// </summary>
        TypeMismatch,

        /// <summary>
        /// Indicates that an assigment is targetting a read-only value.
        /// </summary>
        ValueIsReadonly,

        /// <summary>
        /// A declaration of method is being used in an invalid way.
        /// </summary>
        ImproperUsage,

        /// <summary>
        /// Indicates that a name a declaration with a specified name could not be found.
        /// </summary>
        NotDefined,

        /// <summary>
        /// Indicates that a file referenced was not found.
        /// </summary>
        FileNotFound,

        /// <summary>
        /// Indicates that a loaded file had an invalid format that could not be understood.
        /// </summary>
        InvalidFormat
    }
}