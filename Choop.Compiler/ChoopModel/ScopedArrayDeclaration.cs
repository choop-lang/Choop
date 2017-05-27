using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an array declaration scoped inside a method.
    /// </summary>
    public class ScopedArrayDeclaration : IArrayDeclaration, IScopedDeclaration, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the array.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in each element of the array.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the length of the array.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the expressions for the initial values stored in the array.
        /// </summary>
        public IExpression[] Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ScopedArrayDeclaration"/> class. 
        /// </summary>
        /// <param name="name">The name of the array.</param>
        /// <param name="type">The data type of items inside the array.</param>
        /// <param name="length">The length of the array.</param>
        /// <param name="value">The expressions for the initial values in the array.</param>
        public ScopedArrayDeclaration(string name, DataType type, int length, IExpression[] value)
        {
            if (length != value.Length)
                throw new ArgumentException("Stack space and array length do not match", nameof(value));

            Name = name;
            Type = type;
            Length = length;
            Value = value;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets the stack reference for this array.
        /// </summary>
        /// <returns>The stack reference for this array.</returns>
        public StackValue GetStackRef()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
