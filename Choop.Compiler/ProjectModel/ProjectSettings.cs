using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Choop.Compiler.BlockModel;
using Newtonsoft.Json;

namespace Choop.Compiler.ProjectModel
{
    /// <summary>
    /// Represents a Choop project settings file.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ProjectSettings
    {
        #region Properties

        /// <summary>
        /// Gets the display name of the Choop project.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the author of the Choop project.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description for the Choop project.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the instructions for the Choop project.
        /// </summary>
        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets the date of when the project was first created.
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the project tempo in BPM.
        /// </summary>
        [JsonProperty("tempoBpm")]
        public double TempoBpm { get; set; } = 60;

        /// <summary>
        /// Gets the transparency (0 - 1) of the webcam feed.
        /// </summary>
        [JsonProperty("videoChannelAlpha")]
        public double VideoChannelAlpha { get; set; } = 0.5;

        /// <summary>
        /// Gets or sets whether the webcam feed is on.
        /// </summary>
        [JsonProperty("videoOn")]
        public bool VideoOn { get; set; }

        /// <summary>
        /// Gets the collection of files to use as backdrops.
        /// </summary>
        [JsonProperty("backdrops")]
        public Collection<Asset> Backdrops { get; } = new Collection<Asset>();

        /// <summary>
        /// Gets the collection of files in the Choop project.
        /// </summary>
        [JsonProperty("files")]
        public Collection<ChoopFile> Files { get; } = new Collection<ChoopFile>();

        /// <summary>
        /// Gets the collection of global watchers.
        /// </summary>
        [JsonProperty("watchers")]
        public Collection<StageMonitor> Watchers { get; } = new Collection<StageMonitor>();

        /// <summary>
        /// Gets the image used for the base pen layer.
        /// </summary>
        [JsonIgnore]
        public Image PenLayerImage { get; set; } = new Bitmap(480, 360);

        #endregion
    }
}
