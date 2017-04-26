using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Choop.Compiler
{
    /// <summary>
    /// Handles parsing errors in the <see cref="ChoopParser"/>. 
    /// </summary>
    class ChoopParserErrorListener : BaseErrorListener
    {
        #region Fields
        private ICollection<CompilerError> ErrorCollection;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopParserErrorListener"/> class. 
        /// </summary>
        /// <param name="errorCollection">The collection to store add compiler errors to.</param>
        public ChoopParserErrorListener(ICollection<CompilerError> errorCollection)
        {
            ErrorCollection = errorCollection;
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
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);

            // Create message
            // Default to system message
            string message = msg;
            IToken symbol = offendingSymbol;

            // Get parser and dictionary
            ChoopParser parser = recognizer as ChoopParser;

            if (parser != null)
            {
                // Get lexer rules
                IVocabulary vocabulary = ChoopLexer.DefaultVocabulary;

                // Get expected tokens
                List<int> expectedTokenTypes = parser.GetExpectedTokensWithinCurrentRule().ToIntegerList();
                string[] expectedTokens = expectedTokenTypes.Select(t => vocabulary.GetDisplayName(t)).ToArray();

                if (expectedTokens.Length == 1)
                {
                    // Simple case - only 1 expected token
                    message = expectedTokens[0] + " expected";
                }
                else
                {
                    // Multiple potential expected tokens
                    // Use analysis to work out best message

                    if (e == null)
                    {
                        // No exception - generic error

                        // Assume extraneous input
                        message = string.Concat("Expected {", string.Join(", ", expectedTokens), "} but found '", offendingSymbol.Text, "'");
                    }
                    else
                    {
                        // Exception occured

                        if (e is NoViableAltException)
                        {
                            // Could not match input to token
                            symbol = ((NoViableAltException)e).StartToken;
                            message = string.Concat("Expected {", string.Join(", ", expectedTokens), "} but found '", symbol.Text, "'");
                        }
                    }
                }
            }

            // Add error to collection
            ErrorCollection.Add(new CompilerError(message, line, charPositionInLine, symbol.StartIndex, symbol.StopIndex, symbol.Text));
        }
        #endregion
    }
}
