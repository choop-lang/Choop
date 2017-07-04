using System;
using Choop.Compiler.BlockModel;

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
        public ArrayAssignStmt(string arrayName, IExpression index, AssignOperator @operator, IExpression value = null)
        {
            Operator = @operator;
            Index = index;
            ArrayName = arrayName;
            Value = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
