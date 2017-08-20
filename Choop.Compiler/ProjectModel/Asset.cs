using Newtonsoft.Json;

namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Represents an asset.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Asset
    {
        #region Properties

        /// <summary>
        /// Gets or sets the path to the file referenced.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
