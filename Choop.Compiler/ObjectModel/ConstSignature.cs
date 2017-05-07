using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a constant.
    /// </summary>
    public class ConstSignature : VarSignature
    {
        #region Properties
        /// <summary>
        /// Gets the value of the constant.
        /// </summary>
        public object Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ConstSignature"/> class.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <param name="type">The type of the constant.</param>
        /// <param name="value">The value of the constant.</param>
        public ConstSignature(string name, DataType type, object value) : base(name, type)
        {
            Value = value;
        }
        #endregion
    }
}
