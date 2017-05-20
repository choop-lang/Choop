using System;
using System.Linq;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global list or array declaration.
    /// </summary>
    public class GlobalListDeclaration : IGlobalVarDeclaration<TerminalExpression[], VarSignature>, IArrayDeclaration, ICompilable<List>
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
        public int Length => Value.Length;

        /// <summary>
        /// Gets the initial values stored in the list.
        /// </summary>
        public TerminalExpression[] Value { get; }

        /// <summary>
        /// Gets whether the list acts as an array.
        /// </summary>
        public bool IsArray { get; }
        
        IExpression[] IVarDeclaration<IExpression[]>.Value => Value.Cast<IExpression>().ToArray();
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="GlobalListDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the list.</param>
        /// <param name="type">The data type of items in the list.</param>
        /// <param name="value">The initial values of the list.</param>
        /// <param name="isArray">Whether the list acts as an array.</param>
        public GlobalListDeclaration(string name, DataType type, TerminalExpression[] value, bool isArray)
        {
            Name = name;
            Type = type;
            Value = value;
            IsArray = isArray;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Returns the signature of the object being declared.
        /// </summary>
        /// <returns>The signature of the object being declared.</returns>
        public VarSignature GetSignature()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public List Translate()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}