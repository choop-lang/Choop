using Antlr4.Runtime;

namespace Choop.Compiler
{
    /// <summary>
    /// Represents a custom syntax error.
    /// </summary>
    class CustomRecognitionException : RecognitionException
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="CustomRecognitionException"/> class. 
        /// </summary>
        /// <param name="message">The message of the exception.</param>
        /// <param name="recognizer">The parser.</param>
        /// <param name="input">The input stream.</param>
        /// <param name="ctx">The current rule.</param>
        public CustomRecognitionException(string message, IRecognizer recognizer, IIntStream input, ParserRuleContext ctx) : base(message, recognizer, input, ctx)
        {
            
        }
        #endregion
    }
}
