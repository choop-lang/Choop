namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents the info for a Scratch project.
    /// </summary>
    public class ProjectInfo
    {
        #region Properties
        /// <summary>
        /// Gets or sets the version of the flash player plugin under which the project was running.
        /// </summary>
        public string FlashVersion { get; set; } = "WIN 25,0,0,127";

        /// <summary>
        /// Gets or sets the user agent string of the browser or the scratch editor.
        /// </summary>
        public string UserAgent { get; set; } = "Scratch 2.0 Offline Editor";

        /// <summary>
        /// Gets or sets the number of scripts in the project.
        /// </summary>
        public int ScriptCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the number of sprites in the project.
        /// </summary>
        public int SpriteCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets whether the video was on when the project was last saved.
        /// </summary>
        public bool VideoOn { get; set; } = false;

        /// <summary>
        /// Gets or sets the version of the Scratch editor which the project was created using.
        /// </summary>
        public string SwfVersion { get; set; } = "v454";
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ProjectInfo"/> class. 
        /// </summary>
        public ProjectInfo() {

        }
        #endregion
    }
}
