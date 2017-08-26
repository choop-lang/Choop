using Choop.Compiler.ChoopModel.Expressions;

namespace Choop.Compiler.ChoopModel.Selection
{
    /// <summary>
    /// Represents a single condition within a <see cref="ConditionalBlock"/>.
    /// </summary>
    public class Condition
    {
        #region Properties

        /// <summary>
        /// Gets the expression for the condtion.
        /// </summary>
        public IExpression Expression { get; }

        /// <summary>
        /// Gets or sets the comparison operator, for use in switch statements only.
        /// </summary>
        public CompoundOperator ComparisonOperator { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="Condition"/> class.
        /// </summary>
        /// <param name="expression">The expression of the condition.</param>
        /// <param name="comparisonOperator">The comparison operator, for use in switch statements only.</param>
        public Condition(IExpression expression, CompoundOperator comparisonOperator = CompoundOperator.Equal)
        {
            Expression = expression;
            ComparisonOperator = comparisonOperator;
        }

        #endregion
    }
}