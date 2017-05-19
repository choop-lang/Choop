using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a variable or array declaration scoped inside a method.
    /// </summary>
    public interface IScopedDeclaration : IDeclaration
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="StackValue"/> describing the subject on the stack.
        /// </summary>
        StackValue StackRef { get; }
        #endregion
    }
}
