using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Declarations
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
        public int Length => Value.Count;

        /// <summary>
        /// Gets the expressions for the initial values stored in the array.
        /// </summary>
        public Collection<IExpression> Value { get; } = new Collection<IExpression>();

        /// <summary>
        /// Gets whether the variable was marked as unsafe at declaration.
        /// </summary>
        public bool UnsafeDeclaration { get; }

        /// <summary>
        /// Gets the token to report any compiler errors to.
        /// </summary>
        public IToken ErrorToken { get; }

        /// <summary>
        /// Gets the file name where the grammar structure was found.
        /// </summary>
        public string FileName { get; }

        IEnumerable<IExpression> IVarDeclaration<IEnumerable<IExpression>>.Value => Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ScopedArrayDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the array.</param>
        /// <param name="type">The data type of items inside the array.</param>
        /// <param name="unsafeDeclaration">Whether the array was marked unsafe at declaration.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ScopedArrayDeclaration(string name, DataType type, bool unsafeDeclaration, string fileName, IToken errorToken)
        {
            Name = name;
            Type = type;
            UnsafeDeclaration = unsafeDeclaration;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the stack reference for this array.
        /// </summary>
        /// <returns>The stack reference for this array.</returns>
        public StackValue GetStackRef() => new StackValue(Name, Type, UnsafeDeclaration, Length);

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public IEnumerable<Block> Translate(TranslationContext context)
        {
            if (context.GetDeclaration(Name) != null)
            {
                // Declaration already exits
                context.ErrorList.Add(new CompilerError($"Project already contains a definition for '{Name}'",
                    ErrorType.DuplicateDeclaration, ErrorToken, FileName));
                return new Block[0];
            }

            // Add to stack
            StackValue stackValue = GetStackRef();
            context.CurrentScope.StackValues.Add(stackValue);

            // Create blocks
            return stackValue.CreateDeclaration(context, Value.ToArray());
        }

        #endregion
    }
}