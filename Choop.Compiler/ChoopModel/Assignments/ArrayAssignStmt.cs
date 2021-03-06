﻿using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Assignments
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
        public IEnumerable<Block> Translate(TranslationContext context)
        {
            // Get generic declaration

            IDeclaration declaration = context.GetDeclaration(ArrayName);

            if (declaration == null)
            {
                context.ErrorList.Add(new CompilerError($"'{ArrayName}' is not defined", ErrorType.NotDefined,
                    ErrorToken, FileName));
                return Enumerable.Empty<Block>();
            }

            // Scoped arrays

            if (declaration is StackValue scopedArray)
            {
                Operator.TestCompatible(scopedArray.Type, context, FileName, ErrorToken);

                switch (Operator)
                {
                    case AssignOperator.Equals:
                        return scopedArray.CreateArrayAssignment(context, Value, Index);

                    case AssignOperator.AddEquals:
                        return scopedArray.CreateArrayAssignment(context,
                            new CompoundExpression(CompoundOperator.Plus,
                                new ArrayLookupExpression(scopedArray, Index, FileName, ErrorToken), Value, FileName,
                                ErrorToken), Index);

                    case AssignOperator.MinusEquals:
                        return scopedArray.CreateArrayAssignment(context,
                            new CompoundExpression(CompoundOperator.Minus,
                                new ArrayLookupExpression(scopedArray, Index, FileName, ErrorToken), Value, FileName,
                                ErrorToken), Index);

                    case AssignOperator.DotEquals:
                        return scopedArray.CreateArrayAssignment(context,
                            new CompoundExpression(CompoundOperator.Concat,
                                new ArrayLookupExpression(scopedArray, Index, FileName, ErrorToken), Value, FileName,
                                ErrorToken), Index);

                    case AssignOperator.PlusPlus:
                        return scopedArray.CreateArrayAssignment(context,
                            new CompoundExpression(CompoundOperator.Plus,
                                new ArrayLookupExpression(scopedArray, Index, FileName, ErrorToken), new TerminalExpression(1, DataType.Number),
                                FileName, ErrorToken), Index);

                    case AssignOperator.MinusMinus:
                        return scopedArray.CreateArrayAssignment(context,
                            new CompoundExpression(CompoundOperator.Minus,
                                new ArrayLookupExpression(scopedArray, Index, FileName, ErrorToken), new TerminalExpression(1, DataType.Number),
                                FileName, ErrorToken), Index);

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // Global arrays

            GlobalListDeclaration globalArray = declaration as GlobalListDeclaration;

            if (globalArray == null)
            {
                // Not an array
                context.ErrorList.Add(new CompilerError($"Object '{ArrayName}' is not an array", ErrorType.ImproperUsage, ErrorToken, FileName));
                return Enumerable.Empty<Block>();
            }

            Operator.TestCompatible(globalArray.Type, context, FileName, ErrorToken);

            IExpression value;

            switch (Operator)
            {
                case AssignOperator.Equals:
                    DataType valueType = Value.GetReturnType(context);
                    if (!globalArray.Type.IsCompatible(valueType))
                        context.ErrorList.Add(new CompilerError(
                            $"Expected value of type '{globalArray.Type}' but instead found value of type '{valueType}'",
                            ErrorType.TypeMismatch, Value.ErrorToken, Value.FileName));

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
                        new TerminalExpression(1, DataType.Number), FileName, ErrorToken);
                    break;

                case AssignOperator.MinusMinus:
                    value = new CompoundExpression(CompoundOperator.Minus,
                        new ArrayLookupExpression(ArrayName, Index, FileName, ErrorToken),
                        new TerminalExpression(1, DataType.Number), FileName, ErrorToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new BlockBuilder(BlockSpecs.ReplaceItemOfList, context)
                .AddParam(Index)
                .AddParam(ArrayName)
                .AddParam(value)
                .Create();
        }

        #endregion
    }
}