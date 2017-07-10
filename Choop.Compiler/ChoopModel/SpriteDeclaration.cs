using System;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a sprite declaration.
    /// </summary>
    public class SpriteDeclaration : SpriteBaseDeclaration, ICompilable<Sprite>
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="SpriteDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the sprite.</param>
        /// <param name="metaFile">The file path to the metadata file for this sprite.</param>
        public SpriteDeclaration(string name, string metaFile) : base(name, metaFile)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Sprite Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}