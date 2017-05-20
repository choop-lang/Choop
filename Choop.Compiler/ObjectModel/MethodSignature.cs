using Choop.Compiler.ChoopModel;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a void or function signature.
    /// </summary>
    public class MethodSignature : IMethodSignature, ITypedSignature
    {
        #region Properties
        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the collection of parameters for the method.
        /// </summary>
        public Collection<ParamSignature> Params { get; } = new Collection<ParamSignature>();

        /// <summary>
        /// Gets the scope for the method.
        /// </summary>
        public Scope MainScope { get; } = new Scope();

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets whether the method will return a value.
        /// </summary>
        public bool HasReturn { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="MethodSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="type">The return type of the method.</param>
        public MethodSignature(string name, DataType type)
        {
            Name = name;
            Type = type;
        }
        #endregion
    }
}
