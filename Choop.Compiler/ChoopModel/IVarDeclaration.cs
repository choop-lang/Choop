namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a variable, const, array or list declaration.
    /// </summary>
    /// <typeparam name="T">The type of data stored inside the subject.</typeparam>
    public interface IVarDeclaration<out T> : ITypedDeclaration
    {
        #region Properties
        /// <summary>
        /// Gets the initial value stored in the subject.
        /// </summary>
        T Value { get; }
        #endregion
    }
}
