namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a file inside a Choop project.
    /// </summary>
    public class ChoopFile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the build action for the file.
        /// </summary>
        public BuildAction BuildAction { get; set; }

        /// <summary>
        /// Gets or sets the path of the file, relative to the project base path.
        /// </summary>
        public string Path { get; set; }

        #endregion
    }
}
