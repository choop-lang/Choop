using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a full array assignment statement.
    /// </summary>
    public class ArrayReAssignStmt : ICompilable<Block[]>
    {
        #region Properties
        /// <summary>
        /// Gets the name of the array being assigned.
        /// </summary>
        public string ArrayName { get; }
        
        /// <summary>
        /// Gets the new array contents.
        /// </summary>
        public IExpression[] Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array being assigned.</param>
        /// <param name="value">The new array contents.</param>
        public ArrayReAssignStmt(string arrayName, IExpression[] value)
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
