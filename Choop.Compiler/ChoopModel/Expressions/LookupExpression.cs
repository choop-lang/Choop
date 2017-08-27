using System;
using Antlr4.Runtime;
using Choop.Compiler.BlockModel;
using Choop.Compiler.ChoopModel.Declarations;
using Choop.Compiler.ChoopModel.Methods;
using Choop.Compiler.Helpers;

namespace Choop.Compiler.ChoopModel.Expressions
{
    /// <summary>
    /// Represents a value lookup expression.
    /// </summary>
    public class LookupExpression : IExpression
    {
        #region Fields

        /// <summary>
        /// The name of the identifier to look up.
        /// </summary>
        private readonly string _identifierName;

        /// <summary>
        /// The declaration of the variable being looked up.
        /// </summary>
        private readonly ITypedDeclaration _variable;

        #endregion

        #region Properties

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
            _identifierName = identifierName;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LookupExpression"/> class.
        /// </summary>
        /// <param name="variable">The variable being looked up.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="errorToken">The token to report any compiler errors to.</param>
        public LookupExpression(ITypedDeclaration variable, string fileName, IToken errorToken)
        {
            _variable = variable;
            _identifierName = variable.Name;
            FileName = fileName;
            ErrorToken = errorToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Balances the binary trees within the expression.
        /// </summary>
        /// <returns>The balanced expression.</returns>
        public virtual IExpression Balance() => this;

        /// <summary>
        /// Returns the output type of the translated expression.
        /// </summary>
        /// <param name="context">The current translation state.</param>
        public DataType GetReturnType(TranslationContext context) =>
            (context.GetDeclaration(_identifierName) as ITypedDeclaration)?.Type ?? DataType.Object;

        /// <summary>
        /// Gets the translated code for the grammar structure.
        /// </summary>
        /// <returns>The translated code for the grammar structure.</returns>
        public virtual object Translate(TranslationContext context)
        {
            ITypedDeclaration variable = GetDeclaration(context);

            switch (variable)
            {
                case null:
                    // Error already processed
                    return null;

                case StackValue stackValue:
                    return stackValue.CreateVariableLookup();

                case ParamDeclaration _:
                    return new Block(BlockSpecs.GetParameter, variable.Name);

                case GlobalVarDeclaration _:
                    return new Block(BlockSpecs.GetVariable, variable.Name);

                case ConstDeclaration constDeclaration:
                    return constDeclaration.Value.Value;

                default:
                    context.ErrorList.Add(new CompilerError($"'{_identifierName}' is not a variable",
                        ErrorType.ImproperUsage, ErrorToken, FileName));
                    return null;
            }
        }

        /// <summary>
        /// Returns the declaration for the variable to look up.
        /// </summary>
        protected ITypedDeclaration GetDeclaration(TranslationContext context)
        {
            // Try pre-found declaration
            if (_variable != null)
                return _variable;

            // Search for declaration
            IDeclaration declaration = context.GetDeclaration(_identifierName);

            if (declaration == null)
            {
                context.ErrorList.Add(new CompilerError($"Variable '{_identifierName}' is not defined", ErrorType.NotDefined, ErrorToken, FileName));
                return null;
            }

            // Check declaration is a value
            if (declaration is ITypedDeclaration typedDeclaration)
                return typedDeclaration;

            context.ErrorList.Add(new CompilerError($"'{_identifierName}' is not a value",
                ErrorType.ImproperUsage, ErrorToken, FileName));
            return null;
        }

        #endregion
    }
}