using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a full array assignment statement.
    /// </summary>
    public class ArrayReAssignStmt : IStatement
    {
        #region Properties
        /// <summary>
        /// Gets the name of the array being assigned.
        /// </summary>
        public string ArrayName { get; }
        
        /// <summary>
        /// Gets the new array contents.
        /// </summary>
        public Collection<IExpression> Items { get; } = new Collection<IExpression>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="VarAssignStmt"/> class.
        /// </summary>
        /// <param name="arrayName">The name of the array being assigned.</param>
        public ArrayReAssignStmt(string arrayName)
        {
            ArrayName = arrayName;
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
