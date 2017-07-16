using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

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
        public CompoundOperator Operator { get; }

        /// <summary>
        /// Gets the  first input to the operator.
        /// </summary>
        public IExpression First { get; }

        /// <summary>
        /// Gets the  second input to the operator.
        /// </summary>
        public IExpression Second { get; }

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
        /// Creates a new instance of the <see cref="CompoundExpression"/> class.
        /// </summary>
        /// <param name="operator">The operator use in the expression.</param>
        /// <param name="first">The first input to the operator.</param>
        /// <param name="second">The second input to the operator.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public CompoundExpression(CompoundOperator @operator, IExpression first, IExpression second, string fileName,
            IToken errorToken)
        {
            Operator = @operator;
            First = first;
            Second = second;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            // TODO: Optimise arithematic on constants

            switch (Operator)
            {
                case CompoundOperator.Pow:
                    // TODO: negative inputs
                    return new Block(BlockSpecs.ComputeFunction, "e ^",
                        new Block(BlockSpecs.Times,
                            new MethodCall("ln", FileName, ErrorToken, First).Translate(context),
                            Second.Translate(context)));
                case CompoundOperator.Multiply:
                    return new Block(BlockSpecs.Times, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Divide:
                    return new Block(BlockSpecs.Divide, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Mod:
                    return new Block(BlockSpecs.Mod, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Concat:
                    return new Block(BlockSpecs.Join, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Plus:
                    return new Block(BlockSpecs.Add, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Minus:
                    return new Block(BlockSpecs.Minus, First.Translate(context), Second.Translate(context));
                case CompoundOperator.LShift:
                    return new Block(BlockSpecs.Times, First.Translate(context),
                        new CompoundExpression(CompoundOperator.Pow,
                            new TerminalExpression("2", DataType.Number, FileName, ErrorToken),
                            Second, FileName, ErrorToken).Translate(context));
                case CompoundOperator.RShift:
                    return new Block(BlockSpecs.Divide, First.Translate(context),
                        new CompoundExpression(CompoundOperator.Pow,
                            new TerminalExpression("2", DataType.Number, FileName, ErrorToken),
                            Second, FileName, ErrorToken).Translate(context));
                case CompoundOperator.LessThan:
                    return new Block(BlockSpecs.LessThan, First.Translate(context), Second.Translate(context));
                case CompoundOperator.GreaterThan:
                    return new Block(BlockSpecs.GreaterThan, First.Translate(context), Second.Translate(context));
                case CompoundOperator.LessThanEq:
                    return new Block(BlockSpecs.Not,
                        new Block(BlockSpecs.GreaterThan, First.Translate(context), Second.Translate(context)));
                case CompoundOperator.GreaterThanEq:
                    return new Block(BlockSpecs.Not,
                        new Block(BlockSpecs.LessThan, First.Translate(context), Second.Translate(context)));
                case CompoundOperator.Equal:
                    return new Block(BlockSpecs.Equal, First.Translate(context), Second.Translate(context));
                case CompoundOperator.NotEqual:
                    return new Block(BlockSpecs.Not,
                        new Block(BlockSpecs.Equal, First.Translate(context), Second.Translate(context)));
                case CompoundOperator.And:
                    // TODO: Extra optimisation here for when one input is known
                    return new Block(BlockSpecs.And, First.Translate(context), Second.Translate(context));
                case CompoundOperator.Or:
                    return new Block(BlockSpecs.Or, First.Translate(context), Second.Translate(context));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}