using System.Collections.ObjectModel;
using System.Drawing;
using Choop.Compiler.BlockModel;
using Newtonsoft.Json;

namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Represents a Choop sprite settings file.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class SpriteSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index of the current costume.
        /// </summary>
        [JsonProperty("costumeIndex")]
        public int CostumeIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the location of the sprite.
        /// </summary>
        [JsonProperty("location")]
        public Point Location { get; set; } = Point.Empty;

        /// <summary>
        /// Gets or sets the scale of the sprite. (0 - 1)
        /// </summary>
        [JsonProperty("scale")]
        public double Scale { get; set; } = 1;

        /// <summary>
        /// Gets or sets the angle in degrees of the sprite from the upwards direction. (Default is 90)
        /// </summary>
        [JsonProperty("direction")]
        public double Direction { get; set; } = 90;

        /// <summary>
        /// Gets or sets the rotation style of the sprite. (Default is normal)
        /// </summary>
        [JsonProperty("rotationStyle")]
        public RotationType RotationStyle { get; set; } = RotationType.Normal;

        /// <summary>
        /// Gets or sets whether the sprite is draggable in the player. (Default is false)
        /// </summary>
        [JsonProperty("draggable")]
        public bool Draggable { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the sprite is visible. (Default is true)
        /// </summary>
        [JsonProperty("visible")]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets the collection of costumes in the sprite.
        /// </summary>
        [JsonProperty("costumes")]
        public Collection<Asset> Costumes { get; set; } = new Collection<Asset>();

        #endregion
    }
}
