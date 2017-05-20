using System;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ObjectModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a global variable declaration.
    /// </summary>
    public class GlobalVarDeclaration : IGlobalVarDeclaration<TerminalExpression, VarSignature>, ICompilable<Variable>
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
        public TerminalExpression Value { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new instance of the <see cref="GlobalVarDeclaration"/> class.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The data type of the variable.</param>
        /// <param name="value">The initial value of the variable.</param>
        public GlobalVarDeclaration(string name, DataType type, TerminalExpression value)
        {
            Name = name;
            Type = type;
            Value = value;
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
        public Variable Translate()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}