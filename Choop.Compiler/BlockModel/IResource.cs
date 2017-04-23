namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a resource.
    /// </summary>
    interface IResource
    {
        #region Properties
        /// <summary>
        /// Gets or sets the display name of the resource.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the corresponding file in the project ZIP archive.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash of the contents of the resource, followed by the file extension.
        /// </summary>
        string MD5 { get; set; }
        #endregion
    }
}
