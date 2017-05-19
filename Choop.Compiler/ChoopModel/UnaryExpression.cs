using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an expression with an operator that takes a single input.
    /// </summary>
    public class UnaryExpression : IExpression
    {
        #region Properties
        /// <summary>
        /// Gets the operator used in the unary expression.
        /// </summary>
        public UnaryOperator Operator { get; }

        /// <summary>
        /// Gets the expression that the unary operator modifies.
        /// </summary>
        public IExpression Expression { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression that the unary operator modifies.</param>
        /// <param name="operator">The unary operator use in the unary expression.</param>
        public UnaryExpression(IExpression expression, UnaryOperator @operator)
        {
            Operator = @operator;
            Expression = expression;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
