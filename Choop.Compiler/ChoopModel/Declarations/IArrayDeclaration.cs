using System.Collections.Generic;
using Choop.Compiler.ChoopModel.Expressions;

namespace Choop.Compiler.ChoopModel.Declarations
{
    /// <summary>
    /// Represents an array or list declaration.
    /// </summary>
    public interface IArrayDeclaration : IVarDeclaration<IEnumerable<IExpression>>
    {
        #region Properties

        /// <summary>
        /// Gets the length of the array.
        /// </summary>
        int Length { get; }

        #endregion
    }
}