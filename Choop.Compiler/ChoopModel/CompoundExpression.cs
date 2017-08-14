using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Fields

        /// <summary>
        /// Whether the compund expression has been balanced.
        /// </summary>
        private bool _Balanced = false;

        #endregion

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

        /// <summary>
        /// Creates a new instance of the <see cref="CompoundExpression"/> class.
        /// </summary>
        /// <param name="operator">The operator use in the expression.</param>
        /// <param name="first">The first input to the operator.</param>
        /// <param name="second">The second input to the operator.</param>
        /// <param name="balanced">Whether the expression has been balanced.</param>
        private CompoundExpression(CompoundOperator @operator, IExpression first, IExpression second, bool balanced)
        {
            Operator = @operator;
            First = first;
            Second = second;
            _Balanced = balanced;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IExpression Balance()
        {
            if (_Balanced) return this;

            // TODO: Check operator is valid

            List<IExpression> chain = new List<IExpression>();
            GetChainedValues(this, chain);

            return Rebuild(chain, Operator);
        }

        /// <summary>
        /// Recursively gets all the values within a chain of the same operator.
        /// </summary>
        /// <param name="currentExpression">The current expression to inspect.</param>
        /// <param name="currentValues">The collection of values within the chain to add to.</param>
        private static void GetChainedValues(CompoundExpression currentExpression,
            ICollection<IExpression> currentValues)
        {
            // Left side, can be recursive
            CompoundExpression firstCompound = currentExpression.First as CompoundExpression;
            if (firstCompound == null || firstCompound.Operator != currentExpression.Operator)
                // Not explicitally part of this chain
                currentValues.Add(currentExpression.First.Balance());
            else
                // Part of chain, enter new expression
                GetChainedValues(firstCompound, currentValues);

            // Right side, not recursive
            currentValues.Add(currentExpression.Second.Balance());
        }

        /// <summary>
        /// Generates a balanced binary tree from a collection of values.
        /// </summary>
        /// <param name="chain">The list of values.</param>
        /// <param name="operation">The operation to be used between adjacent values.</param>
        /// <returns>A compound expression combining all the values in the chain which is balanced.</returns>
        private static CompoundExpression Rebuild(List<IExpression> chain, CompoundOperator operation)
        {
            // Get point to split chain at
            int midPos = (int) Math.Floor(chain.Count / 2d);

            // First
            IExpression first = midPos == 1
                ? chain[0]
                : Rebuild(chain.GetRange(0, midPos), operation);

            // Second
            IExpression second = chain.Count == 2
                ? chain[1]
                : Rebuild(chain.GetRange(midPos, chain.Count - midPos), operation);

            // Combine
            return new CompoundExpression(operation, first, second, true);
        }

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
                            ((ICompilable<object>) new MethodCall("ln", FileName, ErrorToken, First))
                            .Translate(context),
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
                            new TerminalExpression("2", TerminalType.Int),
                            Second, FileName, ErrorToken).Translate(context));
                case CompoundOperator.RShift:
                    return new Block(BlockSpecs.Divide, First.Translate(context),
                        new CompoundExpression(CompoundOperator.Pow,
                            new TerminalExpression("2", TerminalType.Int),
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