using System;
using System.Globalization;
using Antlr4.Runtime;
using Choop.Compiler.Helpers;
using Newtonsoft.Json.Linq;

namespace Choop.Compiler.ChoopModel.Expressions
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
        public TerminalType LiteralType { get; }

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
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public TerminalExpression(string literal, TerminalType literalType, string fileName = null, IToken errorToken = null)
        {
            Literal = literal;
            LiteralType = literalType;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        public IExpression Balance() => this;

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        public DataType GetReturnType(TranslationContext context)
        {
            switch (LiteralType)
            {
                case TerminalType.Bool:
                    return DataType.Boolean;

                case TerminalType.String:
                    return DataType.String;

                case TerminalType.Hex:
                case TerminalType.Scientific:
                case TerminalType.Decimal:
                case TerminalType.Int:
                    return DataType.Number;

                default:
                    throw new ArgumentOutOfRangeException(nameof(LiteralType));
            }
        }

        /// <summary>
        /// Parses the literal into an object.
        /// </summary>
        /// <returns>The internal representation of the literal.</returns>
        public object Parse()
        {
            switch (LiteralType)
            {
                case TerminalType.Bool:
                    return bool.Parse(Literal);

                case TerminalType.String:
                    return JToken.Parse(Literal).ToString();

                case TerminalType.Int:
                    return int.Parse(Literal);

                case TerminalType.Decimal:
                    return decimal.Parse(Literal);

                case TerminalType.Hex:
                    return int.Parse(Literal.Substring(2), NumberStyles.AllowHexSpecifier);

                case TerminalType.Scientific:
                    return decimal.Parse(Literal,
                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context) => Parse();

        #endregion
    }
}