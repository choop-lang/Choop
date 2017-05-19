using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a return statement.
    /// </summary>
    public class ReturnStmt : ICompilable<Block[]>
    {
        #region Properties
        /// <summary>
        /// Gets the expression for the value to be returned.
        /// </summary>
        public IExpression Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ReturnStmt"/> class.
        /// </summary>
        /// <param name="value">The value to be returned.</param>
        public ReturnStmt(IExpression value)
        {
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
