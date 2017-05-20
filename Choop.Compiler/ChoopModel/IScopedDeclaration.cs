using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a variable or array declaration scoped inside a method.
    /// </summary>
    public interface IScopedDeclaration : IDeclaration, IHasSignature<StackValue>
    {
    }
}
