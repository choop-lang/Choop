using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an expression which is terminal. (constant)
    /// </summary>
    public class TerminalExpression : IExpression
    {
        #region Properties
        /// <summary>
        /// Gets the constant value of the terminal expression.
        /// </summary>
        public object Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="TerminalExpression"/> class.
        /// </summary>
        /// <param name="value">The constant value of the expression.</param>
        public TerminalExpression(object value)
        {
            Value = value;
        }
        #endregion
        #region Operators

        public static implicit operator TerminalExpression(string value)
        {
            return new TerminalExpression(value);
        }

        public static implicit operator TerminalExpression(double value)
        {
            return new TerminalExpression(value);
        }

        public static implicit operator TerminalExpression(bool value)
        {
            return new TerminalExpression(value);
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
