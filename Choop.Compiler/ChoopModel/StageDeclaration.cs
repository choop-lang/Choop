using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a stage declaration.
    /// </summary>
    public class StageDeclaration : SpriteBaseDeclaration, ICompilable<Stage>
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="StageDeclaration"/> class.
        /// </summary>
        /// <param name="metaFile">The file path to the metadata file for the stage.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public StageDeclaration(string metaFile, string fileName, IToken errorToken) : base(Settings.StageName, metaFile, fileName, errorToken)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Stage Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}