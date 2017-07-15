using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a value lookup expression.
    /// </summary>
    public class LookupExpression : IExpression
    {
        #region Properties

        /// <summary>
        /// Gets the name of the identifier being looked up.
        /// </summary>
        public string IdentifierName { get; }

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
        /// Creates a new instance of the <see cref="LookupExpression"/> class.
        /// </summary>
        /// <param name="identifierName">The name of the identifier being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public LookupExpression(string identifierName, string fileName, IToken errorToken)
        {
            IdentifierName = identifierName;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public virtual Block Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}