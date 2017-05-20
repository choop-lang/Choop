using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global variable, const, array or list declaration.
    /// </summary>
    /// <typeparam name="T1">The type of data stored inside the subject.</typeparam>
    /// <typeparam name="T2">The type of variable signature.</typeparam>
    public interface IGlobalVarDeclaration<out T1, out T2> : IVarDeclaration<T1>, IHasSignature<T2> where T2:VarSignature
    {
    }
}