using System;
using System.Collections.ObjectModel;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;

namespace Choop.Compiler.ChoopModel
{
    /// <summary>
    /// Represents a method call.
    /// </summary>
    /// <remarks>Can be used inside an expression or as a standalone statement.</remarks>
    public class MethodCall : IExpression, IStatement
    {
        #region Properties

        /// <summary>
        /// Gets the name of the method being called.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets the collection of parameters to the method.
        /// </summary>
        public Collection<IExpression> Parameters { get; }

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
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public MethodCall(string methodName, string fileName, IToken errorToken)
        {
            MethodName = methodName;
            FileName = fileName;
            ErrorToken = errorToken;
            Parameters = new Collection<IExpression>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method being called.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        /// <param name="parameters">The parameters for the method.</param>
        public MethodCall(string methodName, string fileName, IToken errorToken, params IExpression[] parameters)
        {
            MethodName = methodName;
            FileName = fileName;
            ErrorToken = errorToken;
            Parameters = new Collection<IExpression>(parameters);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public object Translate(TranslationContext context)
        {
            throw new NotImplementedException();
        }

        Block[] ICompilable<Block[]>.Translate(TranslationContext context) => new[] {(Block) Translate(context)};

        #endregion
    }
}