using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration of a block that is ran dependent on a condition.
    /// </summary>
    public class ConditionalBlock : IHasBody, ICompilable<Block[]>
    {
        #region Properties

        /// <summary>
        /// Gets the condition to use when deiciding whether to run the block.
        /// </summary>
        public Collection<IExpression> Conditions { get; } = new Collection<IExpression>();

        /// <summary>
        /// Gets whether the block is the default case. (No parameter)
        /// </summary>
        public bool IsDefault => Conditions.Count == 0;

        /// <summary>
        /// Gets the collection of statements within the block.
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
        /// Creates a new instance of the <see cref="ConditionalBlock"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ConditionalBlock(string fileName, IToken errorToken)
        {
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Translates the Choop grammar structure.
        /// </summary>
        /// <param name="context">The context of the translation.</param>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            Scope newScope = new Scope(context.CurrentScope);
            TranslationContext newContext = new TranslationContext(newScope, context.ErrorList);

            List<Block> blocks = new List<Block>();

            foreach (IStatement statement in Statements)
                blocks.AddRange(statement.Translate(newContext));

            return blocks.ToArray();
        }

        #endregion
    }
}