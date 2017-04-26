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

            Console.WriteLine(new string(' ', context.Depth() - 1) + Parser.RuleNames[context.RuleIndex]);
        }
        
        public override void VisitErrorNode([NotNull] IErrorNode node)
        {
            base.VisitErrorNode(node);

            Console.WriteLine("Error: " + node.GetText());
        }
        #endregion
    }
}
