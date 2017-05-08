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
        /// Gets the default value for the parameter, if applicable.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Gets whether the parameter is optional.
        /// </summary>
        public bool Optional => DefaultValue == null;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ParamSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="defaultValue">The default value of the parameter, if it is optional.</param>
        public ParamSignature(string name, DataType type, object defaultValue = null) : base(name, type)
        {
            DefaultValue = defaultValue;
        }
        #endregion
    }
}
