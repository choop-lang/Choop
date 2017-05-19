using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a variable declaration scoped inside a method.
    /// </summary>
    public class ScopedVarDeclaration : IVarDeclaration<IExpression>, IScopedDeclaration, ICompilable<Block[]>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name => StackRef.Name;

        /// <summary>
        /// Gets the type of the data stored in the variable.
        /// </summary>
        public DataType Type => StackRef.Type;

        /// <summary>
        /// Gets the initial value stored in the variable.
        /// </summary>
        public IExpression Value { get; }

        /// <summary>
        /// Gets the <see cref="StackValue"/> describing the variable on the stack.
        /// </summary>
        public StackValue StackRef { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="ScopedVarDeclaration"/> class. 
        /// </summary>
        /// <param name="value">The initial value of the variable.</param>
        /// <param name="stackRef">The stack ref for this variable.</param>
        public ScopedVarDeclaration(IExpression value, StackValue stackRef)
        {
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
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
