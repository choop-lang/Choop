using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an expression with an operator that accepts combines 2 inputs.
    /// </summary>
    public class CompoundExpression : IExpression
    {
        #region Properties
        /// <summary>
        /// Gets the operator used in the expression.
        /// </summary>
        public CompundOperator Operator { get; }

        /// <summary>
        /// Gets the  first input to the operator.
        /// </summary>
        public IExpression First { get; }

        /// <summary>
        /// Gets the  second input to the operator.
        /// </summary>
        public IExpression Second { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="CompoundExpression"/> class.
        /// </summary>
        /// <param name="operator">The operator use in the expression.</param>
        /// <param name="first">The first input to the operator.</param>
        /// <param name="second">The second input to the operator.</param>
        public CompoundExpression(CompundOperator @operator, IExpression first, IExpression second)
        {
            Operator = @operator;
            First = first;
            Second = second;
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
