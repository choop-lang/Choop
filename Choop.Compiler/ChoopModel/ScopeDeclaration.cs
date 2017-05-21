using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

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
        /// Gets the scope of the scope being declared.
        /// </summary>
        public Scope Scope { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ScopeDeclaration"/> class.
        /// </summary>
        /// <param name="parentScope">The parent scope of the declaration.</param>
        public ScopeDeclaration(Scope parentScope)
        {
            Scope = new Scope(parentScope);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
