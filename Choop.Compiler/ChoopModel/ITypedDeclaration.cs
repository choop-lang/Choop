namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration with a type parameter.
    /// </summary>
    public interface ITypedDeclaration : IDeclaration
    {
        #region Properties
        /// <summary>
        /// Gets the type of the object being declared.
        /// </summary>
        DataType Type { get; }
        #endregion
    }
}
