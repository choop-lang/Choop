namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a code item.
    /// </summary>
    public interface ISignature
    {
        #region Properties
        /// <summary>
        /// Gets the name of the subject.
        /// </summary>
        string Name { get; }
        #endregion
    }
}
