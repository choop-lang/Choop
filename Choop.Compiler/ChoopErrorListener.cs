using System;
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

            IList<string> stack = ((ChoopParser)recognizer).GetRuleInvocationStack();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Syntax error:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Stack: [{string.Join(" ", stack.Reverse())}]");
            Console.WriteLine($"Line: {line}:{charPositionInLine} at {offendingSymbol.ToString()}: {msg}");
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ChoopErrorListener"/> class. 
        /// </summary>
        public ChoopErrorListener()
        {

        }
        #endregion
    }
}
