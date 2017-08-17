using System;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an if-else statement.
    /// </summary>
    public class IfStmt : IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the collection of code blocks within the if-else statement.
        /// </summary>
        /// <remarks>The code blocks should be in order, with the primary case first and the default case (if specified) last.</remarks>
        public Collection<ConditionalBlock> Blocks { get; } = new Collection<ConditionalBlock>();

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
        /// Creates a new instance of the <see cref="IfStmt"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public IfStmt(string fileName, IToken errorToken)
        {
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
            return BuildIfElse(context, 0);
        }

        /// <summary>
        /// Recursively builds an if-else statement.
        /// </summary>
        /// <param name="context">The context of the translation.</param>
        /// <param name="element">The current block.</param>
        /// <returns>The translated if-else statement.</returns>
        private Block[] BuildIfElse(TranslationContext context, int element)
        {
            if (element == Blocks.Count)
                throw new IndexOutOfRangeException("Element is out of range");

            if (element + 1 != Blocks.Count)
                return new BlockBuilder(BlockSpecs.IfThenElse, context)
                    .AddParam(Blocks[element].Conditions[0].Expression)
                    .AddParam(Blocks[element].Translate(context))
                    .AddParam(BuildIfElse(context, element + 1))
                    .Create().ToArray();

            if (Blocks[element].IsDefault)
                return Blocks[element].Translate(context);

            return new BlockBuilder(BlockSpecs.IfThen, context)
                .AddParam(Blocks[element].Conditions[0].Expression)
                .AddParam(Blocks[element].Translate(context))
                .Create().ToArray();
        }

        #endregion
    }
}