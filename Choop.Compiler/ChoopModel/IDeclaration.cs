namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a declaration.
    /// </summary>
    public interface IDeclaration
    {
        #region Properties

        /// <summary>
        /// Gets the name of the object being declared.
        /// </summary>
        string Name { get; }

        #endregion
    }
}