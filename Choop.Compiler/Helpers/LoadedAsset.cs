namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Represents an asset that has been loaded into memory.
    /// </summary>
    public class LoadedAsset
    {
        #region Properties

        /// <summary>
        /// Gets the loaded contents of the asset.
        /// </summary>
        public byte[] Contents { get; }

        /// <summary>
        /// Gets the file extension of the asset.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Gets the internal file id of the asset.
        /// </summary>
        public int Id { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="LoadedAsset"/> class.
        /// </summary>
        /// <param name="contents">The contents of the asset.</param>
        /// <param name="extension">The file extension of the asset.</param>
        /// <param name="id">The internal file id of the asset.</param>
        public LoadedAsset(byte[] contents, string extension, int id)
        {
            Contents = contents;
            Extension = extension;
            Id = id;
        }

        #endregion
    }
}
