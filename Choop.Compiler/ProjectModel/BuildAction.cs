namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Specifies the possible build actions for a file.
    /// </summary>
    public enum BuildAction
    {
        /// <summary>
        /// Indicates that the compiler should ignore this file.
        /// </summary>
        Ignore,

        /// <summary>
        /// Indicates that the compiler should treat this file as Choop source code.
        /// </summary>
        SourceCode,

        /// <summary>
        /// Indiciates that the compiler should treat this file as a bitmap asset.
        /// </summary>
        BitmapAsset,

        /// <summary>
        /// Indiciates that the compiler should treat this file as a vector asset.
        /// </summary>
        VectorAsset,

        /// <summary>
        /// Indicates that the compiler should treat this file as a sound asset.
        /// </summary>
        SoundAsset
    }
}