﻿using System;
using System.IO;

namespace Choop.Compiler.Interfaces
{
    /// <summary>
    /// Provides functionality to read files from a disk within a Choop project.
    /// </summary>
    public class DiskFileProvider : FileProvider
    {
        #region Methods

        /// <summary>
        /// Returns the stream to read the file at the specified path, relative to the project base path.
        /// </summary>
        /// <param name="path">The path of the file being read, relative to the project base path.</param>
        /// <returns>The stream to read the file from.</returns>
        public override StreamReader GetFileReadStream(string path)
        {
            if (!ProjectOpen)
                throw new InvalidOperationException("No project open");

            return new StreamReader(ProjectPath + path);
        }

        #endregion
    }
}