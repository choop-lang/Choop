using System.Collections.Generic;
using Choop.Compiler.ProjectModel;

namespace Choop.Compiler.Helpers
{
    /// <summary>
    /// Represents a collection of loaded assets.
    /// </summary>
    public class AssetCollection
    {
        #region Properties

        /// <summary>
        /// The collection of costume files, with their path and file contents.
        /// </summary>
        public Dictionary<string, byte[]> CostumeFiles { get; } = new Dictionary<string, byte[]>();

        /// <summary>
        /// The collection of sound files, with their path and file contents.
        /// </summary>
        public Dictionary<string, byte[]> SoundFiles { get; } = new Dictionary<string, byte[]>();

        /// <summary>
        /// The collection of sprite definition files, with their path and their deserialized file contents.
        /// </summary>
        public Dictionary<string, SpriteSettings> SpriteDefinitionFiles { get; } = new Dictionary<string, SpriteSettings>();

        #endregion
    }
}
