using System.IO;

namespace Choop.Compiler.Interfaces
{
    /// <summary>
    /// Provides base functionality to read files within a Choop project.
    /// </summary>
    public abstract class FileProvider : IFileProvider
    {
        #region Fields

        /// <summary>
        /// Whether a project is currently open.
        /// </summary>
        protected bool ProjectOpen;

        /// <summary>
        /// The path of the current open project.
        /// </summary>
        protected string ProjectPath;

        #endregion

        #region Methods

        /// <summary>
        /// Opens the project at the specified path.
        /// </summary>
        /// <param name="path">The path of the project being opened.</param>
        public virtual void OpenProject(string path)
        {
            ProjectPath = path;
            ProjectOpen = true;
        }

        /// <summary>
        /// Returns the stream to read the file at the specified path, relative to the project base path.
        /// </summary>
        /// <param name="path">The path of the file being read, relative to the project base path.</param>
        /// <returns>The stream to read the file from.</returns>
        public abstract StreamReader GetFileReadStream(string path);

        /// <summary>
        /// Closes the current open project.
        /// </summary>
        public virtual void CloseProject()
        {
            ProjectPath = string.Empty;
            ProjectOpen = false;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion
    }
}
