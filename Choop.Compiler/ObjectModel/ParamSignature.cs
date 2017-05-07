using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a method parameter.
    /// </summary>
    public class ParamSignature : VarSignature
    {
        #region Properties
        /// <summary>
        /// Gets whether the parameter is optional.
        /// </summary>
        public bool Optional { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ParamSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="optional">Whether the parameter is optional.</param>
        public ParamSignature(string name, DataType type, bool optional = false) : base(name, type)
        {
            Optional = optional;
        }
        #endregion
    }
}
