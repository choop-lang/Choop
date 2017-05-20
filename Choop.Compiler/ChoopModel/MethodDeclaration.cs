using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a function or void declaration.
    /// </summary>
    public class MethodDeclaration : ITypedDeclaration, ICompilable<BlockDef>, IHasSignature<MethodSignature>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets whether the method will return a value.
        /// </summary>
        public bool HasReturn { get; }

        /// <summary>
        /// Gets the collection of parameters declared in the method.
        /// </summary>
        public ReadOnlyCollection<ParamDeclaration> Params { get; }

        /// <summary>
        /// Gets the collection of statements within the method.
        /// </summary>
        public Collection<IStatement> Statements { get; } = new Collection<IStatement>();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="MethodDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="type">The return type of the method, if applicable.</param>
        /// <param name="hasReturn">Whether the method returns a value.</param>
        /// <param name="params">The collection of parameter declarations for the method.</param>
        public MethodDeclaration(string name, DataType type, bool hasReturn, ReadOnlyCollection<ParamDeclaration> @params)
        {
            Name = name;
            Type = type;
            HasReturn = hasReturn;
            Params = @params;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Returns the signature of the object being declared.
        /// </summary>
        /// <returns>The signature of the object being declared.</returns>
        public MethodSignature GetSignature()
        {
            throw new NotImplementedException();
        }

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
