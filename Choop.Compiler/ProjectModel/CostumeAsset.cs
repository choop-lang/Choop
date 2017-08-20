using Newtonsoft.Json;

namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Represents a costume in a sprite.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class CostumeAsset
    {
        #region Properties

        /// <summary>
        /// Gets or sets the path to the image referenced.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the costume.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
