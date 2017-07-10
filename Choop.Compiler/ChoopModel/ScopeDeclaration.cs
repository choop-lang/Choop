using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

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

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}