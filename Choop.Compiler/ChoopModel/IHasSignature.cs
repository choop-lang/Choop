using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration that has produces a signature.
    /// </summary>
    /// <typeparam name="T">The type of the signature</typeparam>
    public interface IHasSignature<out T> where T : ISignature
    {
        #region Methods
        /// <summary>
        /// Returns the signature of the object being declared.
        /// </summary>
        /// <returns>The signature of the object being declared.</returns>
        T GetSignature();
        #endregion
    }
}
