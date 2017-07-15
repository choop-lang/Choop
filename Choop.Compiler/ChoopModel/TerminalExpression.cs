﻿using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents an expression which is terminal. (constant)
    /// </summary>
    public class TerminalExpression : IExpression
    {
        #region Properties

        /// <summary>
        /// Gets the unparsed string value of the terminal expression.
        /// </summary>
        public string Literal { get; }

        /// <summary>
        /// Gets the data type of the literal.
        /// </summary>
        public DataType LiteralType { get; }

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
        /// Creates a new instance of the <see cref="TerminalExpression"/> class.
        /// </summary>
        /// <param name="literal">The unparsed string value of the expression.</param>
        /// <param name="literalType">The data type of the literal value.</param>
        public TerminalExpression(string literal, DataType literalType)
        {
            Literal = literal;
            LiteralType = literalType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public Block Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}