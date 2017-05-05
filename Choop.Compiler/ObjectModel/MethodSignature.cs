using Choop.Compiler.ChoopModel;
using System.Collections.ObjectModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a method signature.
    /// </summary>
    public class MethodSignature : ITypedSignature
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of parameters for the method.
        /// </summary>
        public Collection<ParamSignature> Params { get; } = new Collection<ParamSignature>();

        /// <summary>
        /// Gets the scope for the method.
        /// </summary>
        public Scope MainScope { get; } = new Scope();

        /// <summary>
        /// Gets or sets the return type of the method.
        /// </summary>
        public DataType Type { get; set; }

        /// <summary>
        /// Gets or sets whether the method will return a value.
        /// </summary>
        public bool HasReturn { get; set; }
        #endregion
    }
}
