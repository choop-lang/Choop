using System.Collections.Generic;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a statement that can be executed.
    /// </summary>
    public interface IStatement : ICompilable<IEnumerable<Block>>
    {
    }
}