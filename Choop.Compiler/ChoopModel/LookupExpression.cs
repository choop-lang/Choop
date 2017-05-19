using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a value lookup expression.
    /// </summary>
    public class LookupExpression : IExpression
    {
        #region Properties
        /// <summary>
        /// Gets the name of the identifier being looked up.
        /// </summary>
        public string IdentifierName { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="LookupExpression"/> class.
        /// </summary>
        /// <param name="identifierName">The name of the identifier being looked up.</param>
        public LookupExpression(string identifierName)
        {
            IdentifierName = identifierName;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
