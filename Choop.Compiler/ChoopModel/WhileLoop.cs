using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a while loop.
    /// </summary>
    public class WhileLoop : IHasBody, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the expression for the stopping condition.
        /// </summary>
        public IExpression Condition { get; }

        /// <summary>
        /// Gets the collection of statements within the loop.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="WhileLoop"/> class.
        /// </summary>
        /// <param name="condition">The stopping condition for the loop.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public WhileLoop(IExpression condition, string fileName, IToken errorToken)
        {
            Condition = condition;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            List<Block> blocks = new List<Block>();
            TranslationContext newContext = new TranslationContext(new Scope(context.CurrentScope), context);

            foreach (IStatement statement in Statements)
                blocks.AddRange(statement.Translate(newContext));

            return new[] { new Block(BlockSpecs.While, Condition.Balance().Translate(newContext), blocks.ToArray()) };
        }

        #endregion
    }
}