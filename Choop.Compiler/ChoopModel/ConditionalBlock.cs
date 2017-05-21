using System.Collections.ObjectModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration of a block that is ran dependent on a condition.
    /// </summary>
    public class ConditionalBlock : IHasBody
    {
        #region Properties
        /// <summary>
        /// Gets the condition to use when deiciding whether to run the block.
        /// </summary>
        public IExpression Condiion { get; }

        /// <summary>
        /// Gets whether the block is the default case. (No parameter)
        /// </summary>
        public bool IsDefault => Condiion == null;

        /// <summary>
        /// Gets the collection of statements within the block.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();

        /// <summary>
        /// Gets the scope of the block.
        /// </summary>
        public Scope Scope { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ConditionalBlock"/> class.
        /// </summary>
        /// <param name="parentScope">The parent scope of the block.</param>
        /// <param name="condition">The condition determining whether the code block should be run.</param>
        public ConditionalBlock(Scope parentScope, IExpression condition = null)
        {
            Scope = new Scope(parentScope);
            Condiion = condition;
        }
        #endregion
    }
}
