﻿namespace Choop.Compiler.BlockModel
{
    /// <summary>
    /// Represents a sound.
    /// </summary>
    class Sound : IResource
    {
        #region Properties
        /// <summary>
        /// Gets or sets the display name of the sound.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of the corresponding file in the project ZIP archive.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash of the contents of the sound, followed by the file extension.
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// Gets or sets the number of samples in the sound.
        /// </summary>
        public int SampleCount { get; set; }

        /// <summary>
        /// Gets or sets the sampling rate of the sound.
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// Gets or sets a string describing the sound format. (Unused)
        /// </summary>
        public string Format { get; set; } = "";
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="Sound"/> class. 
        /// </summary>
        public Sound()
        {

        }
        #endregion
    }
}
