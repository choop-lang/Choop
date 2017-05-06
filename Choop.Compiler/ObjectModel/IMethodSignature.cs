namespace Choop.Compiler.ObjectModel
{
    /// <summary>
    /// Represents a signature for any void, function or event handler.
    /// </summary>
    public interface IMethodSignature
    {
        #region Properties
        /// <summary>
        /// Gets the scope for the method.
        /// </summary>
        Scope MainScope { get; }
        #endregion
    }
}
