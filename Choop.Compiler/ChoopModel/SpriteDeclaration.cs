using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a sprite declaration.
    /// </summary>
    public class SpriteDeclaration : ISpriteDeclaration, ICompilable<Sprite>
    {
        #region Properties
        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        public string Name => Signature.Name;

        /// <summary>
        /// Gets the filepath of the Sprite metadata file for this sprite.
        /// </summary>
        public string MetaFile { get; }

        /// <summary>
        /// Gets the collection of names of modules imported by the sprite.
        /// </summary>
        public Collection<string> ImportedModules { get; } = new Collection<string>();

        /// <summary>
        /// Gets the collection of constant declarations. (Not compiled)
        /// </summary>
        public Collection<ConstDeclaration> Constants { get; } = new Collection<ConstDeclaration>();

        /// <summary>
        /// Gets the collection of variable declarations.
        /// </summary>
        public Collection<GlobalVarDeclaration> Variables { get; } = new Collection<GlobalVarDeclaration>();

        /// <summary>
        /// Gets the collection of list declarations.
        /// </summary>
        public Collection<GlobalListDeclaration> Lists { get; } = new Collection<GlobalListDeclaration>();

        /// <summary>
        /// Gets the collection of method declarations.
        /// </summary>
        public Collection<MethodDeclaration> Methods { get; } = new Collection<MethodDeclaration>();

        /// <summary>
        /// Gets the signature of the sprite.
        /// </summary>
        public SpriteSignature Signature { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="SpriteDeclaration"/> class.
        /// </summary>
        /// <param name="signature">The signature of the sprite.</param>
        /// <param name="metaFile">The file path to the metadata file for this sprite.</param>
        public SpriteDeclaration(SpriteSignature signature, string metaFile)
        {
            MetaFile = metaFile;
            Signature = signature;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Imports the specified module into the sprite.
        /// </summary>
        /// <param name="module">The module to import.</param>
        public void Import(ModuleDeclaration module)
        {
            // Import signatures
            Signature.Import(module.Signature);
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Sprite Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
