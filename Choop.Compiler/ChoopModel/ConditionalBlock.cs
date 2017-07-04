using System.Collections.ObjectModel;

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
        public Collection<IExpression> Conditions { get; } = new Collection<IExpression>();

        /// <summary>
        /// Gets whether the block is the default case. (No parameter)
        /// </summary>
        public bool IsDefault => Conditions.Count == 0;

        /// <summary>
        /// Gets the collection of statements within the block.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();
        #endregion
    }
}
