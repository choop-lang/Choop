using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.ObjectModel;

namespace Choop.Compiler
{
    /// <summary>
    /// Handles lexer errors in the <see cref="ChoopParser"/>. 
    /// </summary>
    internal class ChoopTokenErrorListener : IAntlrErrorListener<int>
    {
        #region Fields

        /// <summary>
        /// The collection of compiler errors to add to.
        /// </summary>
        private readonly Collection<CompilerError> _errorCollection;

        /// <summary>
        /// The name of the file currently being compiled.
        /// </summary>
        private readonly string _fileName;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ChoopTokenErrorListener"/> class. 
        /// </summary>
        /// <param name="errorCollection">The collection to store add compiler errors to.</param>
        /// <param name="fileName">The name of the file currently being compiled.</param>
        public ChoopTokenErrorListener(Collection<CompilerError> errorCollection, string fileName)
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
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            // Default to system message
            string token = "";
            string message = msg;
            int startIndex = -1;
            int endIndex = -1;
            ErrorType errorType = ErrorType.GenericLexerError;

            if (recognizer is ChoopLexer lexer)
            {
                startIndex = lexer._tokenStartCharIndex;
                endIndex = lexer._input.Index;
                token = lexer.GetErrorDisplay(lexer._input.GetText(Interval.Of(startIndex, endIndex)));
                message = "Could not recognise token '" + token + "'";
                errorType = ErrorType.TokenRecognitionError;
            }

            // Add error to collection
            _errorCollection.Add(new CompilerError(message, line, charPositionInLine, startIndex, endIndex, token,
                _fileName, errorType));
        }

        #endregion
    }
}