using System;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Choop.Compiler
{
    internal class ChoopListener : ChoopBaseListener
    {
        #region Fields
        protected readonly ChoopParser Parser;

        protected int Depth;
        #endregion
        #region Constructor
        public ChoopListener(ChoopParser parser)
        {
            Parser = parser;
        }
        #endregion
        #region Methods
        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);

            Depth = context.Depth();
            Console.WriteLine(GetIndent(Depth) + Parser.RuleNames[context.RuleIndex]);
            Depth++;
        }

        public override void ExitEveryRule([NotNull] ParserRuleContext context)
        {
            base.ExitEveryRule(context);

            Depth--;
        }

        public override void VisitTerminal([NotNull] ITerminalNode node)
        {
            base.VisitTerminal(node);

            Console.WriteLine(GetIndent(Depth) + "'" + node.GetText() + "'");
        }

        public override void VisitErrorNode([NotNull] IErrorNode node)
        {
            base.VisitErrorNode(node);

            Console.WriteLine(GetIndent(Depth) + "Error: " + node.GetText());
        }

        private static string GetIndent(int depth)
        {
            StringBuilder sb = new StringBuilder(depth);

            for (int i = 0; i < (depth - 1) * 2; i++)
            {
                sb.Append(i % 2 == 1 ? ' ' : '|');
            }

            return sb.ToString();
        }
        #endregion
    }
}
