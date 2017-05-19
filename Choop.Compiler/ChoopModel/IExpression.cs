using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an expression that can be evaluated to produce a value.
    /// </summary>
    public interface IExpression : ICompilable<Block>
    {
    }
}
