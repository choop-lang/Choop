using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an array declaration scoped inside a method.
    /// </summary>
    public class ScopedArrayDeclaration : IArrayDeclaration, IScopedDeclaration, ICompilable<Block[]>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the array.
        /// </summary>
        public string Name => StackRef.Name;

        /// <summary>
        /// Gets the type of the data stored in each element of the array.
        /// </summary>
        public DataType Type => StackRef.Type;

        /// <summary>
        /// Gets the length of the array.
        /// </summary>
        public int Length => StackRef.StackSpace;

        /// <summary>
        /// Gets the initial values stored in the array.
        /// </summary>
        public IExpression[] Value { get; }

        /// <summary>
        /// Gets the <see cref="StackValue"/> describing the array on the stack.
        /// </summary>
        public StackValue StackRef { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ScopedArrayDeclaration"/> class. 
        /// </summary>
        /// <param name="value">The initial values in the array.</param>
        /// <param name="stackRef">The stack ref for this array.</param>
        public ScopedArrayDeclaration(IExpression[] value, StackValue stackRef)
        {
            if (StackRef.StackSpace != value.Length)
                throw new ArgumentException("Stack space and array length do not match", nameof(value));

            Value = value;
            StackRef = stackRef;
        }
        #endregion
        #region Methods
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
