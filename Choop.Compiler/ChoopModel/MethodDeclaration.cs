using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a function or void declaration.
    /// </summary>
    public class MethodDeclaration : ITypedDeclaration, ICompilable<BlockDef>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name => Signature.Name;

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public DataType Type => Signature.Type;

        /// <summary>
        /// Gets the collection of statements within the method.
        /// </summary>
        public Collection<ICompilable<Block[]>> Statements { get; } = new Collection<ICompilable<Block[]>>();

        /// <summary>
        /// Gets the signature of the method.
        /// </summary>
        public MethodSignature Signature { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="MethodDeclaration"/> class.
        /// </summary>
        /// <param name="signature">The signature of the method.</param>
        public MethodDeclaration(MethodSignature signature)
        {
            Signature = signature;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public BlockDef Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
