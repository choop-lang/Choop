using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an array item assignment statement.
    /// </summary>
    public class ArrayAssignStmt : IAssignStmt
    {
        #region Properties

        /// <summary>
        /// Gets the name of the array of the element being assigned.
        /// </summary>
        public string ArrayName { get; }

        /// <summary>
        /// Gets the index of the element being assigned.
        /// </summary>
        public IExpression Index { get; }

        /// <summary>
        /// Gets the operator used for the assignment.
        /// </summary>
        public AssignOperator Operator { get; }

        /// <summary>
        /// Gets the input to the assignment operator. (Null for increment and decrement)
        /// </summary>
        public IExpression Value { get; }

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        string IAssignStmt.ItemName => ArrayName;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array of the element being assigned.</param>
        /// <param name="index">The index of the element being assigned.</param>
        /// <param name="operator">The operator to use for the assignment.</param>
        /// <param name="value">The input to the assignment operator.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ArrayAssignStmt(string arrayName, IExpression index, AssignOperator @operator, IExpression value,
            string fileName, IToken errorToken)
        {
            Operator = @operator;
            Index = index;
            ArrayName = arrayName;
            Value = value;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            IExpression value;

            switch (Operator)
            {
                case AssignOperator.Equals:
                    value = Value;
                    break;
                case AssignOperator.AddEquals:
                    value = new CompoundExpression(CompoundOperator.Plus,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken),
                        Value, FileName, ErrorToken);
                    break;
                case AssignOperator.MinusEquals:
                    value = new CompoundExpression(CompoundOperator.Minus,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken),
                        Value, FileName, ErrorToken);
                    break;
                case AssignOperator.DotEquals:
                    value = new CompoundExpression(CompoundOperator.Multiply,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken), Value, FileName, ErrorToken);
                    break;
                case AssignOperator.PlusPlus:
                    value = new CompoundExpression(CompoundOperator.Plus,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken),
                        new TerminalExpression("1", TerminalType.Int), FileName, ErrorToken);
                    break;
                case AssignOperator.MinusMinus:
                    value = new CompoundExpression(CompoundOperator.Minus,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken),
                        new TerminalExpression("1", TerminalType.Int), FileName, ErrorToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // TODO stack arrays
            return new[]
            {
                new Block(BlockSpecs.ReplaceItemOfList, Index.Balance().Translate(context), ArrayName, value.Balance().Translate(context))
            };
        }

        #endregion
    }
}