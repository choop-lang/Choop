using System;
using System.Collections.Generic;
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
        private readonly bool _balanced;

        /// <summary>
        /// Gets the collection of operators that can be balanced.
        /// </summary>
        private static readonly Dictionary<CompoundOperator, CompoundOperator> BalanceOps =
            new Dictionary<CompoundOperator, CompoundOperator>
            {
                {CompoundOperator.Plus, CompoundOperator.Plus},
                {CompoundOperator.Minus, CompoundOperator.Plus},
                {CompoundOperator.Multiply, CompoundOperator.Multiply},
                {CompoundOperator.Divide, CompoundOperator.Multiply},
                {CompoundOperator.Concat, CompoundOperator.Concat},
                {CompoundOperator.And, CompoundOperator.And},
                {CompoundOperator.Or, CompoundOperator.Or}
            };

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
            _balanced = balanced;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IExpression Balance()
        {
            if (_balanced) return this;

            if (!BalanceOps.TryGetValue(Operator, out CompoundOperator inverseOp))
                return new CompoundExpression(Operator, First.Balance(), Second.Balance(), true);

            List<IExpression> chain = new List<IExpression>();
            GetChainedValues(this, chain);

            return Rebuild(chain, Operator, inverseOp);
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
        /// <param name="primaryOp">The operation to be used between adjacent values.</param>
        /// <param name="secondaryOp">The operation used on the RHS of rebalanced trees.</param>
        /// <returns>A compound expression combining all the values in the chain which is balanced.</returns>
        private static CompoundExpression Rebuild(List<IExpression> chain, CompoundOperator primaryOp,
            CompoundOperator secondaryOp)
        {
            // Get point to split chain at
            int midPos = (int) Math.Floor(chain.Count / 2d);

            // First
            IExpression first = midPos == 1
                ? chain[0]
                : Rebuild(chain.GetRange(0, midPos), primaryOp, secondaryOp);

            // Second
            IExpression second = chain.Count == 2
                ? chain[1]
                : Rebuild(chain.GetRange(midPos, chain.Count - midPos), secondaryOp, secondaryOp);

            // Combine
            return new CompoundExpression(primaryOp, first, second, true);
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            // Translate expresion, assume already balanced
            object firstTranslated = First.Translate(context);
            object secondTranslated = Second.Translate(context);

            // Try and predetermine values to optimise with
            decimal secondValue = default(decimal);
            bool optimise = decimal.TryParse(firstTranslated.ToString(), out decimal firstValue) &&
                            decimal.TryParse(secondTranslated.ToString(), out secondValue);

            switch (Operator)
            {
                case CompoundOperator.Pow:
                    // Note: does not support negative x values; see https://github.com/chooper100/Choop/issues/3
                    return ((IExpression) new MethodCall("PowE", FileName, ErrorToken,
                        new CompoundExpression(CompoundOperator.Multiply,
                            new MethodCall("Ln", FileName, ErrorToken, First), Second, FileName, ErrorToken))
                    ).Translate(context);

                case CompoundOperator.Multiply:
                    return optimise
                        ? (object) (firstValue * secondValue)
                        : new Block(BlockSpecs.Times, firstTranslated, secondTranslated);

                case CompoundOperator.Divide:
                    return optimise
                        ? (object) (firstValue / secondValue)
                        : new Block(BlockSpecs.Divide, firstTranslated, secondTranslated);

                case CompoundOperator.Mod:
                    return optimise
                        ? (object) (firstValue % secondValue)
                        : new Block(BlockSpecs.Mod, firstTranslated, secondTranslated);

                case CompoundOperator.Concat:
                    string firstString = firstTranslated as string;
                    string secondString = secondTranslated as string;

                    return firstString != null && secondString != null
                        ? (object) (firstString + secondString)
                        : new Block(BlockSpecs.Join, firstTranslated, secondTranslated);

                case CompoundOperator.Plus:
                    return optimise
                        ? (object) (firstValue + secondValue)
                        : new Block(BlockSpecs.Add, firstTranslated, secondTranslated);

                case CompoundOperator.Minus:
                    return optimise
                        ? (object) (firstValue - secondValue)
                        : new Block(BlockSpecs.Minus, firstTranslated, secondTranslated);

                case CompoundOperator.LShift:
                    return new CompoundExpression(CompoundOperator.Multiply, First,
                        new CompoundExpression(CompoundOperator.Pow,
                            new TerminalExpression("2", TerminalType.Int),
                            Second, FileName, ErrorToken), FileName, ErrorToken).Translate(context);

                case CompoundOperator.RShift:
                    return new CompoundExpression(CompoundOperator.Divide, First,
                        new CompoundExpression(CompoundOperator.Pow,
                            new TerminalExpression("2", TerminalType.Int),
                            Second, FileName, ErrorToken), FileName, ErrorToken).Translate(context);

                case CompoundOperator.LessThan:
                    return optimise
                        ? (object) (firstValue < secondValue)
                        : new Block(BlockSpecs.LessThan, firstTranslated, secondTranslated);

                case CompoundOperator.GreaterThan:
                    return optimise
                        ? (object) (firstValue > secondValue)
                        : new Block(BlockSpecs.GreaterThan, firstTranslated, secondTranslated);

                case CompoundOperator.LessThanEq:
                    return optimise
                        ? (object) (firstValue <= secondValue)
                        : new Block(BlockSpecs.Not,
                            new Block(BlockSpecs.GreaterThan, firstTranslated, secondTranslated));

                case CompoundOperator.GreaterThanEq:
                    return optimise
                        ? (object) (firstValue >= secondValue)
                        : new Block(BlockSpecs.Not,
                            new Block(BlockSpecs.LessThan, firstTranslated, secondTranslated));

                case CompoundOperator.Equal:
                    return optimise
                        ? (object) (firstValue == secondValue)
                        : new Block(BlockSpecs.Equal, firstTranslated, secondTranslated);

                case CompoundOperator.NotEqual:
                    return optimise
                        ? (object) (firstValue != secondValue)
                        : new Block(BlockSpecs.Not,
                            new Block(BlockSpecs.Equal, firstTranslated, secondTranslated));

                case CompoundOperator.And:
                {
                    bool firstConverted = bool.TryParse(firstTranslated.ToString(), out bool firstBool);
                    bool secondConverted = bool.TryParse(secondTranslated.ToString(), out bool secondBool);

                    if (firstConverted && secondConverted)
                        return firstBool && secondBool;

                    if (firstConverted) return firstBool;

                    if (secondConverted) return secondBool;

                    return new Block(BlockSpecs.And, firstTranslated, secondTranslated);
                }

                case CompoundOperator.Or:
                {
                    bool firstConverted = bool.TryParse(firstTranslated.ToString(), out bool firstBool);
                    bool secondConverted = bool.TryParse(secondTranslated.ToString(), out bool secondBool);

                    if (firstConverted && secondConverted)
                        return firstBool || secondBool;

                    if (firstConverted)
                        return firstBool ? true : secondTranslated;

                    if (secondConverted)
                        return secondBool ? true : firstTranslated;

                    return new Block(BlockSpecs.Or, firstTranslated, secondTranslated);
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}