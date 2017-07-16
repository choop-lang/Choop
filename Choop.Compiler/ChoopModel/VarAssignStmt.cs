using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an assignment statement.
    /// </summary>
    public class VarAssignStmt : IAssignStmt
    {
        #region Properties

        /// <summary>
        /// Gets the name of the variable being assigned.
        /// </summary>
        public string VariableName { get; }

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

        string IAssignStmt.ItemName => VariableName;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="variableName">The name of the variable being assigned.</param>
        /// <param name="operator">The operator to use for the assignment.</param>
        /// <param name="value">The input to the assignment operator.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public VarAssignStmt(string variableName, AssignOperator @operator, string fileName, IToken errorToken,
            IExpression value = null)
        {
            Operator = @operator;
            FileName = fileName;
            ErrorToken = errorToken;
            VariableName = variableName;
            Value = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            // TODO: variables on stack

            Block setBlock;

            switch (Operator)
            {
                case AssignOperator.Equals:
                    setBlock = new Block(BlockSpecs.SetVariableTo, VariableName, Value.Translate(context));
                    break;
                case AssignOperator.AddEquals:
                    setBlock = new Block(BlockSpecs.ChangeVarBy, VariableName, Value.Translate(context));
                    break;
                case AssignOperator.MinusEquals:
                    setBlock = new Block(BlockSpecs.ChangeVarBy, VariableName,
                        new UnaryExpression(Value, UnaryOperator.Minus, FileName, ErrorToken).Translate(context));
                    break;
                case AssignOperator.DotEquals:
                    setBlock = new Block(BlockSpecs.SetVariableTo, VariableName,
                        new CompoundExpression(CompoundOperator.Concat,
                                new LookupExpression(VariableName, FileName, ErrorToken), Value, FileName, ErrorToken)
                            .Translate(context));
                    break;
                case AssignOperator.PlusPlus:
                    setBlock = new Block(BlockSpecs.ChangeVarBy, VariableName, 1);
                    break;
                case AssignOperator.MinusMinus:
                    setBlock = new Block(BlockSpecs.ChangeVarBy, VariableName, -1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new[] {setBlock};
        }

        #endregion
    }
}