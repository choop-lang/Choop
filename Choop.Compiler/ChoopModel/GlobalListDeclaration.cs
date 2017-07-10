using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
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

        IEnumerable<IExpression> IVarDeclaration<IEnumerable<IExpression>>.Value => Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalListDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <param name="type">The data type of items in the list.</param>
        /// <param name="isArray">Whether the list acts as an array.</param>
        public GlobalListDeclaration(string name, DataType type, bool isArray)
        {
            Name = name;
            Type = type;
            IsArray = isArray;
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
            {
                result.Contents.Add(expression.Literal);
            }

            return result;
        }

        #endregion
    }
}