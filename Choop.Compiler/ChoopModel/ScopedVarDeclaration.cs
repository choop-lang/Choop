using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a variable declaration scoped inside a method.
    /// </summary>
    public class ScopedVarDeclaration : IVarDeclaration<IExpression>, IScopedDeclaration, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in the variable.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the initial value stored in the variable.
        /// </summary>
        public IExpression Value { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ScopedVarDeclaration"/> class. 
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The data type of the variable.</param>
        /// <param name="value">The initial value of the variable.</param>
        public ScopedVarDeclaration(string name, DataType type, IExpression value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the stack reference for this variable.
        /// </summary>
        /// <returns>The stack reference for this variable.</returns>
        public StackValue GetStackRef()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}