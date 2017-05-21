using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a forever loop.
    /// </summary>
    public class ForeverLoop : IHasBody, IStatement
    {
        #region Properties
        /// <summary>
        /// Gets the collection of statements within the loop.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();

        /// <summary>
        /// Gets the scope of the loop.
        /// </summary>
        public Scope Scope { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ForeverLoop"/> class.
        /// </summary>
        /// <param name="parentScope">The parent scope of the declaration.</param>
        public ForeverLoop(Scope parentScope)
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
