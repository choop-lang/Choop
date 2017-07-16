using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.TranslationUtils;

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
        public int Length => Value.Count;

        /// <summary>
        /// Gets the expressions for the initial values stored in the array.
        /// </summary>
        public Collection<IExpression> Value { get; } = new Collection<IExpression>();

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
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public ScopedArrayDeclaration(string name, DataType type, string fileName, IToken errorToken)
        {
            Name = name;
            Type = type;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the stack reference for this array.
        /// </summary>
        /// <returns>The stack reference for this array.</returns>
        public StackValue GetStackRef()
        {
            return new StackValue(Name, Type, Length);
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block[] Translate(TranslationContext context)
        {
            // Add to stack
            context.CurrentScope.StackValues.Add(GetStackRef());

            // Create blocks
            return Value.Select(
                expression => new Block(BlockSpecs.AddToList, expression.Translate(context), Settings.StackIdentifier)
            ).ToArray();
        }

        #endregion
    }
}