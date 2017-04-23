namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a variable or list.
    /// </summary>
    interface IVariable
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
