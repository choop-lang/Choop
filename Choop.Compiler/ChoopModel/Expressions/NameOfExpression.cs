using Antlr4.Runtime;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
{
    /// <summary>
    /// Represents a nameof expression.
    /// </summary>
    public class NameOfExpression : IExpression
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the identifier to validate.
        /// </summary>
        public string Identifier { get; }

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
        /// Creates a new instance of the <see cref="NameOfExpression"/> class.
        /// </summary>
        public NameOfExpression(string identifier, string fileName, IToken errorToken)
        {
            Identifier = identifier;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            IDeclaration declaration = context.GetDeclaration(Identifier);

            if (declaration != null) return Identifier;

            context.ErrorList.Add(new CompilerError($"Identifier '{Identifier}' is not defined", ErrorType.NotDefined,
                ErrorToken, FileName));
            return string.Empty;
        }

        /// <summary>
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        public IExpression Balance() => this;

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        public DataType GetReturnType(TranslationContext context) => DataType.String;

        #endregion
    }
}
