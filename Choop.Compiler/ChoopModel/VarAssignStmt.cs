using System;
using Choop.Compiler.BlockModel;

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

        string IAssignStmt.ItemName => VariableName;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="variableName">The name of the variable being assigned.</param>
        /// <param name="operator">The operator to use for the assignment.</param>
        /// <param name="value">The input to the assignment operator.</param>
        public VarAssignStmt(string variableName, AssignOperator @operator, IExpression value = null)
        {
            Operator = @operator;
            VariableName = variableName;
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
