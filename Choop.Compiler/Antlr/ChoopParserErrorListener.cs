using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;

namespace Choop.Compiler.Antlr
{
    /// <summary>
    /// Handles parsing errors in the <see cref="ChoopParser"/>.
    /// </summary>
    internal class ChoopParserErrorListener : BaseErrorListener
    {
        #region Fields

        /// <summary>
        /// The collection of compiler errors to add to.
        /// </summary>
        private readonly Collection<CompilerError> _errorCollection;

        /// <summary>
        /// The name of the file being parsed.
        /// </summary>
        private readonly string _fileName;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ChoopParserErrorListener"/> class. 
        /// </summary>
        /// <param name="errorCollection">The collection to store add compiler errors to.</param>
        /// <param name="fileName">The name of the file currently being compiled.</param>
        public ChoopParserErrorListener(Collection<CompilerError> errorCollection, string fileName)
        {
            _errorCollection = errorCollection;
            _fileName = fileName;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifies that there has been a syntax error.
        /// </summary>
        /// <param name="recognizer">The state of the parser when the syntax error was found.</param>
        /// <param name="offendingSymbol">The offending token in the input stream.</param>
        /// <param name="line">The line number of the syntax error.</param>
        /// <param name="charPositionInLine">The column number of the syntax error.</param>
        /// <param name="msg">The message to emit.</param>
        /// <param name="e">The base exception generated that lead to the reporting of an error.</param>
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line,
            int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);

            // Create error params
            string message = msg;
            IToken symbol = offendingSymbol;
            ErrorType errorType = ErrorType.GenericParserError;

            // Get parser and dictionary
            ChoopParser parser = recognizer as ChoopParser;

            if (parser != null && parser.State > -1)
            {
                // Parser is a ChoopParser and an automatic parser error was thrown

                // Get lexer rules
                IVocabulary vocabulary = ChoopLexer.DefaultVocabulary;

                // Get expected tokens
                List<int> expectedTokenTypes = parser.GetExpectedTokensWithinCurrentRule().ToIntegerList();
                string[] expectedTokens = expectedTokenTypes.Select(t => vocabulary.GetDisplayName(t)).ToArray();

                if (expectedTokens.Length == 1)
                {
                    // Simple case - only 1 expected token
                    message = expectedTokens[0] + " expected";
                    errorType = ErrorType.TokenMissing;
                }
                else
                {
                    // Multiple potential expected tokens
                    // Use analysis to work out best message

                    if (e == null)
                    {
                        // No exception - generic error

                        // Assume extraneous input
                        message = string.Concat("Expected {", string.Join(", ", expectedTokens), "} but found '",
                            offendingSymbol.Text, "'");
                        errorType = ErrorType.ExtraneousToken;
                    }
                    else
                    {
                        // Exception occured

                        NoViableAltException exception = e as NoViableAltException;
                        if (exception != null)
                        {
                            // Could not match input to token
                            symbol = exception.StartToken;
                            message = string.Concat("Expected {", string.Join(", ", expectedTokens), "} but found '",
                                symbol.Text, "'");
                            errorType = ErrorType.NoViableAlternative;
                        }
                    }
                }
            }

            // Add error to collection
            _errorCollection.Add(
                new CompilerError(
                    message,
                    errorType,
                    line,
                    charPositionInLine,
                    symbol.StartIndex,
                    symbol.StopIndex,
                    symbol.Text,
                    _fileName
                )
            );
        }

        #endregion
    }
}