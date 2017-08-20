using System;
using System.IO;

namespace Choop.Compiler.Interfaces
{
    /// <summary>
    /// Provides functionality to read files within a Choop project.
    /// </summary>
    public interface IFileProvider : IDisposable
    {
        #region Methods

        /// <summary>
        /// Opens the project at the specified path.
        /// </summary>
        /// <param name="path">The path of the project being opened.</param>
        void OpenProject(string path);

        /// <summary>
        /// Returns the underlying stream of the file at the specified path, relative to the project base path.
        /// </summary>
        /// <param name="path">The path of the file being read, relative to the project base path.</param>
        /// <returns>The stream to read the file from.</returns>
        Stream GetFileReadStream(string path);

        /// <summary>
        /// Closes the current open project.
        /// </summary>
        void CloseProject();

        #endregion
    }
}
