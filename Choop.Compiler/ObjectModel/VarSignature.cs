using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a variable.
    /// </summary>
    public class VarSignature : ITypedSignature
    {
        #region Properties
        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the data type of the variable.
        /// </summary>
        public DataType Type { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="VarSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The type of the variable.</param>
        public VarSignature(string name, DataType type)
        {
            Name = name;
            Type = type;
        }
        #endregion
    }
}
