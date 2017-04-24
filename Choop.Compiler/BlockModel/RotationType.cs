namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Specifies different modes of rotation for a sprite.
    /// </summary>
    public enum RotationType
    {
        /// <summary>
        /// Indicates standard rotation.
        /// </summary>
        Normal,
        /// <summary>
        /// Indicates that the sprite should not rotate but flip horizontally beyond 180 degrees.
        /// </summary>
        LeftRight,
        /// <summary>
        /// Indicates that the sprite should not rotate or flip.
        /// </summary>
        None
    }
}
