using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Choop.Compiler
{
    class ChoopErrorListener : BaseErrorListener
    {
        #region Methods
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);

            IList<string> stack = ((Parser)recognizer).GetRuleInvocationStack();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Syntax error:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Stack: " + string.Join(" ", stack.Reverse()));
            Console.WriteLine($"Line: {line}:{charPositionInLine} at {offendingSymbol.Text}: {msg}");
        }
        #endregion
    }
}
