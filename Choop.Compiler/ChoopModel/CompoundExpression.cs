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
            // TODO: Optimise arithematic on constants

            switch (Operator)
            {
                case CompundOperator.Pow:
                    // TODO: negative inputs
                    return new Block("computeFunction:of:", "e ^",
                        new Block("*", new MethodCall("ln", First).Translate(), Second.Translate()));
                case CompundOperator.Multiply:
                    return new Block("*", First.Translate(), Second.Translate());
                case CompundOperator.Divide:
                    return new Block("/", First.Translate(), Second.Translate());
                case CompundOperator.Mod:
                    return new Block("%", First.Translate(), Second.Translate());
                case CompundOperator.Concat:
                    return new Block("concatenate:with:", First.Translate(), Second.Translate());
                case CompundOperator.Plus:
                    return new Block("+", First.Translate(), Second.Translate());
                case CompundOperator.Minus:
                    return new Block("-", First.Translate(), Second.Translate());
                case CompundOperator.LShift:
                    return new Block("*", First.Translate(),
                        new CompoundExpression(CompundOperator.Pow, new TerminalExpression("2", DataType.Number),
                            Second).Translate());
                case CompundOperator.RShift:
                    return new Block("/", First.Translate(),
                        new CompoundExpression(CompundOperator.Pow, new TerminalExpression("2", DataType.Number),
                            Second).Translate());
                case CompundOperator.LessThan:
                    return new Block("<", First.Translate(), Second.Translate());
                case CompundOperator.GreaterThan:
                    return new Block(">", First.Translate(), Second.Translate());
                case CompundOperator.LessThanEq:
                    return new Block("not", new Block(">", First.Translate(), Second.Translate()));
                case CompundOperator.GreaterThanEq:
                    return new Block("not", new Block("<", First.Translate(), Second.Translate()));
                case CompundOperator.Equal:
                    return new Block("=", First.Translate(), Second.Translate());
                case CompundOperator.NotEqual:
                    return new Block("not", new Block("=", First.Translate(), Second.Translate()));
                case CompundOperator.And:
                    // TODO: Extra optimisation here for when one input is known
                    return new Block("&", First.Translate(), Second.Translate());
                case CompundOperator.Or:
                    return new Block("|", First.Translate(), Second.Translate());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}