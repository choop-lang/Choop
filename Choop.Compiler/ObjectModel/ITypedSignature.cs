using Choop.Compiler.ChoopModel;

namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a typed code item.
    /// </summary>
    public interface ITypedSignature : ISignature
    {
        #region Properties
        /// <summary>
        /// Gets the data type of the subject.
        /// </summary>
        DataType Type { get; }
        #endregion
    }
}
