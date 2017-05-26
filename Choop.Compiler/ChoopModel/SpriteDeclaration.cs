﻿using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a sprite declaration.
    /// </summary>
    public class SpriteDeclaration : SpriteBaseDeclaration, ICompilable<Sprite>, IHasSignature<SpriteSignature>
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
        public Sprite Translate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the signature of the object being declared.
        /// </summary>
        /// <returns>The signature of the object being declared.</returns>
        public SpriteSignature GetSignature()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
