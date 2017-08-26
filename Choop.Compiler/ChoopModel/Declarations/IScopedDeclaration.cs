using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Declarations
{
    /// <summary>
    /// Represents a variable or array declaration scoped inside a method.
    /// </summary>
    public interface IScopedDeclaration : IDeclaration
    {
        #region Methods

        /// <summary>
        /// Gets the stack reference for this variable.
        /// </summary>
        /// <returns>The stack reference for this variable.</returns>
        StackValue GetStackRef();

        #endregion
    }
}