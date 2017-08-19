using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a Choop project settings file.
    /// </summary>
    public class ProjectSettings
    {
        #region Properties

        /// <summary>
        /// Gets the display name of the Choop project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the author of the Choop project.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description for the Choop project.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the instructions for the Choop project.
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets the date of when the project was first created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the date of when the project was last modified.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the project tempo in BPM.
        /// </summary>
        public double TempoBpm { get; set; }

        /// <summary>
        /// Gets the transparency (0 - 1) of the webcam feed.
        /// </summary>
        public double VideoChannelAlpha { get; set; }

        /// <summary>
        /// Gets or sets whether the webcam feed is on.
        /// </summary>
        public bool VideoOn { get; set; }

        /// <summary>
        /// Gets or sets the file path to the base pen layer.
        /// </summary>
        public string PenLayerFile { get; set; }

        /// <summary>
        /// Gets the collection of files to use as backdrops.
        /// </summary>
        public Collection<string> Backdrops { get; } = new Collection<string>();

        /// <summary>
        /// Gets the collection of global watchers.
        /// </summary>
        public Collection<StageMonitor> Watchers { get; } = new Collection<StageMonitor>();

        #endregion
    }
}
