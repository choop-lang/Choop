using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;

namespace Choop.Compiler
{
    /// <summary>
    /// Handles lexer errors in the <see cref="ChoopParser"/>. 
    /// </summary>
    class ChoopTokenErrorListener : IAntlrErrorListener<int>
    {
        #region Fields
        private ICollection<CompilerError> ErrorCollection;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopTokenErrorListener"/> class. 
        /// </summary>
        /// <param name="errorCollection">The collection to store add compiler errors to.</param>
        public ChoopTokenErrorListener(ICollection<CompilerError> errorCollection)
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
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            // Default to system message
            string token = "";
            string message = msg;
            int startIndex = -1;
            int endIndex = -1;
            ErrorType errorType = ErrorType.Generic;

            ChoopLexer lexer = recognizer as ChoopLexer;
            if (lexer != null)
            {
                startIndex = lexer._tokenStartCharIndex;
                endIndex = lexer._input.Index;
                token = lexer.GetErrorDisplay(lexer._input.GetText(Interval.Of(startIndex, endIndex)));
                message = "Could not recognise token '" + token + "'";
                errorType = ErrorType.TokenRecognitionError;
            }

            // Add error to collection
            ErrorCollection.Add(new CompilerError(message, line, charPositionInLine, startIndex, endIndex, token, errorType));
        }
        #endregion
    }
}
