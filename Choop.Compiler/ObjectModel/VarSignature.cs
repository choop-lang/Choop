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
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the data type of the variable.
        /// </summary>
        public DataType Type { get; set; }
        #endregion
    }
}
