using System;
using Choop.Compiler.BlockModel;

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
        public StageDeclaration(string metaFile) : base(Settings.StageName, metaFile)
        {

        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Stage Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
