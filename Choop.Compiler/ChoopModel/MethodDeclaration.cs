using System;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a function or void declaration.
    /// </summary>
    public class MethodDeclaration : ITypedDeclaration, ICompilable<BlockDef>, IHasBody
    {
        #region Properties
        /// <summary>
        /// Gets whether the method is unsafe.
        /// </summary>
        public bool Unsafe { get; }

        /// <summary>
        /// Gets whether the method should be inlined.
        /// </summary>
        public bool Inline { get; }

        /// <summary>
        /// Gets whether the method should be atomic.
        /// </summary>
        public bool Atomic { get; }

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
        public Collection<ParamDeclaration> Params { get; } = new Collection<ParamDeclaration>();

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
        /// <param name="unsafe">Whether the method is unsafe.</param>
        /// <param name="inline">Whether the method should be inlined.</param>
        /// <param name="atomic">Whether the method should be atomic.</param>
        public MethodDeclaration(string name, DataType type, bool hasReturn, bool @unsafe, bool inline, bool atomic)
        {
            Name = name;
            Type = type;
            HasReturn = hasReturn;
            Unsafe = @unsafe;
            Inline = inline;
            Atomic = atomic;
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
