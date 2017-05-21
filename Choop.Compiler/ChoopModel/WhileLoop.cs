using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

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
        /// Gets the scope of the loop.
        /// </summary>
        public Scope Scope { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="WhileLoop"/> class.
        /// </summary>
        /// <param name="condition">The stopping condition for the loop.</param>
        /// <param name="parentScope">The parent scope of the declaration.</param>
        public WhileLoop(IExpression condition, Scope parentScope)
        {
            Condition = condition;
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
