namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a resource.
    /// </summary>
    public interface IResource : IJsonConvertable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the display name of the resource.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the corresponding file in the project ZIP archive.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash of the contents of the resource, followed by the file extension.
        /// </summary>
        string Md5 { get; set; }

        #endregion
    }
}