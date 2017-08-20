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
        /// Indicates that the compiler should treat this file as a Choop sprite definition.
        /// </summary>
        SpriteDefinition,

        /// <summary>
        /// Indiciates that the compiler should treat this file as a costume asset.
        /// </summary>
        CostumeAsset,

        /// <summary>
        /// Indicates that the compiler should treat this file as a sound asset.
        /// </summary>
        SoundAsset
    }
}