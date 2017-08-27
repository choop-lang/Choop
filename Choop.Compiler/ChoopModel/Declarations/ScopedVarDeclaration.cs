using System.Collections.Generic;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Declarations
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

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="ScopedVarDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The data type of the variable.</param>
        /// <param name="unsafeDeclaration">Whether the variable was marked as unsafe at declaration.</param>
        /// <param name="value">The initial value of the variable.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ScopedVarDeclaration(string name, DataType type, bool unsafeDeclaration, IExpression value, string fileName, IToken errorToken)
        {
            Name = name;
            Type = type;
            UnsafeDeclaration = unsafeDeclaration;
            Value = value;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the stack reference for this variable.
        /// </summary>
        /// <returns>The stack reference for this variable.</returns>
        public StackValue GetStackRef() => new StackValue(Name, Type, UnsafeDeclaration);

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

            // TODO: Value created before any method values
            StackValue variable = GetStackRef();
            context.CurrentScope.StackValues.Add(variable);

            return variable.CreateDeclaration(context, Value);
        }

        #endregion
    }
}