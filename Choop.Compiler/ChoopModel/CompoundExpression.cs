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
        public Block Translate(TranslationContext context)
        {
            // TODO: Optimise arithematic on constants

            switch (Operator)
            {
                case CompundOperator.Pow:
                    // TODO: negative inputs
                    return new Block(BlockSpecs.ComputeFunction, "e ^",
                        new Block(BlockSpecs.Times, new MethodCall("ln", First).Translate(context), Second.Translate(context)));
                case CompundOperator.Multiply:
                    return new Block(BlockSpecs.Times, First.Translate(context), Second.Translate(context));
                case CompundOperator.Divide:
                    return new Block(BlockSpecs.Divide, First.Translate(context), Second.Translate(context));
                case CompundOperator.Mod:
                    return new Block(BlockSpecs.Mod, First.Translate(context), Second.Translate(context));
                case CompundOperator.Concat:
                    return new Block(BlockSpecs.Join, First.Translate(context), Second.Translate(context));
                case CompundOperator.Plus:
                    return new Block(BlockSpecs.Add, First.Translate(context), Second.Translate(context));
                case CompundOperator.Minus:
                    return new Block(BlockSpecs.Minus, First.Translate(context), Second.Translate(context));
                case CompundOperator.LShift:
                    return new Block(BlockSpecs.Times, First.Translate(context),
                        new CompoundExpression(CompundOperator.Pow, new TerminalExpression("2", DataType.Number),
                            Second).Translate(context));
                case CompundOperator.RShift:
                    return new Block(BlockSpecs.Divide, First.Translate(context),
                        new CompoundExpression(CompundOperator.Pow, new TerminalExpression("2", DataType.Number),
                            Second).Translate(context));
                case CompundOperator.LessThan:
                    return new Block(BlockSpecs.LessThan, First.Translate(context), Second.Translate(context));
                case CompundOperator.GreaterThan:
                    return new Block(BlockSpecs.GreaterThan, First.Translate(context), Second.Translate(context));
                case CompundOperator.LessThanEq:
                    return new Block(BlockSpecs.Not, new Block(BlockSpecs.GreaterThan, First.Translate(context), Second.Translate(context)));
                case CompundOperator.GreaterThanEq:
                    return new Block(BlockSpecs.Not, new Block(BlockSpecs.LessThan, First.Translate(context), Second.Translate(context)));
                case CompundOperator.Equal:
                    return new Block(BlockSpecs.Equal, First.Translate(context), Second.Translate(context));
                case CompundOperator.NotEqual:
                    return new Block(BlockSpecs.Not, new Block(BlockSpecs.Equal, First.Translate(context), Second.Translate(context)));
                case CompundOperator.And:
                    // TODO: Extra optimisation here for when one input is known
                    return new Block(BlockSpecs.And, First.Translate(context), Second.Translate(context));
                case CompundOperator.Or:
                    return new Block(BlockSpecs.Or, First.Translate(context), Second.Translate(context));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}