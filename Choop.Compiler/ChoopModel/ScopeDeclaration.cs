using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration of a nested scope.
    /// </summary>
    public class ScopeDeclaration : IHasBody, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the collection of statements within the scope.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();

        /// <summary>
        /// Gets whether the scope is unsafe.
        /// </summary>
        public bool Unsafe { get; }

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
        /// Creates a new instance of the <see cref="ScopeDeclaration"/> class.
        /// </summary>
        /// <param name="unsafe">Whether the scope is unsafe.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ScopeDeclaration(bool @unsafe, string fileName, IToken errorToken)
        {
            Unsafe = @unsafe;
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
            TranslationContext newContext = new TranslationContext(new Scope(context.CurrentScope, Unsafe), context);

            // Translate
            List<Block> translated = new List<Block>();
            foreach (IStatement statement in Statements)
                translated.AddRange(statement.Translate(newContext));

            return translated.ToArray();
        }

        #endregion
    }
}