using System.Collections.ObjectModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a module declaration.
    /// </summary>
    public class ModuleDeclaration : ISpriteDeclaration
    {
        #region Properties
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        public string Name => Signature.Name;
        
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
        /// Gets the signature of the module.
        /// </summary>
        public ModuleSignature Signature { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleDeclaration"/> class.
        /// </summary>
        /// <param name="signature">The signature of the module.</param>
        public ModuleDeclaration(ModuleSignature signature)
        {
            Signature = signature;
        }
        #endregion
    }
}
