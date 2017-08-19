using Newtonsoft.Json;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a file inside a Choop project.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ChoopFile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the attribution for the file.
        /// </summary>
        [JsonProperty("attribution")]
        public string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the build action for the file.
        /// </summary>
        [JsonProperty("buildAction")]
        public BuildAction BuildAction { get; set; }

        /// <summary>
        /// Gets or sets the path of the file, relative to the project base path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        #endregion
    }
}
