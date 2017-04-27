using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Choop.Compiler
{
    class ChoopListener : ChoopBaseListener
    {
        #region Fields
        protected ChoopParser Parser;

        protected int Depth = 0;
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

        string GetIndent(int depth)
        {
            StringBuilder sb = new StringBuilder(depth);

            for (int i = 0; i < depth; i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append(' ');
                }
                else
                {
                    sb.Append('|');
                }
            }

            return sb.ToString();
        }
        #endregion
    }
}
