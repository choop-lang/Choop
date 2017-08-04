using System;
using System.Linq;
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
            // Get variable
            IDeclaration variable = context.GetDeclaration(VariableName);

            // Check variable was found
            if (variable == null)
            {
                context.ErrorList.Add(new CompilerError($"Variable '{VariableName}' was not defined",
                    ErrorType.NotDefined, ErrorToken, FileName));
                return new Block[0];
            }

            // Try as stack variable
            StackValue stackValue = variable as StackValue;
            if (stackValue != null && stackValue.StackSpace == 1)
                switch (Operator)
                {
                    case AssignOperator.Equals:
                        return new[] {stackValue.CreateVariableAssignment(Value.Translate(context))};
                    case AssignOperator.AddEquals:
                        return new[] {stackValue.CreateVariableIncrement(Value.Translate(context))};
                    case AssignOperator.MinusEquals:
                        return new[]
                        {
                            stackValue.CreateVariableIncrement(new UnaryExpression(Value, UnaryOperator.Minus, FileName,
                                ErrorToken).Translate(context))
                        };
                    case AssignOperator.DotEquals:
                        return new[]
                        {
                            stackValue.CreateVariableAssignment(new CompoundExpression(CompoundOperator.Concat,
                                    new LookupExpression(stackValue, FileName, ErrorToken), Value, FileName, ErrorToken)
                                .Translate(context))
                        };
                    case AssignOperator.PlusPlus:
                        return new[] {stackValue.CreateVariableIncrement(1)};
                    case AssignOperator.MinusMinus:
                        return new[] {stackValue.CreateVariableIncrement(-1)};
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            // Try as global variable
            GlobalVarDeclaration globalVarDeclaration = variable as GlobalVarDeclaration;
            if (globalVarDeclaration != null)
                switch (Operator)
                {
                    case AssignOperator.Equals:
                        return new BlockBuilder(BlockSpecs.SetVariableTo, context).AddParam(VariableName)
                            .AddParam(Value).Create().ToArray();

                    case AssignOperator.AddEquals:
                        return new BlockBuilder(BlockSpecs.ChangeVarBy, context).AddParam(VariableName).AddParam(Value)
                            .Create().ToArray();

                    case AssignOperator.MinusEquals:
                        return new BlockBuilder(BlockSpecs.ChangeVarBy, context).AddParam(VariableName)
                            .AddParam(new UnaryExpression(Value, UnaryOperator.Minus, FileName, ErrorToken)).Create()
                            .ToArray();

                    case AssignOperator.DotEquals:
                        return new BlockBuilder(BlockSpecs.SetVariableTo, context).AddParam(VariableName).AddParam(
                            new CompoundExpression(CompoundOperator.Concat,
                                new LookupExpression(globalVarDeclaration, FileName, ErrorToken), Value, FileName,
                                ErrorToken)).Create().ToArray();

                    case AssignOperator.PlusPlus:
                        return new BlockBuilder(BlockSpecs.ChangeVarBy, context).AddParam(VariableName).AddParam(1)
                            .Create().ToArray();

                    case AssignOperator.MinusMinus:
                        return new BlockBuilder(BlockSpecs.ChangeVarBy, context).AddParam(VariableName).AddParam(-1)
                            .Create().ToArray();

                    default:
                        throw new ArgumentOutOfRangeException();
                }

            // Fail
            context.ErrorList.Add(new CompilerError($"'{VariableName}' is not a variable", ErrorType.InvalidArgument,
                ErrorToken, FileName));
            return new Block[0];
        }

        #endregion
    }
}