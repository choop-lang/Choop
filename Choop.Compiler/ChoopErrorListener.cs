using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;

namespace Choop.Compiler
{
    /// <summary>
    /// Handles parsing errors in the <see cref="ChoopParser"/>. 
    /// </summary>
    class ChoopErrorListener : BaseErrorListener
    {
        #region Fields
        private ICollection<CompilerError> ErrorCollection;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopErrorListener"/> class. 
        /// </summary>
        /// <param name="errorCollection">The collection to store add compiler errors to.</param>
        public ChoopErrorListener(ICollection<CompilerError> errorCollection)
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

            // Get node hierarchy
            IList<string> stack = ((ChoopParser)recognizer).GetRuleInvocationStack();

            // Get compiler error message
            string message = $"Stack: [{string.Join(" ", stack.Reverse())}]\r\nLine {line}:{charPositionInLine} at {offendingSymbol.ToString()}: {msg}";

            // Add error
            ErrorCollection.Add(new CompilerError(message, line, charPositionInLine));
        }
        #endregion
    }
}
