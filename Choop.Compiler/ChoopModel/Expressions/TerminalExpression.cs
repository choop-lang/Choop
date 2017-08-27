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
        /// Gets the parsed value of the terminal expression.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Gets the return type of the terminal expression.
        /// </summary>
        public DataType Type { get; }

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
            Value = Parse(literal, literalType);
            Type = literalType.GetOutputType();
            FileName = fileName;
            ErrorToken = errorToken;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TerminalExpression"/> class.
        /// </summary>
        /// <param name="value">The parsed value of the expression.</param>
        /// <param name="type">The return type of the expression.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public TerminalExpression(object value, DataType type = DataType.Object, string fileName = null, IToken errorToken = null)
        {
            Value = value;
            Type = type;
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
        public DataType GetReturnType(TranslationContext context) => Type;

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context) => Value;

        /// <summary>
        /// Parses the literal into an object.
        /// </summary>
        /// <returns>The internal representation of the literal.</returns>
        private static object Parse(string literal, TerminalType literalType)
        {
            switch (literalType)
            {
                case TerminalType.Bool:
                    return bool.Parse(literal);

                case TerminalType.String:
                    return JToken.Parse(literal).ToString();

                case TerminalType.Int:
                    return int.Parse(literal);

                case TerminalType.Decimal:
                    return decimal.Parse(literal);

                case TerminalType.Hex:
                    return int.Parse(literal.Substring(2), NumberStyles.AllowHexSpecifier);

                case TerminalType.Scientific:
                    return decimal.Parse(literal,
                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}