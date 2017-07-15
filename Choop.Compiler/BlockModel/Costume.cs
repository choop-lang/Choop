using System.Drawing;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a costume.
    /// </summary>
    public class Costume : IResource
    {
        #region Properties

        /// <summary>
        /// Gets or sets the display name of the costume.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the corresponding file in the project ZIP archive.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash of the contents of the costume, followed by the file extension.
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// Gets or sets the number of pixels that fit along the X axis of a single screen pixel
        /// in a bitmap image. (Default is 1)
        /// </summary>
        public int BitmapResolution { get; set; } = 1;

        /// <summary>
        /// Gets or sets the rotation centre of the costume.
        /// </summary>
        public Point RotationCenter { get; set; } = Point.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the current instance into a JSON object.
        /// </summary>
        /// <returns>The JSON representation of the current instance.</returns>
        public JToken ToJson()
        {
            return new JObject
            {
                {"costumeName", Name},
                {"baseLayerID", Id},
                {"baseLayerMD5", Md5},
                {"bitmapResolution", BitmapResolution},
                {"rotationCenterX", RotationCenter.X},
                {"rotationCenterY", RotationCenter.Y}
            };
        }

        #endregion
    }
}