using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a full array assignment statement.
    /// </summary>
    public class ArrayFullAssignStmt : IAssignStmt
    {
        #region Properties
        /// <summary>
        /// Gets the name of the array being assigned.
        /// </summary>
        public string ArrayName { get; }

        /// <summary>
        /// Gets the operator used for the assignment. (Can only be equals)
        /// </summary>
        public AssignOperator Operator => AssignOperator.Equals;

        /// <summary>
        /// Gets the new array contents.
        /// </summary>
        public IExpression[] Value { get; }
        
        string IAssignStmt.ItemName => ArrayName;
        
        IExpression IAssignStmt.Value => Value;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array being assigned.</param>
        /// <param name="value">The new array contents.</param>
        public ArrayFullAssignStmt(string arrayName, object[] value)
        {
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
