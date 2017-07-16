using System;
using System.Drawing;
using Antlr4.Runtime;
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
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public SpriteDeclaration(string name, string metaFile, string fileName, IToken errorToken) : base(name, metaFile, fileName, errorToken)
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
            // Create new sprite instance
            // TODO use metafile
            Sprite sprite = new Sprite
            {
                Name = Name
            };

            // TODO: Import modules

            // Variables
            foreach (GlobalVarDeclaration globalVarDeclaration in Variables)
                sprite.Variables.Add(globalVarDeclaration.Translate(context));

            // Lists
            foreach (GlobalListDeclaration globalListDeclaration in Lists)
                sprite.Lists.Add(globalListDeclaration.Translate(context));

            // Events
            foreach (EventHandler eventHandler in EventHandlers)
            {
                Tuple<BlockModel.EventHandler, BlockDef> translated = eventHandler.Translate(context);
                sprite.Scripts.Add(translated.Item1);
                sprite.Scripts.Add(translated.Item2);
            }

            // Methods
            foreach (MethodDeclaration methodDeclaration in Methods)
                sprite.Scripts.Add(methodDeclaration.Translate(context));

            // Insert default costume
            // TODO use metafile
            sprite.Costumes.Add(new Costume
            {
                Name = "costume1",
                Id = 1,
                Md5 = "09dc888b0b7df19f70d81588ae73420e.svg",
                BitmapResolution = 1,
                RotationCenter = new Point(47, 55)
            });

            return sprite;
        }

        #endregion
    }
}