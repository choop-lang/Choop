using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
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

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="UnaryExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression that the unary operator modifies.</param>
        /// <param name="operator">The unary operator use in the unary expression.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public UnaryExpression(IExpression expression, UnaryOperator @operator, string fileName, IToken errorToken)
        {
            Operator = @operator;
            FileName = fileName;
            ErrorToken = errorToken;
            Expression = expression;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IExpression Balance() => new UnaryExpression(Expression.Balance(), Operator, FileName, ErrorToken);

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            object translatedExpression = Expression.Translate(context);

            switch (Operator)
            {
                case UnaryOperator.Minus:
                    if (decimal.TryParse(translatedExpression.ToString(), out decimal translatedDecimal))
                        return -translatedDecimal;

                    return new Block(BlockSpecs.Minus, 0, translatedExpression);
                case UnaryOperator.Not:
                    if (bool.TryParse(translatedExpression.ToString(), out bool translatedBool))
                        return !translatedBool;

                    return new Block(BlockSpecs.Not, translatedExpression);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}