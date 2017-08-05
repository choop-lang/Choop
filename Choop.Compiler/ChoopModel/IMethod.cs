namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents any method.
    /// </summary>
    public interface IMethod : IDeclaration, IHasBody
    {
        #region Properties

        /// <summary>
        /// Gets whether the method is unsafe.
        /// </summary>
        bool Unsafe { get; }

        /// <summary>
        /// Gets whether the method is atomic.
        /// </summary>
        bool Atomic { get; }

        #endregion
    }
}
