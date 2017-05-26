using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a stage declaration.
    /// </summary>
    public class StageDeclaration : SpriteBaseDeclaration, ICompilable<Stage>, IHasSignature<Project>
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="StageDeclaration"/> class.
        /// </summary>
        /// <param name="metaFile">The file path to the metadata file for the stage.</param>
        public StageDeclaration(string metaFile) : base("Stage", metaFile)
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

        /// <summary>
        /// Returns the signature of the object being declared.
        /// </summary>
        /// <returns>The signature of the object being declared.</returns>
        public Project GetSignature()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
