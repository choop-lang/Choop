namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents the signature for a code item.
    /// </summary>
    public interface ISignature
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        string Name { get; set; }
        #endregion
    }
}
