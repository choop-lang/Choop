using System;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;
using System.Collections.Generic;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a repeat loop.
    /// </summary>
    public class RepeatLoop : IHasBody, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets whether the repeat loop should be inlined.
        /// </summary>
        public bool Inline { get; }

        /// <summary>
        /// Gets the expression for the number of iterations to perform.
        /// </summary>
        public IExpression Iterations { get; }

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
        /// Creates a new instance of the <see cref="RepeatLoop"/> class.
        /// </summary>
        /// <param name="inline">Whether to inline the repeat loop.</param>
        /// <param name="iterations">The expression for the number of iterations to be run.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public RepeatLoop(bool inline, IExpression iterations, string fileName, IToken errorToken)
        {
            Inline = inline;
            Iterations = iterations;
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
            // Create new translation context
            TranslationContext newContext = new TranslationContext(new Scope(context.CurrentScope), context);

            List<Block> loopContents = new List<Block>();
            foreach (IStatement statement in Statements)
                loopContents.AddRange(statement.Translate(newContext));

            if (!Inline) return new[]
            {
                new Block(BlockSpecs.Repeat, Iterations, loopContents.ToArray())
            };

            // Inline loop (assume already validated)
            int repetitions = int.Parse((string)Iterations.Translate(context));

            List<Block> inlinedLoopContents = new List<Block>();

            for (int i = 0; i < repetitions; i++)
                inlinedLoopContents.AddRange(loopContents);

            return inlinedLoopContents.ToArray();
        }

        #endregion
    }
}