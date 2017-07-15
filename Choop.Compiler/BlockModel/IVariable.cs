namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a variable or list.
    /// </summary>
    public interface IVariable : IJsonConvertable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the <see cref="IVariable"/>. 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets whether the <see cref="IVariable"/> is stored in the cloud. 
        /// </summary>
        bool Persistant { get; set; }

        #endregion
    }
}