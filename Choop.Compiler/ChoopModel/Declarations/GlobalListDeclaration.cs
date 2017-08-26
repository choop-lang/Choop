using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Expressions;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Declarations
{
    /// <summary>
    /// Represents a global list or array declaration.
    /// </summary>
    public class GlobalListDeclaration : IVarDeclaration<Collection<TerminalExpression>>, IArrayDeclaration,
        ICompilable<List>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the list.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the data stored in each list item.
        /// </summary>
        public DataType Type { get; }

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        public int Length => Value.Count;

        /// <summary>
        /// Gets the initial values stored in the list.
        /// </summary>
        public Collection<TerminalExpression> Value { get; } = new Collection<TerminalExpression>();

        /// <summary>
        /// Gets whether the list acts as an array.
        /// </summary>
        public bool IsArray { get; }

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
        /// Creates a new instance of the <see cref="GlobalListDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <param name="type">The data type of items in the list.</param>
        /// <param name="isArray">Whether the list acts as an array.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public GlobalListDeclaration(string name, DataType type, bool isArray, string fileName, IToken errorToken)
        {
            Name = name;
            Type = type;
            IsArray = isArray;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalListDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <param name="type">The data type of items in the list.</param>
        /// <param name="isArray">Whether the list acts as an array.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        /// <param name="initialValues">The initial values in the lsit.</param>
        public GlobalListDeclaration(string name, DataType type, bool isArray, string fileName, IToken errorToken,
            params TerminalExpression[] initialValues) : this(name, type, isArray, fileName, errorToken)
        {
            Value = new Collection<TerminalExpression>(initialValues);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public List Translate(TranslationContext context)
        {
            List result = new List(Name);

            foreach (TerminalExpression expression in Value)
                result.Contents.Add(expression.Parse());

            return result;
        }

        #endregion
    }
}