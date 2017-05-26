using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

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
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="WhileLoop"/> class.
        /// </summary>
        /// <param name="condition">The stopping condition for the loop.</param>
        public WhileLoop(IExpression condition)
        {
            Condition = condition;
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
