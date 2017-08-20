using System.Collections.ObjectModel;
using System.Drawing;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Represents a Choop sprite settings file.
    /// </summary>
    public class SpriteSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index of the current costume.
        /// </summary>
        public int CostumeIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the location of the sprite.
        /// </summary>
        public Point Location { get; set; } = Point.Empty;

        /// <summary>
        /// Gets or sets the scale of the sprite. (0 - 1)
        /// </summary>
        public double Scale { get; set; } = 1;

        /// <summary>
        /// Gets or sets the angle in degrees of the sprite from the upwards direction. (Default is 90)
        /// </summary>
        public double Direction { get; set; } = 90;

        /// <summary>
        /// Gets or sets the rotation style of the sprite. (Default is normal)
        /// </summary>
        public RotationType RotationStyle { get; set; } = RotationType.Normal;

        /// <summary>
        /// Gets or sets whether the sprite is draggable in the player. (Default is false)
        /// </summary>
        public bool Draggable { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the sprite is visible. (Default is true)
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets the collection of costumes in the sprite.
        /// </summary>
        public Collection<CostumeAsset> Costumes { get; set; } = new Collection<CostumeAsset>();

        #endregion
    }
}
